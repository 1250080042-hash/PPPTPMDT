-- Tạo Cơ sở dữ liệu
CREATE DATABASE QuanLySucKhoeSauXuatVien;
GO

USE QuanLySucKhoeSauXuatVien;
GO

CREATE TABLE BenhNhan (
    MaBenhNhan INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE NOT NULL,
    DiaChi NVARCHAR(255),
    CCCD VARCHAR(12) UNIQUE NOT NULL
);
GO

CREATE TABLE BacSi (
    MaBacSi INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    ChuyenKhoa NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL
);
GO

CREATE TABLE HoSoXuatVien (
    MaHoSo INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    NgayXuatVien DATETIME NOT NULL DEFAULT GETDATE(),
    ChanDoanRaVien NVARCHAR(MAX) NOT NULL,
    LoiDanCuaBacSi NVARCHAR(MAX),
    CONSTRAINT FK_HoSo_BenhNhan FOREIGN KEY (MaBenhNhan) 
        REFERENCES BenhNhan(MaBenhNhan) ON DELETE CASCADE
);
GO

CREATE TABLE ChiSoSucKhoe (
    MaChiSo INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo INT NOT NULL,
    NgayDo DATETIME NOT NULL DEFAULT GETDATE(),
    HuyetAp FLOAT CHECK (HuyetAp > 0),
    NhipTim INT CHECK (NhipTim > 0),
    SpO2 FLOAT CHECK (SpO2 >= 0 AND SpO2 <= 100),
    NhietDo FLOAT CHECK (NhietDo > 30 AND NhipTim < 45),
    MucDoCanhBao NVARCHAR(50) DEFAULT N'Bình thường',
    CONSTRAINT FK_ChiSo_HoSo FOREIGN KEY (MaHoSo) 
        REFERENCES HoSoXuatVien(MaHoSo) ON DELETE CASCADE
);
GO

CREATE TABLE LichTaiKham (
    MaLichHen INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    MaBacSi INT NOT NULL,
    NgayHen DATETIME NOT NULL,
    LyDoKham NVARCHAR(MAX) NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Chờ xác nhận',
    CONSTRAINT FK_LichHen_BenhNhan FOREIGN KEY (MaBenhNhan) 
        REFERENCES BenhNhan(MaBenhNhan),
    CONSTRAINT FK_LichHen_BacSi FOREIGN KEY (MaBacSi) 
        REFERENCES BacSi(MaBacSi),
    CONSTRAINT CK_TrangThaiLichHen CHECK (TrangThai IN (N'Chờ xác nhận', N'Đã xác nhận', N'Đang diễn ra', N'Đã hoàn thành', N'Đã hủy'))
);
GO

CREATE TABLE ToaThuoc (
    MaToaThuoc INT IDENTITY(1,1) PRIMARY KEY,
    MaHoSo INT NOT NULL,
    NgayKe DATETIME NOT NULL DEFAULT GETDATE(),
    LoiDan NVARCHAR(MAX),
    CONSTRAINT FK_ToaThuoc_HoSo FOREIGN KEY (MaHoSo) 
        REFERENCES HoSoXuatVien(MaHoSo) ON DELETE CASCADE
);
GO

CREATE TABLE ChiTietToaThuoc (
    MaChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    MaToaThuoc INT NOT NULL,
    TenThuoc NVARCHAR(150) NOT NULL,
    HamLuong NVARCHAR(50),
    SoLuong INT CHECK (SoLuong > 0),
    CachDung NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_ChiTiet_ToaThuoc FOREIGN KEY (MaToaThuoc) 
        REFERENCES ToaThuoc(MaToaThuoc) ON DELETE CASCADE
);
GO

CREATE PROCEDURE sp_TaoToaThuocNgoaiTru
    @MaHoSo INT,
    @LoiDan NVARCHAR(MAX),
    @TenThuoc NVARCHAR(150),
    @HamLuong NVARCHAR(50),
    @SoLuong INT,
    @CachDung NVARCHAR(255)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO ToaThuoc (MaHoSo, NgayKe, LoiDan)
        VALUES (@MaHoSo, GETDATE(), @LoiDan);

        DECLARE @NewMaToaThuoc INT = SCOPE_IDENTITY();

        INSERT INTO ChiTietToaThuoc (MaToaThuoc, TenThuoc, HamLuong, SoLuong, CachDung)
        VALUES (@NewMaToaThuoc, @TenThuoc, @HamLuong, @SoLuong, @CachDung);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END;
GO