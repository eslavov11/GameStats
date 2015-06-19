
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/18/2015 22:34:30
-- Generated from EDMX file: D:\Edi\GitHub\GameStats\GameStats.DataAccess\GameStatsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Context-20150422135428];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[GAME_CATEGORY]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GAME_CATEGORY];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CATEGORY'
CREATE TABLE [dbo].[CATEGORY] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GAME'
CREATE TABLE [dbo].[GAME] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] nvarchar(max)  NOT NULL,
    [DESCRIPTION] nvarchar(max)  NOT NULL,
    [CATEGORY_ID] varbinary(max)  NOT NULL
);
GO

-- Creating table 'GAME_CATEGORY'
CREATE TABLE [dbo].[GAME_CATEGORY] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CATEGORY_ID] int  NOT NULL,
    [GAME_ID] int  NOT NULL,
    [CATEGORY_ID1] int  NULL,
    [GAME_ID1] int  NULL
);
GO

-- Creating table 'HUMAN'
CREATE TABLE [dbo].[HUMAN] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FIRST_NAME] nvarchar(50)  NOT NULL,
    [SECOND_NAME] nvarchar(50)  NOT NULL,
    [LAST_NAME] nvarchar(50)  NOT NULL,
    [EMAIL] nvarchar(50)  NOT NULL,
    [PHONE_NUMBER] nvarchar(50)  NOT NULL,
    [DATE_OF_BIRTH] datetime  NOT NULL,
    [PICTURE] varbinary(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'CATEGORY'
ALTER TABLE [dbo].[CATEGORY]
ADD CONSTRAINT [PK_CATEGORY]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GAME'
ALTER TABLE [dbo].[GAME]
ADD CONSTRAINT [PK_GAME]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GAME_CATEGORY'
ALTER TABLE [dbo].[GAME_CATEGORY]
ADD CONSTRAINT [PK_GAME_CATEGORY]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'HUMAN'
ALTER TABLE [dbo].[HUMAN]
ADD CONSTRAINT [PK_HUMAN]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CATEGORY_ID] in table 'GAME_CATEGORY'
ALTER TABLE [dbo].[GAME_CATEGORY]
ADD CONSTRAINT [FK_CategoryGAME_CATEGORY]
    FOREIGN KEY ([CATEGORY_ID])
    REFERENCES [dbo].[CATEGORY]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryGAME_CATEGORY'
CREATE INDEX [IX_FK_CategoryGAME_CATEGORY]
ON [dbo].[GAME_CATEGORY]
    ([CATEGORY_ID]);
GO

-- Creating foreign key on [GAME_ID] in table 'GAME_CATEGORY'
ALTER TABLE [dbo].[GAME_CATEGORY]
ADD CONSTRAINT [FK_GameGAME_CATEGORY]
    FOREIGN KEY ([GAME_ID])
    REFERENCES [dbo].[GAME]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GameGAME_CATEGORY'
CREATE INDEX [IX_FK_GameGAME_CATEGORY]
ON [dbo].[GAME_CATEGORY]
    ([GAME_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------