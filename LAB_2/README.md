# LAB 2: HỆ THỐNG QUẢN LÝ THƯ VIỆN (OOSD)

> **Họ và tên:** [Điền Họ tên của bạn]  
> **MSSV:** [Điền Mã số sinh viên của bạn]  
> **Lớp:** [Điền Mã lớp của bạn]  
> **Môn học:** Object-Oriented Software Design (OOSD)  
> **Công nghệ sử dụng:** C# WinForms (.NET Framework 4.7.2), SQL Server LocalDB / ADO.NET

---

## 1. MỤC TIÊU VÀ SẢN PHẨM ĐẦU RA

### Mục tiêu
- Đi trọn chu trình từ khảo sát yêu cầu nghiệp vụ, mô hình hóa UML, thiết kế CSDL, thiết kế giao diện đến cài đặt ứng dụng Desktop WinForms hoàn chỉnh có giao tiếp cơ sở dữ liệu.
- Đảm bảo toàn bộ các quy tắc nghiệp vụ (Business Rules - BR) được kiểm soát chặt chẽ ở tầng **Service / Business Logic** lẫn tầng **Database Constraints**.

### Sản phẩm trong thư mục LAB2
1. `README.md`: Báo cáo chi tiết trọn gói bài Lab 2.
2. `Database/QuanLyThuVien.sql`: Script SQL khởi tạo CSDL, các ràng buộc và dữ liệu thử nghiệm.
3. `QuanLyThuVien/`: Solution C# WinForms (.NET Framework 4.7.2) xử lý nghiệp vụ mượn - trả - phạt - thống kê.

---

## 2. MÔ TẢ BÀI TOÁN & KHẢO SÁT NGHIỆP VỤ

### 2.1. Bảng Khảo sát Đầu vào - Xử lý - Kết xuất


| Nghiệp vụ | Thông tin đầu vào | Quá trình xử lý | Thông tin kết xuất |
| :--- | :--- | :--- | :--- |
| **Cấp / Gia hạn thẻ** | Thông tin độc giả, ảnh 3x4, ngày cấp, hạn sử dụng, tình trạng lệ phí | Kiểm tra dữ liệu; bảo đảm độc giả chỉ có 01 thẻ còn giá trị hoạt động tại một thời điểm | Thông tin thẻ mới / Thông báo từ chối |
| **Mượn sách** | Thẻ thư viện, mã độc giả, danh sách đầu sách (1–3 cuốn), ngày mượn, ngày hẹn trả | Kiểm tra thẻ (còn hạn, đã đóng lệ phí), kiểm tra nợ quá hạn, kiểm tra tổng số sách mượn ≤ 3, kiểm tra tồn kho và chặn trùng đầu sách trong phiếu | Lập phiếu mượn, chi tiết phiếu mượn và tự động giảm số lượng hiện có |
| **Trả sách** | Mã chi tiết mượn, ngày trả thực tế, tình trạng sách (Bình thường / Rách / Hư / Mất), phí phạt (nếu có) | So sánh ngày hẹn trả và ngày trả; kiểm tra tình trạng sách hư/mất; cập nhật trạng thái; khôi phục tồn kho (nếu sách bình thường) | Ghi nhận trả; tự động lập phiếu phạt nếu vi phạm |
| **Quản lý đầu sách** | Mã sách, tên sách, năm XB, số lượng hiện có, thể loại, nhà xuất bản | Thêm / Sửa / Xóa / Tìm kiếm; kiểm tra ràng buộc khóa ngoại FK và số lượng hiện có ≥ 0 | Danh sách đầu sách được cập nhật |
| **Thống kê** | Khoảng thời gian (Từ ngày ... Đến ngày ...) | Tổng hợp số lượt mượn, quá hạn, mất, hư hỏng và tổng tiền phí phạt thu được | KPI tổng hợp và danh sách phiếu phạt chi tiết |

---

### 2.2. Quy định nghiệp vụ (Business Rules - BR)


- **BR01:** Độc giả phải có thẻ thư viện để mượn sách.
- **BR02:** Tại một thời điểm, độc giả chỉ có đúng 01 thẻ thư viện có giá trị sử dụng (`TrangThai = 1`).
- **BR03:** Thẻ quá hạn hoặc chưa đóng lệ phí năm thì không có giá trị.
- **BR04:** Một độc giả được mượn tối đa 3 cuốn sách khác nhau về nhà.
- **BR05:** Nếu độc giả còn sách quá hạn chưa trả thì không được mượn thêm.
- **BR06:** Trong một phiếu mượn không được chọn 2 cuốn cùng một đầu sách.
- **BR07:** Chỉ cho mượn khi đầu sách còn số lượng hiện có trong kho (`SoLuongHienCo > 0`).
- **BR08:** Ngày hẹn trả không được trước ngày mượn (`NgayHenTra >= NgayMuon`).
- **BR09:** Trả trễ hạn, làm mất hoặc làm rách/hư hỏng sách phải lập phiếu phạt.
- **BR10:** Phiếu phạt phải ghi nhận rõ ngày phạt, lý do phạt, phí cụ thể từng cuốn và nhân viên lập.
- **BR11:** Hệ thống hỗ trợ thống kê định kỳ số lượng mượn, mất, hư hỏng, quá hạn và thu phí phạt.
- **BR12:** Một đầu sách thuộc đúng 01 thể loại (`1-N`).
- **BR13:** Một đầu sách gắn với 01 nhà xuất bản (`1-N`).

---

## 3. THIẾT KẾ CƠ SỞ DỮ LIỆU (SQL SERVER)

### 3.1. Danh sách các bảng


1. `NhanVien`: Quản lý thông tin nhân viên (Thủ thư, NV Quản lý sách).
2. `TheLoai`: Danh mục thể loại sách.
3. `NhaXuatBan`: Thông tin nhà xuất bản (Địa chỉ, Điện thoại).
4. `DauSach`: Thông tin các đầu sách, năm XB, số lượng tồn kho.
5. `DocGia`: Thông tin cá nhân độc giả và ảnh 3x4.
6. `TheDocGia`: Lịch sử cấp/gia hạn thẻ, hạn sử dụng, tình trạng đóng lệ phí.
7. `PhieuMuon`: Ghi nhận thông tin phiếu mượn (Độc giả, Nhân viên lập, Ngày mượn, Ngày hẹn trả).
8. `ChiTietPhieuMuon`: Mỗi dòng lưu 1 đầu sách mượn, ngày trả thực tế và tình trạng khi trả.
9. `PhieuPhat`: Ghi nhận chi tiết phiếu phạt khi vi phạm quy định mượn trả.

---

### 3.2. Script SQL khởi tạo CSDL (`Database/QuanLyThuVien.sql`)


```sql
IF DB_ID(N'QuanLyThuVienDB') IS NULL 
    CREATE DATABASE QuanLyThuVienDB; 
GO 
USE QuanLyThuVienDB; 
GO 

IF OBJECT_ID('dbo.PhieuPhat','U') IS NOT NULL DROP TABLE dbo.PhieuPhat; 
IF OBJECT_ID('dbo.ChiTietPhieuMuon','U') IS NOT NULL DROP TABLE dbo.ChiTietPhieuMuon; 
IF OBJECT_ID('dbo.PhieuMuon','U') IS NOT NULL DROP TABLE dbo.PhieuMuon; 
IF OBJECT_ID('dbo.TheDocGia','U') IS NOT NULL DROP TABLE dbo.TheDocGia; 
IF OBJECT_ID('dbo.DocGia','U') IS NOT NULL DROP TABLE dbo.DocGia; 
IF OBJECT_ID('dbo.DauSach','U') IS NOT NULL DROP TABLE dbo.DauSach; 
IF OBJECT_ID('dbo.NhaXuatBan','U') IS NOT NULL DROP TABLE dbo.NhaXuatBan; 
IF OBJECT_ID('dbo.TheLoai','U') IS NOT NULL DROP TABLE dbo.TheLoai; 
IF OBJECT_ID('dbo.NhanVien','U') IS NOT NULL DROP TABLE dbo.NhanVien; 
GO 

CREATE TABLE dbo.NhanVien ( 
    MaNhanVien NVARCHAR(20) NOT NULL PRIMARY KEY, 
    Ho NVARCHAR(50) NOT NULL, 
    Ten NVARCHAR(50) NOT NULL, 
    Phai NVARCHAR(10) NOT NULL, 
    NgaySinh DATE NOT NULL, 
    ChucVu NVARCHAR(80) NOT NULL, 
    SoDienThoai NVARCHAR(20) NULL 
); 

CREATE TABLE dbo.TheLoai ( 
    MaTheLoai NVARCHAR(20) NOT NULL PRIMARY KEY, 
    TenTheLoai NVARCHAR(100) NOT NULL UNIQUE 
); 

CREATE TABLE dbo.NhaXuatBan ( 
    MaNhaXuatBan NVARCHAR(20) NOT NULL PRIMARY KEY, 
    DiaChi NVARCHAR(250) NULL, 
    SoDienThoai NVARCHAR(20) NULL 
); 

CREATE TABLE dbo.DauSach ( 
    MaDauSach NVARCHAR(20) NOT NULL PRIMARY KEY, 
    TenSach NVARCHAR(200) NOT NULL, 
    NamXuatBan INT NOT NULL, 
    SoLuongHienCo INT NOT NULL CONSTRAINT CK_DauSach_SoLuong CHECK (SoLuongHienCo >= 0), 
    MaTheLoai NVARCHAR(20) NOT NULL, 
    MaNhaXuatBan NVARCHAR(20) NOT NULL, 
    CONSTRAINT FK_DauSach_TheLoai FOREIGN KEY (MaTheLoai) REFERENCES dbo.TheLoai(MaTheLoai), 
    CONSTRAINT FK_DauSach_NXB FOREIGN KEY (MaNhaXuatBan) REFERENCES dbo.NhaXuatBan(MaNhaXuatBan) 
); 

CREATE TABLE dbo.DocGia ( 
    MaDocGia NVARCHAR(20) NOT NULL PRIMARY KEY, 
    Ho NVARCHAR(50) NOT NULL, 
    Ten NVARCHAR(50) NOT NULL, 
    NgaySinh DATE NOT NULL, 
    Phai NVARCHAR(10) NOT NULL, 
    SoDienThoai NVARCHAR(20) NULL, 
    DiaChi NVARCHAR(250) NOT NULL, 
    Email NVARCHAR(150) NOT NULL, 
    Anh3x4 NVARCHAR(260) NULL 
); 

CREATE TABLE dbo.TheDocGia ( 
    MaThe NVARCHAR(30) NOT NULL PRIMARY KEY, 
    MaDocGia NVARCHAR(20) NOT NULL, 
    NgayCap DATE NOT NULL, 
    HanSuDung DATE NOT NULL, 
    DaDongLePhi BIT NOT NULL, 
    TrangThai BIT NOT NULL CONSTRAINT DF_TheDocGia_TrangThai DEFAULT(1), 
    CONSTRAINT CK_TheDocGia_Han CHECK (HanSuDung >= NgayCap), 
    CONSTRAINT FK_TheDocGia_DocGia FOREIGN KEY (MaDocGia) REFERENCES dbo.DocGia(MaDocGia) 
); 

-- Ràng buộc chỉ có 1 thẻ hoạt động tại 1 thời điểm cho mỗi độc giả
CREATE UNIQUE INDEX UX_TheDocGia_MotTheHoatDong 
    ON dbo.TheDocGia(MaDocGia) WHERE TrangThai = 1; 

CREATE TABLE dbo.PhieuMuon ( 
    MaPhieuMuon NVARCHAR(30) NOT NULL PRIMARY KEY, 
    MaDocGia NVARCHAR(20) NOT NULL, 
    MaNhanVien NVARCHAR(20) NOT NULL, 
    NgayMuon DATE NOT NULL, 
    NgayHenTra DATE NOT NULL, 
    CONSTRAINT CK_PhieuMuon_Ngay CHECK (NgayHenTra >= NgayMuon), 
    CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia) REFERENCES dbo.DocGia(MaDocGia), 
    CONSTRAINT FK_PhieuMuon_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES dbo.NhanVien(MaNhanVien) 
); 

CREATE TABLE dbo.ChiTietPhieuMuon ( 
    MaChiTiet NVARCHAR(35) NOT NULL PRIMARY KEY, 
    MaPhieuMuon NVARCHAR(30) NOT NULL, 
    MaDauSach NVARCHAR(20) NOT NULL, 
    NgayTraThucTe DATE NULL, 
    TinhTrangTra NVARCHAR(50) NULL, 
    CONSTRAINT UQ_CTPM_Phieu_DauSach UNIQUE (MaPhieuMuon, MaDauSach), 
    CONSTRAINT FK_CTPM_PhieuMuon FOREIGN KEY (MaPhieuMuon) REFERENCES dbo.PhieuMuon(MaPhieuMuon), 
    CONSTRAINT FK_CTPM_DauSach FOREIGN KEY (MaDauSach) REFERENCES dbo.DauSach(MaDauSach) 
); 

CREATE TABLE dbo.PhieuPhat ( 
    MaPhieuPhat NVARCHAR(35) NOT NULL PRIMARY KEY, 
    MaChiTiet NVARCHAR(35) NOT NULL, 
    MaNhanVien NVARCHAR(20) NOT NULL, 
    NgayPhat DATE NOT NULL, 
    LyDo NVARCHAR(250) NOT NULL, 
    PhiPhat DECIMAL(18,0) NOT NULL CONSTRAINT CK_PhieuPhat_Phi CHECK (PhiPhat >= 0), 
    CONSTRAINT FK_PhieuPhat_CTPM FOREIGN KEY (MaChiTiet) REFERENCES dbo.ChiTietPhieuMuon(MaChiTiet), 
    CONSTRAINT FK_PhieuPhat_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES dbo.NhanVien(MaNhanVien) 
); 
GO 

-- DỮ LIỆU MẪU BAN ĐẦU
INSERT INTO dbo.NhanVien(MaNhanVien,Ho,Ten,Phai,NgaySinh,ChucVu,SoDienThoai) VALUES 
(N'NV001',N'Nguyễn',N'An',N'Nam','1990-02-15',N'Thủ thư',N'0901000001'), 
(N'NV002',N'Trần',N'Bình',N'Nữ','1992-08-20',N'Nhân viên quản lý sách',N'0901000002'); 

INSERT INTO dbo.TheLoai(MaTheLoai,TenTheLoai) VALUES 
(N'TL001',N'Tin học'),(N'TL002',N'Tiểu thuyết'),(N me'TL003',N'Anh văn'),(N'TL004',N'Truyện ngắn'); 

INSERT INTO dbo.NhaXuatBan(MaNhaXuatBan,DiaChi,SoDienThoai) VALUES 
(N'NXB001',N'Quận 1, TP.HCM',N'0283000001'), 
(N'NXB002',N'Quận Cầu Giấy, Hà Nội',N'0243000002'); 

INSERT INTO dbo.DauSach(MaDauSach,TenSach,NamXuatBan,SoLuongHienCo,MaTheLoai,MaNhaXuatBan) VALUES 
(N'S001',N'Lập trình C# căn bản',2025,5,N'TL001',N'NXB001'), 
(N'S002',N'Cơ sở dữ liệu',2024,4,N'TL001',N'NXB001'), 
(N'S003',N'Mạng máy tính',2023,3,N'TL001',N'NXB002'), 
(N'S004',N'Tiếng Anh chuyên ngành',2024,2,N'TL003',N'NXB002'); 

INSERT INTO dbo.DocGia(MaDocGia,Ho,Ten,NgaySinh,Phai,SoDienThoai,DiaChi,Email,Anh3x4) VALUES 
(N'DG001',N'Lê',N'Minh','2003-05-12',N'Nam',N'0911000001',N'TP.HCM',N'minh@example.com',NULL), 
(N'DG002',N'Phạm',N'Lan','2002-10-23',N'Nữ',N'0911000002',N'TP.HCM',N'lan@example.com',NULL); 

INSERT INTO dbo.TheDocGia(MaThe,MaDocGia,NgayCap,HanSuDung,DaDongLePhi,TrangThai) VALUES 
(N'THE_DG001_2026',N'DG001','2026-01-01','2026-12-31',1,1), 
(N'THE_DG002_2026',N'DG002','2026-01-01','2026-12-31',1,1); 
GO