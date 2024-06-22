-- Create a new database (if it doesn't exist)
USE master;
GO
IF NOT EXISTS (
    SELECT [name]
    FROM sys.databases
    WHERE [name] = N'SOE3'
)
CREATE DATABASE SOE3;
GO

-- Create a new login if it doesn't exist
IF NOT EXISTS (
    SELECT name
    FROM sys.server_principals
    WHERE name = N'SOE3User'
)
BEGIN
    CREATE LOGIN SOE3User WITH PASSWORD = 'temp';
END
GO

-- Create a new user if it doesn't exist
USE SOE3;
GO
IF NOT EXISTS (
    SELECT name
    FROM sys.database_principals
    WHERE name = N'SOE3User'
)
BEGIN
    CREATE USER SOE3User FOR LOGIN SOE3User;
END
GO

-- Grant necessary permissions
ALTER ROLE db_owner ADD MEMBER SOE3User;
GO

USE SOE3;
GO

CREATE TABLE EncryptedTexts (
    Id INT PRIMARY KEY IDENTITY,
    EncryptText NVARCHAR(MAX),
    DecryptText NVARCHAR(MAX),
    KeyValue NVARCHAR(50),
    TimeStamp DATETIME
);