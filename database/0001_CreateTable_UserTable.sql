IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Taskly')
BEGIN
    CREATE DATABASE Taskly;
END
GO

USE Taskly
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        FirstName NVARCHAR(255) NOT NULL,
        LastName NVARCHAR(255) NOT NULL,
        MiddleName NVARCHAR(255) NULL,
        Password VARCHAR(60) NOT NULL,
        DOB DATE NOT NULL,
        PhoneNumber NVARCHAR(20) NULL UNIQUE,
        CreatedAt DATETIME2(0) DEFAULT SYSDATETIME() NOT NULL,
        UpdatedAt DATETIME2(0) DEFAULT SYSDATETIME() NOT NULL,
        IsActive BIT DEFAULT 1 NOT NULL,
        EmailVerified BIT DEFAULT 0 NOT NULL
    );
END
GO






