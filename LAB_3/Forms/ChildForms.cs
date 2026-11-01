using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.Forms
{
    // =============================================
    // 1. FrmDanhMuc (Khu vực, Nhân viên, Loại tiện nghi, Dịch vụ, Quy định đền bù)
    // =============================================
    public class FrmDanhMuc : Form
    {
        public FrmDanhMuc()
        {
            this.Text = "DANH MỤC NỀN";
            this.Size = new Size(880, 620);
            this.StartPosition = FormStartPosition.CenterParent;

            TabControl tab = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };
            
            // Tab Khu vực
            TabPage tabKhu = new TabPage("Khu vực");
            DataGridView dgvKhu = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvKhu.Columns.Add("Ma", "Mã khu vực"); dgvKhu.Columns.Add("Ten", "Tên khu vực");
            dgvKhu.Rows.Add("A", "Khu A - Cao cấp"); dgvKhu.Rows.Add("B", "Khu B - Tiêu chuẩn");
            tabKhu.Controls.Add(dgvKhu);

            // Tab Nhân viên
            TabPage tabNV = new TabPage("Nhân viên");
            DataGridView dgvNV = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvNV.Columns.Add("Ma", "Mã NV"); dgvNV.Columns.Add("Ten", "Họ tên"); dgvNV.Columns.Add("VaiTro", "Vai trò"); dgvNV.Columns.Add("SDT", "Số điện thoại");
            dgvNV.Rows.Add("NV01", "Nguyễn Thu Hà", "Lễ tân", "0901000001");
            dgvNV.Rows.Add("NV02", "Trần Minh An", "Phục vụ phòng", "0901000002");
            dgvNV.Rows.Add("NV03", "Lê Hoàng Nam", "Thanh toán", "0901000003");
            tabNV.Controls.Add(dgvNV);

            // Tab Loại Tiện Nghi
            TabPage tabLoai = new TabPage("Loại tiện nghi");
            DataGridView dgvLoai = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvLoai.Columns.Add("Ma", "Mã loại"); dgvLoai.Columns.Add("Ten", "Tên loại tiện nghi");
            dgvLoai.Rows.Add("TV", "Ti vi"); dgvLoai.Rows.Add("TL", "Tủ lạnh"); dgvLoai.Rows.Add("DT", "Điện thoại");
            tabLoai.Controls.Add(dgvLoai);

            // Tab Dịch vụ
            TabPage tabDV = new TabPage("Dịch vụ");
            DataGridView dgvDV = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvDV.Columns.Add("Ma", "Mã DV"); dgvDV.Columns.Add("Ten", "Tên dịch vụ"); dgvDV.Columns.Add("DVT", "Đơn vị tính"); dgvDV.Columns.Add("Gia", "Đơn giá");
            dgvDV.Rows.Add("DV01", "Ăn sáng", "Suất", "120,000");
            dgvDV.Rows.Add("DV02", "Tắm hơi", "Lượt", "250,000");
            dgvDV.Rows.Add("DV03", "Karaoke", "Giờ", "300,000");
            tabDV.Controls.Add(dgvDV);

            // Tab Quy định đền bù
            TabPage tabQD = new TabPage("Quy định đền bù");
            DataGridView dgvQD = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvQD.Columns.Add("Ma", "Mã quy định"); dgvQD.Columns.Add("Loai", "Loại tiện nghi"); dgvQD.Columns.Add("MucDo", "Mức độ thiệt hại"); dgvQD.Columns.Add("Tien", "Mức đền bù");
            dgvQD.Rows.Add("QD01", "Ti vi", "Hư hỏng nhẹ", "500,000");
            dgvQD.Rows.Add("QD02", "Ti vi", "Mất", "5,000,000");
            dgvQD.Rows.Add("QD03", "Tủ lạnh", "Hư hỏng nhẹ", "400,000");
            tabQD.Controls.Add(dgvQD);

            tab.TabPages.AddRange(new TabPage[] { tabKhu, tabNV, tabLoai, tabDV, tabQD });
            this.Controls.Add(tab);
        }
    }

    // =============================================
    // 2. FrmPhongTienNghi (Quản lý Phòng, Tiện nghi, Phiếu lắp đặt)
    // =============================================
    public class FrmPhongTienNghi : Form
    {
        public FrmPhongTienNghi()
        {
            this.Text = "QUẢN LÝ PHÒNG VÀ TIỆN NGHI";
            this.Size = new Size(880, 620);
            this.StartPosition = FormStartPosition.CenterParent;

            TabControl tab = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10) };

            TabPage tabPhong = new TabPage("Danh sách Phòng");
            DataGridView dgvPhong = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvPhong.Columns.Add("SoPhong", "Số phòng"); dgvPhong.Columns.Add("Khu", "Khu vực"); dgvPhong.Columns.Add("SucChua", "Sức chứa tối đa"); dgvPhong.Columns.Add("Gia", "Đơn giá ngày"); dgvPhong.Columns.Add("TT", "Trạng thái");
            dgvPhong.Rows.Add("A101", "Khu A", "2 người", "600,000", "Trống");
            dgvPhong.Rows.Add("A102", "Khu A", "3 người", "800,000", "Đang ở");
            dgvPhong.Rows.Add("B201", "Khu B", "4 người", "1,200,000", "Trống");
            tabPhong.Controls.Add(dgvPhong);

            TabPage tabTN = new TabPage("Tiện nghi thiết bị");
            DataGridView dgvTN = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgvTN.Columns.Add("Ma", "Mã thiết bị"); dgvTN.Columns.Add("Loai", "Loại tiện nghi"); dgvTN.Columns.Add("STT", "Số thứ tự"); dgvTN.Columns.Add("TT", "Tình trạng");
            dgvTN.Rows.Add("TV01", "Ti vi", "1", "Tốt");
            dgvTN.Rows.Add("TV02", "Ti vi", "2", "Tốt");
            dgvTN.Rows.Add("TL01", "Tủ lạnh", "1", "Tốt");
            tabTN.Controls.Add(dgvTN);

            tab.TabPages.Add(tabPhong);
            tab.TabPages.Add(tabTN);
            this.Controls.Add(tab);
        }
    }

    // =============================================
    // 3. FrmDatPhong (Quản lý Khách, Đặt phòng, Người lưu trú, Nhận phòng)
    // =============================================
    public class FrmDatPhong : Form
    {
        public FrmDatPhong()
        {
            this.Text = "ĐẶT / NHẬN PHÒNG";
            this.Size = new Size(900, 640);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTitle = new Label { Text = "QUẢN LÝ ĐẶT VÀ NHẬN PHÒNG", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(25, 60, 110), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleCenter };

            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 170, Padding = new Padding(15) };
            pnlTop.Controls.Add(new Label { Text = "Mã phiếu đặt:", Location = new Point(15, 15), AutoSize = true });
            pnlTop.Controls.Add(new TextBox { Location = new Point(110, 12), Width = 180, Text = "PD001" });

            pnlTop.Controls.Add(new Label { Text = "Khách hàng:", Location = new Point(310, 15), AutoSize = true });
            ComboBox cbKH = new ComboBox { Location = new Point(400, 12), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            cbKH.Items.AddRange(new string[] { "KH01 - Nguyễn Văn Nam", "KH02 - Lê Thị Hoa" }); cbKH.SelectedIndex = 0;
            pnlTop.Controls.Add(cbKH);

            pnlTop.Controls.Add(new Label { Text = "Kênh đặt:", Location = new Point(640, 15), AutoSize = true });
            ComboBox cbKenh = new ComboBox { Location = new Point(710, 12), Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            cbKenh.Items.AddRange(new string[] { "Trực tiếp", "Website", "Điện thoại" }); cbKenh.SelectedIndex = 0;
            pnlTop.Controls.Add(cbKenh);

            pnlTop.Controls.Add(new Label { Text = "Ngày nhận:", Location = new Point(15, 55), AutoSize = true });
            pnlTop.Controls.Add(new DateTimePicker { Location = new Point(110, 52), Width = 180, Format = DateTimePickerFormat.Short });

            pnlTop.Controls.Add(new Label { Text = "Ngày trả dự kiến:", Location = new Point(310, 55), AutoSize = true });
            pnlTop.Controls.Add(new DateTimePicker { Location = new Point(430, 52), Width = 190, Format = DateTimePickerFormat.Short });

            pnlTop.Controls.Add(new Label { Text = "Tiền cọc:", Location = new Point(640, 55), AutoSize = true });
            pnlTop.Controls.Add(new TextBox { Location = new Point(710, 52), Width = 140, Text = "500,000" });

            pnlTop.Controls.AddRange(new Control[] {
                new Button { Text = "Lập phiếu đặt", Location = new Point(110, 105), Size = new Size(130, 35), BackColor = Color.FromArgb(25, 118, 210), ForeColor = Color.White, FlatStyle = FlatStyle.Flat },
                new Button { Text = "Nhận phòng", Location = new Point(250, 105), Size = new Size(130, 35), BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat },
                new Button { Text = "Đánh dấu No-show", Location = new Point(390, 105), Size = new Size(150, 35), BackColor = Color.FromArgb(211, 47, 47), ForeColor = Color.White, FlatStyle = FlatStyle.Flat }
            });

            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgv.Columns.Add("SoPhieu", "Mã phiếu"); dgv.Columns.Add("Khach", "Tên khách hàng"); dgv.Columns.Add("NgayNhan", "Ngày nhận"); dgv.Columns.Add("NgayTra", "Ngày trả"); dgv.Columns.Add("Coc", "Tiền cọc"); dgv.Columns.Add("TT", "Trạng thái");
            dgv.Rows.Add("PD001", "Nguyễn Văn Nam", "02/11/2026", "05/11/2026", "500,000", "Đang ở");
            dgv.Rows.Add("PD002", "Lê Thị Hoa", "03/11/2026", "06/11/2026", "300,000", "Đã đặt");

            this.Controls.Add(dgv);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);
        }
    }

    // =============================================
    // 4. FrmDichVu (Ghi nhận dịch vụ lưu trú)
    // =============================================
    public class FrmDichVu : Form
    {
        public FrmDichVu()
        {
            this.Text = "SỬ DỤNG DỊCH VỤ";
            this.Size = new Size(880, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTitle = new Label { Text = "GHI NHẬN SỬ DỤNG DỊCH VỤ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(25, 60, 110), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleCenter };

            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 140, Padding = new Padding(15) };
            pnlTop.Controls.Add(new Label { Text = "Lượt ở (Phòng):", Location = new Point(15, 15), AutoSize = true });
            ComboBox cbLuot = new ComboBox { Location = new Point(120, 12), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            cbLuot.Items.AddRange(new string[] { "PD001 - Phòng A102 (Nguyễn Văn Nam)" }); cbLuot.SelectedIndex = 0;
            pnlTop.Controls.Add(cbLuot);

            pnlTop.Controls.Add(new Label { Text = "Dịch vụ:", Location = new Point(430, 15), AutoSize = true });
            ComboBox cbDV = new ComboBox { Location = new Point(500, 12), Width = 240, DropDownStyle = ComboBoxStyle.DropDownList };
            cbDV.Items.AddRange(new string[] { "DV01 - Ăn sáng (120k)", "DV02 - Tắm hơi (250k)", "DV03 - Karaoke (300k)" }); cbDV.SelectedIndex = 0;
            pnlTop.Controls.Add(cbDV);

            pnlTop.Controls.Add(new Label { Text = "Ngày sử dụng:", Location = new Point(15, 55), AutoSize = true });
            pnlTop.Controls.Add(new DateTimePicker { Location = new Point(120, 52), Width = 180, Format = DateTimePickerFormat.Short });

            pnlTop.Controls.Add(new Label { Text = "Số lượng:", Location = new Point(430, 55), AutoSize = true });
            NumericUpDown numSL = new NumericUpDown { Location = new Point(500, 52), Width = 100, Value = 1, Minimum = 1 };
            pnlTop.Controls.Add(numSL);

            Button btnGhi = new Button { Text = "Ghi nhận dịch vụ", Location = new Point(120, 92), Size = new Size(160, 32), BackColor = Color.FromArgb(25, 118, 210), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            pnlTop.Controls.Add(btnGhi);

            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgv.Columns.Add("MaPhieu", "Mã phiếu SDDV"); dgv.Columns.Add("Phong", "Số phòng"); dgv.Columns.Add("Ngay", "Ngày sử dụng"); dgv.Columns.Add("TenDV", "Tên dịch vụ"); dgv.Columns.Add("SL", "Số lượng"); dgv.Columns.Add("Gia", "Đơn giá"); dgv.Columns.Add("ThanhTien", "Thành tiền");
            dgv.Rows.Add("SD01", "A102", "02/11/2026", "Ăn sáng", "2", "120,000", "240,000");
            dgv.Rows.Add("SD02", "A102", "02/11/2026", "Karaoke", "1", "300,000", "300,000");

            this.Controls.Add(dgv);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);
        }
    }

    // =============================================
    // 5. FrmTraPhong (Đền bù, Hóa đơn, Thanh toán, Trả phòng)
    // =============================================
    public class FrmTraPhong : Form
    {
        public FrmTraPhong()
        {
            this.Text = "TRẢ PHÒNG VÀ THANH TOÁN";
            this.Size = new Size(920, 650);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTitle = new Label { Text = "TRẢ PHÒNG VÀ THANH TOÁN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(25, 60, 110), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleCenter };

            Panel pnlTop = new Panel { Dock = DockStyle.Top, Height = 190, Padding = new Padding(15) };
            pnlTop.Controls.Add(new Label { Text = "Phiếu đặt phòng:", Location = new Point(15, 15), AutoSize = true });
            ComboBox cbDat = new ComboBox { Location = new Point(130, 12), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            cbDat.Items.AddRange(new string[] { "PD001 - Nguyễn Văn Nam (Phòng A102)" }); cbDat.SelectedIndex = 0;
            pnlTop.Controls.Add(cbDat);

            pnlTop.Controls.Add(new Label { Text = "Số ngày tính tiền:", Location = new Point(440, 15), AutoSize = true });
            NumericUpDown numNgay = new NumericUpDown { Location = new Point(560, 12), Width = 100, Value = 3 };
            pnlTop.Controls.Add(numNgay);

            pnlTop.Controls.Add(new Label { Text = "Hình thức TT:", Location = new Point(15, 55), AutoSize = true });
            ComboBox cbHT = new ComboBox { Location = new Point(130, 52), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cbHT.Items.AddRange(new string[] { "Tiền mặt", "Chuyển khoản", "Thẻ", "Ví điện tử" }); cbHT.SelectedIndex = 0;
            pnlTop.Controls.Add(cbHT);

            pnlTop.Controls.Add(new Label { Text = "Số tiền thanh toán:", Location = new Point(330, 55), AutoSize = true });
            TextBox txtTien = new TextBox { Location = new Point(460, 52), Width = 200, Text = "2,940,000" };
            pnlTop.Controls.Add(txtTien);

            pnlTop.Controls.AddRange(new Control[] {
                new Button { Text = "Lập phiếu đền bù", Location = new Point(130, 105), Size = new Size(140, 35), BackColor = Color.FromArgb(211, 47, 47), ForeColor = Color.White, FlatStyle = FlatStyle.Flat },
                new Button { Text = "Lập Hóa đơn", Location = new Point(280, 105), Size = new Size(130, 35), BackColor = Color.FromArgb(25, 118, 210), ForeColor = Color.White, FlatStyle = FlatStyle.Flat },
                new Button { Text = "Ghi thanh toán", Location = new Point(420, 105), Size = new Size(130, 35), BackColor = Color.FromArgb(0, 150, 136), ForeColor = Color.White, FlatStyle = FlatStyle.Flat },
                new Button { Text = "Hoàn tất Trả phòng", Location = new Point(560, 105), Size = new Size(160, 35), BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat }
            });

            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgv.Columns.Add("SoHD", "Số Hóa đơn"); dgv.Columns.Add("Phieu", "Mã phiếu đặt"); dgv.Columns.Add("TienPhong", "Tiền phòng"); dgv.Columns.Add("TienDV", "Tiền dịch vụ"); dgv.Columns.Add("TongTien", "Tổng tiền"); dgv.Columns.Add("TT", "Trạng thái HD");
            dgv.Rows.Add("HD001", "PD001", "2,400,000", "540,000", "2,940,000", "Đã thanh toán");

            this.Controls.Add(dgv);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);
        }
    }

    // =============================================
    // 6. FrmThongKe (Thống kê báo cáo theo mốc thời gian)
    // =============================================
    public class FrmThongKe : Form
    {
        public FrmThongKe()
        {
            this.Text = "THỐNG KÊ BÁO CÁO";
            this.Size = new Size(880, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblTitle = new Label { Text = "THỐNG KÊ BÁO CÁO KHÁCH SẠN", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(25, 60, 110), Dock = DockStyle.Top, Height = 45, TextAlign = ContentAlignment.MiddleCenter };

            Panel pnlFilter = new Panel { Dock = DockStyle.Top, Height = 70, Padding = new Padding(15) };
            pnlFilter.Controls.Add(new Label { Text = "Từ ngày:", Location = new Point(20, 20), AutoSize = true });
            pnlFilter.Controls.Add(new DateTimePicker { Location = new Point(85, 17), Width = 150, Format = DateTimePickerFormat.Short });

            pnlFilter.Controls.Add(new Label { Text = "Đến ngày:", Location = new Point(260, 20), AutoSize = true });
            pnlFilter.Controls.Add(new DateTimePicker { Location = new Point(335, 17), Width = 150, Format = DateTimePickerFormat.Short });

            Button btnLoc = new Button { Text = "Xem thống kê", Location = new Point(510, 15), Size = new Size(130, 32), BackColor = Color.FromArgb(25, 118, 210), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            pnlFilter.Controls.Add(btnLoc);

            Panel pnlCards = new Panel { Dock = DockStyle.Top, Height = 90, Padding = new Padding(10) };
            pnlCards.Controls.Add(CreateCard("Tổng phiếu đặt", "15 Phiếu", Color.FromArgb(25, 118, 210), 20));
            pnlCards.Controls.Add(CreateCard("Doanh thu Hóa đơn", "45,800,000 VNĐ", Color.FromArgb(46, 125, 50), 300));
            pnlCards.Controls.Add(CreateCard("Tổng tiền Đền bù", "1,200,000 VNĐ", Color.FromArgb(211, 47, 47), 580));

            DataGridView dgv = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            dgv.Columns.Add("MaDV", "Mã dịch vụ"); dgv.Columns.Add("TenDV", "Tên dịch vụ"); dgv.Columns.Add("TongSL", "Tổng số lượng dùng"); dgv.Columns.Add("TongTien", "Tổng doanh thu dịch vụ");
            dgv.Rows.Add("DV01", "Ăn sáng", "45 suất", "5,400,000");
            dgv.Rows.Add("DV02", "Tắm hơi", "18 lượt", "4,500,000");
            dgv.Rows.Add("DV03", "Karaoke", "12 giờ", "3,600,000");

            this.Controls.Add(dgv);
            this.Controls.Add(pnlCards);
            this.Controls.Add(pnlFilter);
            this.Controls.Add(lblTitle);
        }

        private Panel CreateCard(string title, string val, Color color, int x)
        {
            Panel card = new Panel { Location = new Point(x, 5), Size = new Size(250, 75), BackColor = color };
            Label lbl1 = new Label { Text = title, ForeColor = Color.White, Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(10, 10), AutoSize = true };
            Label lbl2 = new Label { Text = val, ForeColor = Color.White, Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(10, 35), AutoSize = true };
            card.Controls.Add(lbl1); card.Controls.Add(lbl2);
            return card;
        }
    }
}