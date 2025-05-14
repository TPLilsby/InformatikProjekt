CREATE DATABASE LagerSystemDB;
GO

USE LagerSystemDB;
GO

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    CategoryId INT NOT NULL,
    StockLimit INT NOT NULL,
	Price DECIMAL(10, 2) NOT NULL DEFAULT 0,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
	PasswordHash NVARCHAR(255) NOT NULL DEFAULT ''
);

CREATE TABLE [Role] (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL
);

CREATE TABLE UserRole (
    UserId INT,
    RoleId INT,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
);

CREATE TABLE OrderEntry (
    ProductId INT NOT NULL,
    UserId INT NOT NULL,
    Quantity INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    PRIMARY KEY (ProductId, UserId, OrderDate),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

CREATE TABLE Receipt (
    ReceiptId INT PRIMARY KEY IDENTITY(1,1),
    Date DATETIME NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

CREATE TABLE ReceiptProduct (
    ReceiptId INT,
    ProductId INT,
    PRIMARY KEY (ReceiptId, ProductId),
    FOREIGN KEY (ReceiptId) REFERENCES Receipt(ReceiptId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

INSERT INTO Role (RoleName)
VALUES ('Medarbejder'), ('Lagerchef');

INSERT INTO [User] (Name, PasswordHash)
VALUES ('testing', 'AQAAAAIAAYagAAAAEIhU6AFwcC/uS43X/32NQe4XzUWu93S1db7hLUzOgx9K45/Te6l59JHouyzwTdspSg==');


INSERT INTO UserRole (UserId, RoleId)
SELECT u.UserId, r.RoleId
FROM [User] u, Role r
WHERE u.Name = 'testing' AND r.RoleName = 'Lagerchef';

INSERT INTO Category (Name) VALUES ('Mejeri');
INSERT INTO Category (Name) VALUES ('Grøntsager');
INSERT INTO Category (Name) VALUES ('Frostvarer');
INSERT INTO Category (Name) VALUES ('Tøj');
