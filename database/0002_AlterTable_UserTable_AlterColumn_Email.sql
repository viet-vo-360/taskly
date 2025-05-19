-- liquibase formatted sql
-- changeset vohuyvu360:0002_UpdateColumn_Email
USE Taskly
GO

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Users')
BEGIN
	ALTER TABLE Users
	ALTER COLUMN Email NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS;
END
GO