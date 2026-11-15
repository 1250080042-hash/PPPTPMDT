CREATE OR ALTER PROCEDURE sp_TaoToaThuocNgoaiTru
    @MaHoSo INT,
    @LoiDan NVARCHAR(MAX),
    @TenThuoc NVARCHAR(255),
    @HamLuong NVARCHAR(100),
    @SoLuong INT,
    @CachDung NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- 1. Thêm vào bảng ToaThuoc (hoặc tạo mới nếu chưa có mã hồ sơ)
        -- Chèn lời dặn vào bảng ToaThuoc
        INSERT INTO ToaThuoc (MaHoSo, LoiDan)
        VALUES (@MaHoSo, @LoiDan);

        DECLARE @MaToaThuoc INT = SCOPE_IDENTITY();

        -- 2. Thêm chi tiết thuốc vào bảng ChiTietToaThuoc
        INSERT INTO ChiTietToaThuoc (MaToaThuoc, TenThuoc, HamLuong, SoLuong, CachDung)
        VALUES (@MaToaThuoc, @TenThuoc, @HamLuong, @SoLuong, @CachDung);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO