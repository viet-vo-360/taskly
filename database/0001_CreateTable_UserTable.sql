CREATE DATABASE Taskly
USE Taskly
GO

CREATE TABLE Users(
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50) NULL,
    Password NVARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    PhoneNumber NVARCHAR(20) NULL UNIQUE,
    CreatedAt DATETIME2(0) DEFAULT SYSDATETIME() NOT NULL,
    UpdatedAt DATETIME2(0) DEFAULT SYSDATETIME() NOT NULL,
    IsActive BIT DEFAULT 1 NOT NULL,
    EmailVerified BIT DEFAULT 0 NOT NULL
);
GO


