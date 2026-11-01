using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.Forms
{
    public class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Quản lý khách sạn";
            this.Size = new Size(700, 430);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(240, 240, 240);

            Panel pnlCard = new Panel
            {
                Size = new Size(620, 330),
                Location = new Point(35, 25),
                BackColor = Color.White
            };

            Label lblTitle = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 60, 110),
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnDanhMuc = CreateMenuBtn("Danh mục", 40, 80);
            Button btnPhong = CreateMenuBtn("Phòng - Tiện nghi", 230, 80);
            Button btnDatPhong = CreateMenuBtn("Đặt / Nhận phòng", 420, 80);
            Button btnDichVu = CreateMenuBtn("Sử dụng dịch vụ", 40, 150);
            Button btnTraPhong = CreateMenuBtn("Trả phòng - Thanh toán", 230, 150);
            Button btnThongKe = CreateMenuBtn("Thống kê", 420, 150);
            
            Button btnThoat = new Button
            {
                Text = "Thoát",
                Location = new Point(230, 230),
                Size = new Size(160, 45),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(211, 47, 47),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnDanhMuc.Click += (s, e) => { using (var f = new FrmDanhMuc()) f.ShowDialog(this); };
            btnPhong.Click += (s, e) => { using (var f = new FrmPhongTienNghi()) f.ShowDialog(this); };
            btnDatPhong.Click += (s, e) => { using (var f = new FrmDatPhong()) f.ShowDialog(this); };
            btnDichVu.Click += (s, e) => { using (var f = new FrmDichVu()) f.ShowDialog(this); };
            btnTraPhong.Click += (s, e) => { using (var f = new FrmTraPhong()) f.ShowDialog(this); };
            btnThongKe.Click += (s, e) => { using (var f = new FrmThongKe()) f.ShowDialog(this); };
            btnThoat.Click += (s, e) => {
                if (MessageBox.Show("Bạn có thực sự muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    this.Close();
            };

            pnlCard.Controls.AddRange(new Control[] { lblTitle, btnDanhMuc, btnPhong, btnDatPhong, btnDichVu, btnTraPhong, btnThongKe, btnThoat });
            this.Controls.Add(pnlCard);
        }

        private Button CreateMenuBtn(string text, int x, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(160, 50),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                BackColor = Color.FromArgb(225, 235, 245),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
        }
    }
}