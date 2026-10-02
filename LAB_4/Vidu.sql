CREATE DATABASE ATM_DB;
USE ATM_DB;

-- Bảng A: Khách hàng
CREATE TABLE KhachHang (
    maKhachHang VARCHAR(10) PRIMARY KEY,
    hoTenKhachHang NVARCHAR(100) NOT NULL,
    soThe VARCHAR(20) UNIQUE NOT NULL,
    maPIN VARCHAR(10) NOT NULL
);

-- Bảng B: Tài khoản (Chứa khóa ngoại maKhachHang - Mô hình 1-N)
CREATE TABLE TaiKhoan (
    soTaiKhoan VARCHAR(20) PRIMARY KEY,
    loaiTaiKhoan NVARCHAR(50) NOT NULL,
    soDuTaiKhoan DECIMAL(18,2) NOT NULL DEFAULT 0.00,
    maKhachHang VARCHAR(10) NOT NULL,
    FOREIGN KEY (maKhachHang) REFERENCES KhachHang(maKhachHang)
);

-- Dữ liệu mẫu
INSERT INTO KhachHang VALUES ('KH01', N'Nguyễn Văn A', '123456789', '1234');
INSERT INTO TaiKhoan VALUES ('TK101', N'Thanh toán', 5000000.00, 'KH01');
INSERT INTO TaiKhoan VALUES ('TK102', N'Tiết kiệm', 15000000.00, 'KH01');
