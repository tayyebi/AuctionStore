
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2014 22:05:49
-- Generated from EDMX file: D:\Projects\Takhfif\Takhfif\Takhfif\Models\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CityProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_CityProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_GroupProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_ProductImage];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_ProductLike];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_ProductOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_ProductComment];
GO
IF OBJECT_ID(N'[dbo].[FK_AdminProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_AdminProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_UserLike];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_UserOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTransaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transactions] DROP CONSTRAINT [FK_UserTransaction];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTransfer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transfers] DROP CONSTRAINT [FK_UserTransfer];
GO
IF OBJECT_ID(N'[dbo].[FK_AdminSlide]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Slides] DROP CONSTRAINT [FK_AdminSlide];
GO
IF OBJECT_ID(N'[dbo].[FK_AdminUpload]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Uploads] DROP CONSTRAINT [FK_AdminUpload];
GO
IF OBJECT_ID(N'[dbo].[FK_AdminPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_AdminPost];
GO
IF OBJECT_ID(N'[dbo].[FK_AdminFinilization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Finilizations] DROP CONSTRAINT [FK_AdminFinilization];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductFinilization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Finilizations] DROP CONSTRAINT [FK_ProductFinilization];
GO
IF OBJECT_ID(N'[dbo].[FK_UserFinilization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Finilizations] DROP CONSTRAINT [FK_UserFinilization];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Likes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Likes];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO
IF OBJECT_ID(N'[dbo].[Transfers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transfers];
GO
IF OBJECT_ID(N'[dbo].[InMails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InMails];
GO
IF OBJECT_ID(N'[dbo].[Admins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admins];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Slides]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slides];
GO
IF OBJECT_ID(N'[dbo].[Uploads]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Uploads];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Finilizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Finilizations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Date_Create] nvarchar(max)  NOT NULL,
    [Date_Expire] nvarchar(max)  NOT NULL,
    [Price_Original] int  NOT NULL,
    [Price_Off] int  NOT NULL,
    [Price_Off_Percent] int  NOT NULL,
    [Thumbnail] varbinary(max)  NOT NULL,
    [Count_Like] int  NOT NULL,
    [Count_Buy] int  NOT NULL,
    [Priority] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CityId] uniqueidentifier  NOT NULL,
    [GroupId] uniqueidentifier  NOT NULL,
    [Map] nvarchar(max)  NOT NULL,
    [AdminUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Likes'
CREATE TABLE [dbo].[Likes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserUsername] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Username] nvarchar(max)  NOT NULL,
    [Gender] bit  NULL,
    [Birth_Year] int  NULL,
    [Birth_Month] int  NULL,
    [Rate] int  NULL,
    [Bank] int  NULL,
    [IsBlocked] bit  NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date_Buy] nvarchar(max)  NOT NULL,
    [Date_Expire] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL,
    [UserUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [Source] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Direction] bit  NOT NULL,
    [Value] int  NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [UserUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Transfers'
CREATE TABLE [dbo].[Transfers] (
    [Id] uniqueidentifier  NOT NULL,
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Refer] nvarchar(max)  NOT NULL,
    [BranchCode] nvarchar(max)  NOT NULL,
    [Value] int  NOT NULL,
    [Date_Year] int  NOT NULL,
    [Date_Month] int  NOT NULL,
    [Date_Day] int  NOT NULL,
    [IsConfirmed] bit  NULL,
    [UserUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'InMails'
CREATE TABLE [dbo].[InMails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ToUsername] nvarchar(max)  NOT NULL,
    [FromUsername] nvarchar(max)  NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [IsRead] bit  NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [Username] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [About] nvarchar(max)  NOT NULL,
    [HTML] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL,
    [UserUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Slides'
CREATE TABLE [dbo].[Slides] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Source] nvarchar(max)  NOT NULL,
    [NavigateUrl] nvarchar(max)  NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [AdminUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Uploads'
CREATE TABLE [dbo].[Uploads] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Lenght] int  NOT NULL,
    [Bytes] varbinary(max)  NULL,
    [Date] nvarchar(max)  NOT NULL,
    [AdminUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Abstract] nvarchar(max)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [AdminUsername] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Finilizations'
CREATE TABLE [dbo].[Finilizations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [AdminUsername] nvarchar(max)  NOT NULL,
    [ProductId] int  NOT NULL,
    [UserUsername] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [PK_Likes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Username] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transfers'
ALTER TABLE [dbo].[Transfers]
ADD CONSTRAINT [PK_Transfers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InMails'
ALTER TABLE [dbo].[InMails]
ADD CONSTRAINT [PK_InMails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Username] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [Id] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Slides'
ALTER TABLE [dbo].[Slides]
ADD CONSTRAINT [PK_Slides]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Uploads'
ALTER TABLE [dbo].[Uploads]
ADD CONSTRAINT [PK_Uploads]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Finilizations'
ALTER TABLE [dbo].[Finilizations]
ADD CONSTRAINT [PK_Finilizations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CityId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_CityProduct]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityProduct'
CREATE INDEX [IX_FK_CityProduct]
ON [dbo].[Products]
    ([CityId]);
GO

-- Creating foreign key on [GroupId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_GroupProduct]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupProduct'
CREATE INDEX [IX_FK_GroupProduct]
ON [dbo].[Products]
    ([GroupId]);
GO

-- Creating foreign key on [ProductId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_ProductImage]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductImage'
CREATE INDEX [IX_FK_ProductImage]
ON [dbo].[Images]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_ProductLike]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductLike'
CREATE INDEX [IX_FK_ProductLike]
ON [dbo].[Likes]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_ProductOrder]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrder'
CREATE INDEX [IX_FK_ProductOrder]
ON [dbo].[Orders]
    ([ProductId]);
GO

-- Creating foreign key on [ProductId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_ProductComment]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductComment'
CREATE INDEX [IX_FK_ProductComment]
ON [dbo].[Comments]
    ([ProductId]);
GO

-- Creating foreign key on [AdminUsername] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_AdminProduct]
    FOREIGN KEY ([AdminUsername])
    REFERENCES [dbo].[Admins]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminProduct'
CREATE INDEX [IX_FK_AdminProduct]
ON [dbo].[Products]
    ([AdminUsername]);
GO

-- Creating foreign key on [UserUsername] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_UserLike]
    FOREIGN KEY ([UserUsername])
    REFERENCES [dbo].[Users]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLike'
CREATE INDEX [IX_FK_UserLike]
ON [dbo].[Likes]
    ([UserUsername]);
GO

-- Creating foreign key on [UserUsername] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_UserOrder]
    FOREIGN KEY ([UserUsername])
    REFERENCES [dbo].[Users]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOrder'
CREATE INDEX [IX_FK_UserOrder]
ON [dbo].[Orders]
    ([UserUsername]);
GO

-- Creating foreign key on [UserUsername] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [FK_UserTransaction]
    FOREIGN KEY ([UserUsername])
    REFERENCES [dbo].[Users]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTransaction'
CREATE INDEX [IX_FK_UserTransaction]
ON [dbo].[Transactions]
    ([UserUsername]);
GO

-- Creating foreign key on [UserUsername] in table 'Transfers'
ALTER TABLE [dbo].[Transfers]
ADD CONSTRAINT [FK_UserTransfer]
    FOREIGN KEY ([UserUsername])
    REFERENCES [dbo].[Users]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTransfer'
CREATE INDEX [IX_FK_UserTransfer]
ON [dbo].[Transfers]
    ([UserUsername]);
GO

-- Creating foreign key on [AdminUsername] in table 'Slides'
ALTER TABLE [dbo].[Slides]
ADD CONSTRAINT [FK_AdminSlide]
    FOREIGN KEY ([AdminUsername])
    REFERENCES [dbo].[Admins]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminSlide'
CREATE INDEX [IX_FK_AdminSlide]
ON [dbo].[Slides]
    ([AdminUsername]);
GO

-- Creating foreign key on [AdminUsername] in table 'Uploads'
ALTER TABLE [dbo].[Uploads]
ADD CONSTRAINT [FK_AdminUpload]
    FOREIGN KEY ([AdminUsername])
    REFERENCES [dbo].[Admins]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminUpload'
CREATE INDEX [IX_FK_AdminUpload]
ON [dbo].[Uploads]
    ([AdminUsername]);
GO

-- Creating foreign key on [AdminUsername] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_AdminPost]
    FOREIGN KEY ([AdminUsername])
    REFERENCES [dbo].[Admins]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminPost'
CREATE INDEX [IX_FK_AdminPost]
ON [dbo].[Posts]
    ([AdminUsername]);
GO

-- Creating foreign key on [AdminUsername] in table 'Finilizations'
ALTER TABLE [dbo].[Finilizations]
ADD CONSTRAINT [FK_AdminFinilization]
    FOREIGN KEY ([AdminUsername])
    REFERENCES [dbo].[Admins]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminFinilization'
CREATE INDEX [IX_FK_AdminFinilization]
ON [dbo].[Finilizations]
    ([AdminUsername]);
GO

-- Creating foreign key on [ProductId] in table 'Finilizations'
ALTER TABLE [dbo].[Finilizations]
ADD CONSTRAINT [FK_ProductFinilization]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductFinilization'
CREATE INDEX [IX_FK_ProductFinilization]
ON [dbo].[Finilizations]
    ([ProductId]);
GO

-- Creating foreign key on [UserUsername] in table 'Finilizations'
ALTER TABLE [dbo].[Finilizations]
ADD CONSTRAINT [FK_UserFinilization]
    FOREIGN KEY ([UserUsername])
    REFERENCES [dbo].[Users]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserFinilization'
CREATE INDEX [IX_FK_UserFinilization]
ON [dbo].[Finilizations]
    ([UserUsername]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------