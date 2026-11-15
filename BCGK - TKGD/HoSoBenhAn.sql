CREATE TABLE HoSoBenhAn (
    MaHoSo INT IDENTITY(1,1) PRIMARY KEY,
    HoTenBenhNhan NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(20),
    ChanDoanXuatVien NVARCHAR(255),
    NgayXuatVien DATE,
    TrangThai NVARCHAR(255)
);