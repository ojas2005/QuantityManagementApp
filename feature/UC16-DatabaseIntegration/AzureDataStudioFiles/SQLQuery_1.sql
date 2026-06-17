-- Create database
CREATE DATABASE QuantityMeasurementDb;
GO

-- Use the database
USE QuantityMeasurementDb;
GO

-- Create table
CREATE TABLE QuantityMeasurements (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OperationType NVARCHAR(20) NOT NULL,
    FirstValue FLOAT NULL,
    FirstUnit NVARCHAR(10) NULL,
    FirstMeasurementType NVARCHAR(20) NULL,
    SecondValue FLOAT NULL,
    SecondUnit NVARCHAR(10) NULL,
    SecondMeasurementType NVARCHAR(20) NULL,
    ResultValue FLOAT NULL,
    ResultUnit NVARCHAR(10) NULL,
    ResultMeasurementType NVARCHAR(20) NULL,
    HasError BIT NOT NULL DEFAULT 0,
    ErrorMessage NVARCHAR(500) NULL,
    Timestamp DATETIME2 NOT NULL DEFAULT GETDATE()
);
GO

-- Create indexes for better performance
CREATE INDEX IX_QuantityMeasurements_OperationType ON QuantityMeasurements(OperationType);
CREATE INDEX IX_QuantityMeasurements_MeasurementType ON QuantityMeasurements(FirstMeasurementType);
CREATE INDEX IX_QuantityMeasurements_Timestamp ON QuantityMeasurements(Timestamp);
GO


---------

-- Check if database exists
SELECT name FROM sys.databases WHERE name = 'QuantityMeasurementDb';
GO

-- Use the database
USE QuantityMeasurementDb;
GO

-- Check if table exists
SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'QuantityMeasurements';
GO

-- Check table structure
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'QuantityMeasurements'
ORDER BY ORDINAL_POSITION;
GO

-- Check if __EFMigrationsHistory table exists (tracks migrations)
SELECT * FROM __EFMigrationsHistory;
GO

USE QuantityMeasurementDb;
GO

-- Find out which database you're actually connected to
SELECT 
    DB_NAME() AS CurrentDatabase,
    @@SERVERNAME AS ServerName,
    CURRENT_USER AS CurrentUser;
GO

-- List ALL databases on this server
SELECT name, database_id, create_date 
FROM sys.databases 
ORDER BY name;
GO


USE QuantityMeasurementDb;
GO

-- Check if the table exists
-- Check if any data exists at all
SELECT COUNT(*) AS TotalRecords FROM QuantityMeasurements;
GO

-- If records exist, show the most recent ones
IF (SELECT COUNT(*) FROM QuantityMeasurements) > 0
BEGIN
    SELECT TOP 10 
        Id,
        OperationType,
        FirstValue,
        FirstUnit,
        SecondValue,
        SecondUnit,
        ResultValue,
        ResultUnit,
        Timestamp
    FROM QuantityMeasurements
    ORDER BY Timestamp DESC;
END
ELSE
BEGIN
    PRINT 'No records found in QuantityMeasurements table';
END
GO


USE QuantityMeasurementDb;
SELECT * FROM QuantityMeasurements ORDER BY Timestamp DESC;