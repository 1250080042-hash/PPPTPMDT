CREATE TABLE ChiSoSinhTon (
    MaChiSo INT IDENTITY(1,1) PRIMARY KEY,
    MaBenhNhan INT NOT NULL,
    HuyetAp NVARCHAR(50) NOT NULL, -- Ví dụ: 120/80
    NhipTim INT NOT NULL,          -- Nhịp tim (lần/phút)
    SpO2 INT NOT NULL,             -- Nồng độ oxy (%)
    NhietDo FLOAT NOT NULL,        -- Nhiệt độ cơ thể (°C)
    ThoiGianDo DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) DEFAULT N'Bình thường', -- Bình thường / Cảnh báo bất thường
    FOREIGN KEY (MaBenhNhan) REFERENCES BenhNhan(MaBenhNhan)
);