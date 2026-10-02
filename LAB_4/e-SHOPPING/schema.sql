-- 1. BẢNG KHÁCH HÀNG
CREATE TABLE IF NOT EXISTS Customers (
    CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT NOT NULL UNIQUE,
    FullName TEXT NOT NULL,
    IdentityCard TEXT NOT NULL UNIQUE,
    Address TEXT NOT NULL,
    Phone TEXT NOT NULL,
    Email TEXT
);

-- 2. BẢNG LOẠI PHIẾU ĐẶT HÀNG
CREATE TABLE IF NOT EXISTS OrderTypes (
    OrderTypeID TEXT PRIMARY KEY,
    TypeName TEXT NOT NULL,
    BaseProcessingTime TEXT NOT NULL,
    BaseFee REAL NOT NULL
);

-- 3. BẢNG ĐƠN HÀNG
CREATE TABLE IF NOT EXISTS Orders (
    OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerID INTEGER NOT NULL,
    OrderDate TEXT DEFAULT CURRENT_TIMESTAMP,
    OrderTypeID TEXT NOT NULL,
    ReceiverName TEXT NOT NULL,
    ReceiverAddress TEXT NOT NULL,
    ReceiverPhone TEXT NOT NULL,
    SubTotal REAL NOT NULL,
    ShippingFee REAL NOT NULL,
    TotalAmount REAL NOT NULL,
    CardType TEXT NOT NULL,
    MaskedCardNumber TEXT NOT NULL,
    PaymentStatus TEXT NOT NULL,
    PaymentTransactionID TEXT,
    OrderStatus TEXT NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (OrderTypeID) REFERENCES OrderTypes(OrderTypeID)
);

INSERT OR IGNORE INTO OrderTypes (OrderTypeID, TypeName, BaseProcessingTime, BaseFee) VALUES
('NORMAL', 'Giao hàng thường', '3-5 ngày', 20000),
('EXPRESS', 'Chuyển phát nhanh', '1-2 ngày', 40000),
('SAME_DAY', 'Chuyển phát nhanh trong ngày', 'Trong ngày', 80000);

INSERT OR IGNORE INTO Customers (CustomerID, Username, FullName, IdentityCard, Address, Phone, Email) VALUES
(1, 'nguyenvana', 'Nguyễn Văn A', '0123456789', '123 Lê Lợi, Q1, TP.HCM', '0901234567', 'nguyenvana@gmail.com');
(2, 'tranthib', 'Trần Thị B', '012345678902', '456 Nguyễn Huệ, Q.1, TP.HCM', '0912345678', 'tranthib@gmail.com'),
(3, 'levanc', 'Lê Văn C', '012345678903', '789 Cầu Giấy, Hà Nội', '0923456789', 'levanc@gmail.com'),
(4, 'phamdangd', 'Phạm Đăng D', '012345678904', '12 Trần Phú, Hải Phòng', '0934567890', 'phamdangd@gmail.com'),
(5, 'hoangthie', 'Hoàng Thị E', '012345678905', '99 Nguyễn Văn Linh, Đà Nẵng', '0945678901', 'hoangthie@gmail.com');

INSERT OR IGNORE INTO Orders 
(OrderID, CustomerID, OrderDate, OrderTypeID, ReceiverName, ReceiverAddress, ReceiverPhone, SubTotal, ShippingFee, TotalAmount, CardType, MaskedCardNumber, PaymentStatus, PaymentTransactionID, OrderStatus) VALUES
(1, 1, '2026-09-28 08:30:00', 'NORMAL', 'Nguyễn Văn A', '123 Lê Lợi, Q.1, TP.HCM', '0901234567', 450000, 20000, 470000, 'VISA', 'XXXX-XXXX-XXXX-4444', 'SUCCESS', 'PG-TXN-88A1B2C3', 'COMPLETED'),
(2, 2, '2026-09-29 10:15:00', 'EXPRESS', 'Trần Thị B', '456 Nguyễn Huệ, Q.1, TP.HCM', '0912345678', 1200000, 0, 1200000, 'MASTER', 'XXXX-XXXX-XXXX-8888', 'SUCCESS', 'PG-TXN-99D4E5F6', 'SHIPPED'),
(3, 3, '2026-09-30 14:20:00', 'SAME_DAY', 'Lê Văn C', '789 Cầu Giấy, Hà Nội', '0923456789', 5500000, 0, 5500000, 'AMEX', 'XXXX-XXXX-XXXX-1005', 'SUCCESS', 'PG-TXN-77X8Y9Z0', 'PENDING'),
(4, 4, '2026-10-01 16:45:00', 'EXPRESS', 'Phạm Đăng D', '12 Trần Phú, Hải Phòng', '0934567890', 800000, 40000, 840000, 'DISCOVER', 'XXXX-XXXX-XXXX-6543', 'SUCCESS', 'PG-TXN-55M4N3O2', 'PENDING'),
(5, 5, '2026-10-02 09:10:00', 'NORMAL', 'Hoàng Thị E', '99 Nguyễn Văn Linh, Đà Nẵng', '0945678901', 2500000, 0, 2500000, 'VISA', 'XXXX-XXXX-XXXX-1111', 'SUCCESS', 'PG-TXN-11A2B3C4', 'PROCESSING');