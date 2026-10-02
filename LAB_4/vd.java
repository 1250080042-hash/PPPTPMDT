import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

// ==================== LỚP THỰC THỂ (ENTITY) ====================

// Lớp Tài khoản (Chứa thông tin khóa ngoại maKhachHang)
class TaiKhoan {
    private String soTaiKhoan;
    private String loaiTaiKhoan;
    private double soDuTaiKhoan;
    private String maKhachHang; // Khóa ngoại

    public TaiKhoan(String soTaiKhoan, String loaiTaiKhoan, double soDuTaiKhoan, String maKhachHang) {
        this.soTaiKhoan = soTaiKhoan;
        this.loaiTaiKhoan = loaiTaiKhoan;
        this.soDuTaiKhoan = soDuTaiKhoan;
        this.maKhachHang = maKhachHang;
    }

    public String getSoTaiKhoan() { return soTaiKhoan; }
    public String getLoaiTaiKhoan() { return loaiTaiKhoan; }
    public double getSoDuTaiKhoan() { return soDuTaiKhoan; }

    public boolean rutTien(double soTien) {
        if (soTien > 0 && soTien <= soDuTaiKhoan) {
            this.soDuTaiKhoan -= soTien;
            return true;
        }
        return false;
    }
}

// Lớp Khách hàng (Sở hữu danh sách các tài khoản - Quan hệ 1-N)
class KhachHang {
    private String maKhachHang;
    private String hoTenKhachHang;
    private String soThe;
    private String maPIN;
    private List<TaiKhoan> danhSachTaiKhoan;

    public KhachHang(String maKhachHang, String hoTenKhachHang, String soThe, String maPIN) {
        this.maKhachHang = maKhachHang;
        this.hoTenKhachHang = hoTenKhachHang;
        this.soThe = soThe;
        this.maPIN = maPIN;
        this.danhSachTaiKhoan = new ArrayList<>();
    }

    public String getSoThe() { return soThe; }
    public String getMaPIN() { return maPIN; }
    public String getHoTenKhachHang() { return hoTenKhachHang; }
    public List<TaiKhoan> getDanhSachTaiKhoan() { return danhSachTaiKhoan; }

    public void themTaiKhoan(TaiKhoan tk) {
        this.danhSachTaiKhoan.add(tk);
    }
}

// ==================== LỚP ĐIỀU KHIỂN (CONTROL) ====================

class DieuKhienRutTien {
    private List<KhachHang> danhSachKhachHang;

    public DieuKhienRutTien(List<KhachHang> danhSachKhachHang) {
        this.danhSachKhachHang = danhSachKhachHang;
    }

    // Xác thực thẻ và mã PIN
    public KhachHang xacThuc(String soThe, String maPIN) {
        for (KhachHang kh : danhSachKhachHang) {
            if (kh.getSoThe().equals(soThe) && kh.getMaPIN().equals(maPIN)) {
                return kh;
            }
        }
        return null;
    }

    // Luồng xử lý rút tiền tại ATM
    public void thucHienGiaoDich() {
        Scanner scanner = new Scanner(System.in);

        System.out.println("========== MÀN HÌNH ATM ==========");
        System.out.print("Nhập số thẻ: ");
        String soThe = scanner.nextLine();
        System.out.print("Nhập mã PIN: ");
        String maPIN = scanner.nextLine();

        KhachHang kh = xacThuc(soThe, maPIN);

        if (kh == null) {
            System.out.println("❌ Thông tin thẻ hoặc mã PIN không chính xác!");
            return;
        }

        System.out.println("\n✅ Đăng nhập thành công! Xin chào: " + kh.getHoTenKhachHang());
        List<TaiKhoan> listTK = kh.getDanhSachTaiKhoan();

        if (listTK.isEmpty()) {
            System.out.println("Không tìm thấy tài khoản liên kết với thẻ này.");
            return;
        }

        // Hiển thị danh sách tài khoản thuộc sở hữu của khách hàng (1-N)
        System.out.println("--- Danh sách tài khoản của bạn ---");
        for (int i = 0; i < listTK.size(); i++) {
            TaiKhoan tk = listTK.get(i);
            System.out.printf("%d. Số TK: %s | Loại: %s | Số dư: %,.0f VNĐ\n",
                    (i + 1), tk.getSoTaiKhoan(), tk.getLoaiTaiKhoan(), tk.getSoDuTaiKhoan());
        }

        System.out.print("Chọn tài khoản thực hiện rút tiền (1-" + listTK.size() + "): ");
        int chon = Integer.parseInt(scanner.nextLine());

        if (chon < 1 || chon > listTK.size()) {
            System.out.println("❌ Lựa chọn không hợp lệ!");
            return;
        }

        TaiKhoan tkDuocChon = listTK.get(chon - 1);

        System.out.print("Nhập số tiền cần rút (VNĐ): ");
        double soTien = Double.parseDouble(scanner.nextLine());

        if (tkDuocChon.rutTien(soTien)) {
            System.out.println("----------------------------------");
            System.out.println("🎉 Giao dịch thành công!");
            System.out.printf("Số tiền đã rút: %,.0f VNĐ\n", soTien);
            System.out.printf("Số dư còn lại của TK %s: %,.0f VNĐ\n",
                    tkDuocChon.getSoTaiKhoan(), tkDuocChon.getSoDuTaiKhoan());
            System.out.println("Vui lòng nhận tiền và thẻ!");
        } else {
            System.out.println("❌ Giao dịch thất bại: Số dư không đủ hoặc số tiền không hợp lệ.");
        }
    }
}

// ==================== CHƯƠNG TRÌNH CHÍNH ====================

public class ATMSystem {
    public static void main(String[] args) {
        // Giả lập dữ liệu từ CSDL
        List<KhachHang> dbKhachHang = new ArrayList<>();

        // Khách hàng KH01
        KhachHang kh1 = new KhachHang("KH01", "Nguyễn Văn A", "123456789", "1234");
        // 1 khách hàng sở hữu 2 tài khoản
        kh1.themTaiKhoan(new TaiKhoan("TK101", "Thanh toán", 5000000, "KH01"));
        kh1.themTaiKhoan(new TaiKhoan("TK102", "Tiết kiệm", 15000000, "KH01"));

        dbKhachHang.add(kh1);

        // Khởi chạy bộ điều khiển ATM
        DieuKhienRutTien atmControl = new DieuKhienRutTien(dbKhachHang);
        atmControl.thucHienGiaoDich();
    }
}