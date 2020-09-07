
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/25/2019 11:57:07
-- Generated from EDMX file: C:\Users\LENOVO\Desktop\DoAnWeb\DoAnWeb\DatModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QL_ShopDoChoiTreEm];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TINTUCs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TINTUCs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TINTUCs'
CREATE TABLE [dbo].[TINTUCs] (
    [MATC] int IDENTITY(1,1) NOT NULL,
    [TENTC] nvarchar(max)  NOT NULL,
    [MOTA] nvarchar(max)  NOT NULL,
    [HINHANH] nvarchar(max)  NOT NULL,
    [VT] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MATC] in table 'TINTUCs'
ALTER TABLE [dbo].[TINTUCs]
ADD CONSTRAINT [PK_TINTUCs]
    PRIMARY KEY CLUSTERED ([MATC] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------