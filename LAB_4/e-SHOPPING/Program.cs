using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace eShoppingProject
{
    //THẺ TÍN DỤNG
    public class TheTinDung
    {
        public string LoaiThe { get; set; } // VISA, MASTER, DISCOVER, AMEX
        public string SoThe { get; set; }
        public string NgayHetHan { get; set; }
        public string TenChuThe { get; set; }
        public string MaCSV { get; set; }

        // Hàm khớp với: KiemTraTheHopLe() trong sơ đồ
        public bool KiemTraTheHopLe(out string errorMsg)
        {
            errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(SoThe) || string.IsNullOrWhiteSpace(MaCSV))
            {
                errorMsg = "Số thẻ và mã CSV không được để trống!";
                return false;
            }

            string cleanNum = SoThe.Replace(" ", "").Replace("-", "");

            if (LoaiThe == "VISA" || LoaiThe == "MASTER" || LoaiThe == "DISCOVER")
            {
                if (cleanNum.Length != 16 || !Regex.IsMatch(cleanNum, @"^\d{16}$"))
                {
                    errorMsg = $"{LoaiThe} yêu cầu số thẻ gồm đúng 16 chữ số!";
                    return false;
                }
                if (MaCSV.Length != 3 || !Regex.IsMatch(MaCSV, @"^\d{3}$"))
                {
                    errorMsg = $"{LoaiThe} yêu cầu mã CSV gồm đúng 3 chữ số!";
                    return false;
                }
            }
            else if (LoaiThe == "AMEX")
            {
                if (cleanNum.Length != 15 || !Regex.IsMatch(cleanNum, @"^\d{15}$"))
                {
                    errorMsg = "American Express yêu cầu số thẻ gồm đúng 15 chữ số!";
                    return false;
                }
                if (MaCSV.Length != 4 || !Regex.IsMatch(MaCSV, @"^\d{4}$"))
                {
                    errorMsg = "American Express yêu cầu mã CSV gồm đúng 4 chữ số!";
                    return false;
                }
            }
            else
            {
                errorMsg = "Loại thẻ không nằm trong danh sách hỗ trợ!";
                return false;
            }

            return true;
        }
    }

    //BỘ TÍNH PHÍ GIAO HÀNG
    public static class BoTinhPhiGiaoHang
    {
        // Hàm khớp với: TinhPhiGiaoHang(LoaiPhieu, TienHang) trong sơ đồ
        public static decimal TinhPhiGiaoHang(string loaiPhieu, decimal tienHang)
        {
            if (tienHang >= 5000000m) return 0m; 
            if (tienHang >= 1000000m && (loaiPhieu == "NORMAL" || loaiPhieu == "EXPRESS")) return 0m; 

            if (loaiPhieu == "EXPRESS") return 40000m;
            if (loaiPhieu == "SAME_DAY") return 80000m;
            return 20000m; // NORMAL
        }
    }

    //CỔNG THANH TOÁN ADAPTER
    public static class CongThanhToanAdapter
    {
        // Hàm khớp với: XuLyThanhToan(ThongTinThe, TongTien) trong sơ đồ
        public static bool XuLyThanhToan(TheTinDung thongTinThe, decimal tongTien, out string maGiaoDich, out string error)
        {
            maGiaoDich = string.Empty;

            // Luồng gọi: KiemTraTheHopLe() sang đối tượng Thẻ Tín Dụng
            if (!thongTinThe.KiemTraTheHopLe(out error)) return false;

            if (thongTinThe.SoThe.EndsWith("0000"))
            {
                error = "Giao dịch bị từ chối: Tài khoản thẻ không đủ số dư!";
                return false;
            }

            maGiaoDich = "PG-TXN-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            return true;
        }
    }

    //CƠ SỞ DỮ LIỆU SQLITE
    public static class CoSoDuLieuSqlite
    {
        private static string connectionString = "Data Source=eShopping.db";

        public static void KhoiTaoDatabase()
        {
            if (File.Exists("schema.sql"))
            {
                string script = File.ReadAllText("schema.sql");
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = script;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static long LuuDonHangVaoCSDL(string loaiPhieu, string tenNguoiNhan, string diaChi, string sdt, decimal tienHang, decimal phiGiaoHang, decimal tongTien, string loaiThe, string soThe, string maGiaoDich)
        {
            try
            {
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    string maskedCard = "XXXX-XXXX-XXXX-" + soThe.Substring(soThe.Length - 4);
                    string query = @"
                        INSERT INTO Orders (CustomerID, OrderTypeID, ReceiverName, ReceiverAddress, ReceiverPhone, SubTotal, ShippingFee, TotalAmount, CardType, MaskedCardNumber, PaymentStatus, PaymentTransactionID, OrderStatus)
                        VALUES (1, @LoaiPhieu, @Ten, @DiaChi, @SDT, @TienHang, @PhiGiaoHang, @TongTien, @LoaiThe, @MaskedCard, 'SUCCESS', @MaGiaoDich, 'PENDING');
                        SELECT last_insert_rowid();";

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@LoaiPhieu", loaiPhieu);
                        cmd.Parameters.AddWithValue("@Ten", tenNguoiNhan);
                        cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                        cmd.Parameters.AddWithValue("@SDT", sdt);
                        cmd.Parameters.AddWithValue("@TienHang", tienHang);
                        cmd.Parameters.AddWithValue("@PhiGiaoHang", phiGiaoHang);
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@LoaiThe", loaiThe);
                        cmd.Parameters.AddWithValue("@MaskedCard", maskedCard);
                        cmd.Parameters.AddWithValue("@MaGiaoDich", maGiaoDich);

                        return (long)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi CSDL: " + ex.Message);
                return -1;
            }
        }
    }

    //DỊCH VỤ EMAIL ADAPTER
    public static class DichVuEmailAdapter
    {
        public static void GuiEmailXacNhan(string email, long maDonHang, decimal tongTien, string tenNguoiNhan)
        {
            if (string.IsNullOrEmpty(email)) return;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n┌────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                    [DỊCH VỤ EMAIL] XÁC NHẬN ĐƠN HÀNG                   │");
            Console.WriteLine("├────────────────────────────────────────────────────────────────────────┤");
            Console.WriteLine($"│ Gửi tới:    {email,-58} │");
            Console.WriteLine($"│ Mã đơn hàng: #{maDonHang,-56} │");
            Console.WriteLine($"│ Người nhận:  {tenNguoiNhan,-57} │");
            Console.WriteLine($"│ Tổng tiền:   {tongTien:N0} VNĐ" + new string(' ', 59 - $"{tongTien:N0} VNĐ".Length) + "│");
            Console.WriteLine("│ *(Thông tin thẻ tín dụng đã được bảo mật tuyệt đối theo chuẩn PCI-DSS)*│");
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────┘");
            Console.ResetColor();
        }
    }

    //GIAO DIỆN CHƯƠNG TRÌNH (MAIN)
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CoSoDuLieuSqlite.KhoiTaoDatabase();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================================================");
            Console.WriteLine("             GIAO DIỆN CHƯƠNG TRÌNH BÁN HÀNG e-SHOPPING                 ");
            Console.WriteLine("========================================================================");
            Console.ResetColor();

            // Nhập tiền hàng & chọn loại phiếu giao
            Console.Write("\n➔ Nhập tổng tiền hàng (VNĐ): ");
            decimal tienHang;
            while (!decimal.TryParse(Console.ReadLine(), out tienHang) || tienHang <= 0)
            {
                Console.Write("Số tiền không hợp lệ, nhập lại: ");
            }

            Console.WriteLine("\nChọn loại phiếu giao hàng:");
            Console.WriteLine("1. NORMAL   - Giao hàng thường");
            Console.WriteLine("2. EXPRESS  - Chuyển phát nhanh");
            Console.WriteLine("3. SAME_DAY - Chuyển phát nhanh trong ngày");
            Console.Write("➔ Lựa chọn (1-3): ");
            string choice = Console.ReadLine();
            string loaiPhieu = choice == "2" ? "EXPRESS" : (choice == "3" ? "SAME_DAY" : "NORMAL");

            //Tính Phí Giao Hàng(LoạiPhiếu, TiềnHàng) -> BỘ TÍNH PHÍ GIAO HÀNG
            decimal phiGiaoHang = BoTinhPhiGiaoHang.TinhPhiGiaoHang(loaiPhieu, tienHang);
            decimal tongTien = tienHang + phiGiaoHang;

            // THÔNG ĐIỆP 3: Trả về phí giao hàng & tổng tiền
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n[KẾT QUẢ TÍNH PHÍ]: Phí giao hàng = {phiGiaoHang:N0} VNĐ | TỔNG TIỀN = {tongTien:N0} VNĐ");
            Console.ResetColor();

            //Nhập người nhận & thông tin thẻ tín dụng
            Console.WriteLine("\n--- NHẬP THÔNG TIN NGƯỜI NHẬN ---");
            Console.Write("➔ Họ tên người nhận: ");
            string tenNguoiNhan = Console.ReadLine();
            Console.Write("➔ Địa chỉ giao hàng: ");
            string diaChi = Console.ReadLine();
            Console.Write("➔ Số điện thoại: ");
            string sdt = Console.ReadLine();
            Console.Write("➔ Email nhận thông báo: ");
            string email = Console.ReadLine();

            Console.WriteLine("\n NHẬP THÔNG TIN THẺ TÍN DỤNG ---");
            Console.Write("➔ Loại thẻ (VISA / MASTER / DISCOVER / AMEX): ");
            string loaiThe = Console.ReadLine().Trim().ToUpper();
            Console.Write("➔ Số thẻ: ");
            string soThe = Console.ReadLine().Trim();
            Console.Write("➔ Mã an ninh CSV: ");
            string maCSV = Console.ReadLine().Trim();

            TheTinDung the = new TheTinDung
            {
                LoaiThe = loaiThe,
                SoThe = soThe,
                MaCSV = maCSV,
                NgayHetHan = "12/28",
                TenChuThe = tenNguoiNhan
            };

            //Xử Lý Thanh Toán(ThôngTinThẻ, TổngTiền) -> CỔNG THANH TOÁN ADAPTER
            Console.WriteLine("\n Đang gửi yêu cầu thanh toán...");
            if (!CongThanhToanAdapter.XuLyThanhToan(the, tongTien, out string maGiaoDich, out string loiThanhToan))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n [LỖI THANH TOÁN]: {loiThanhToan}");
                Console.ResetColor();
                return;
            }

            //Lưu Đơn Hàng Vào CSDL(...) -> CƠ SỞ DỮ LIỆU SQLITE
            long maDonHang = CoSoDuLieuSqlite.LuuDonHangVaoCSDL(loaiPhieu, tenNguoiNhan, diaChi, sdt, tienHang, phiGiaoHang, tongTien, loaiThe, soThe, maGiaoDich);

            //Gửi Email Xác Nhận(...) -> DỊCH VỤ EMAIL ADAPTER
            if (maDonHang > 0)
            {
                DichVuEmailAdapter.GuiEmailXacNhan(email, maDonHang, tongTien, tenNguoiNhan);

                //Hiển thị thông báo thành công & Email xác nhận
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✔ THÀNH CÔNG: Đơn hàng #{maDonHang} đã được khởi tạo thành công trên hệ thống!");
                Console.ResetColor();
            }
        }
    }
}