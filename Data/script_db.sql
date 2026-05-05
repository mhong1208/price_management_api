-- Create Database Script for Price Management System
-- Database: MySQL

-- Create Items Table
CREATE TABLE `Items` (
    `Id` CHAR(36) NOT NULL PRIMARY KEY,
    `ItemCode` VARCHAR(50) NOT NULL,
    `ItemName` VARCHAR(255) NOT NULL,
    `Description` TEXT,
    `Unit` VARCHAR(50),
    `Category` VARCHAR(100),
    `Status` INT NOT NULL DEFAULT 1, -- 1: Active, 0: Inactive
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt` DATETIME,
    `CreatedBy` VARCHAR(255),
    `UpdatedBy` VARCHAR(255),
    UNIQUE INDEX `IX_Items_ItemCode` (`ItemCode`)
);

-- Create Suppliers Table
CREATE TABLE `Suppliers` (
    `Id` CHAR(36) NOT NULL PRIMARY KEY,
    `SupplierCode` VARCHAR(50) NOT NULL,
    `SupplierName` VARCHAR(255) NOT NULL,
    `ContactPerson` VARCHAR(255),
    `Email` VARCHAR(255),
    `Phone` VARCHAR(50),
    `Address` TEXT,
    `TaxCode` VARCHAR(50),
    `Description` TEXT,
    `Status` INT NOT NULL DEFAULT 1,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt` DATETIME,
    `CreatedBy` VARCHAR(255),
    `UpdatedBy` VARCHAR(255),
    UNIQUE INDEX `IX_Suppliers_SupplierCode` (`SupplierCode`)
);

-- Create ItemPrices Table
CREATE TABLE `ItemPrices` (
    `Id` CHAR(36) NOT NULL PRIMARY KEY,
    `ItemId` CHAR(36) NOT NULL,
    `SupplierId` CHAR(36) NOT NULL,
    `Price` DECIMAL(18, 2) NOT NULL,
    `Currency` VARCHAR(10) NOT NULL,
    `EffectiveDate` DATETIME NOT NULL,
    `Notes` TEXT,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt` DATETIME,
    `CreatedBy` VARCHAR(255),
    `UpdatedBy` VARCHAR(255),
    CONSTRAINT `FK_ItemPrices_Items` FOREIGN KEY (`ItemId`) REFERENCES `Items` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ItemPrices_Suppliers` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`Id`) ON DELETE CASCADE
);

-- Create ItemPriceHistories Table
CREATE TABLE `ItemPriceHistories` (
    `Id` CHAR(36) NOT NULL PRIMARY KEY,
    `ItemId` CHAR(36) NOT NULL,
    `SupplierId` CHAR(36) NOT NULL,
    `OldPrice` DECIMAL(18, 2) NOT NULL,
    `NewPrice` DECIMAL(18, 2) NOT NULL,
    `Currency` VARCHAR(10) NOT NULL,
    `EffectiveDate` DATETIME NOT NULL,
    `Action` VARCHAR(50), -- CREATE, UPDATE, DELETE
    `Notes` TEXT,
    `CreatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt` DATETIME,
    `CreatedBy` VARCHAR(255),
    `UpdatedBy` VARCHAR(255),
    CONSTRAINT `FK_ItemPriceHistories_Items` FOREIGN KEY (`ItemId`) REFERENCES `Items` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ItemPriceHistories_Suppliers` FOREIGN KEY (`SupplierId`) REFERENCES `Suppliers` (`Id`) ON DELETE CASCADE
);
