-- 1. Bảng lưu cảnh báo bất thường từ sinh hiệu bệnh nhân
CREATE TABLE CanhBaoBatThuong (
    MaCanhBao INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo INT,
    NoiDungCanhBao NVARCHAR(255),
    MucDo NVARCHAR(50), -- Ví dụ: 'Khẩn cấp', 'Cảnh báo'
    NgayTao DATETIME DEFAULT GETDATE(),
    TrangThaiXuLy BIT DEFAULT 0 -- 0: Chưa xử lý, 1: Đã xử lý
);

-- 2. Bảng lưu lịch hẹn tái khám
CREATE TABLE LichHenTaiKham (
    MaLichHen INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo INT,
    NgayHen DATETIME,
    GhiChu NVARCHAR(255),
    TrangThai NVARCHAR(50) DEFAULT 'Chờ tái khám'
);