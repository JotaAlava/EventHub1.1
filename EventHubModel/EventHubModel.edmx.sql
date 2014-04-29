
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/28/2014 12:55:11
-- Generated from EDMX file: C:\Users\joseal\Documents\GitHub\EventHub1.1\EventHubModel\EventHubModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EventHub1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActivityLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_ActivityLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEvent_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEvent] DROP CONSTRAINT [FK_UserEvent_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEvent_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEvent] DROP CONSTRAINT [FK_UserEvent_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPlusOne]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlusOnes] DROP CONSTRAINT [FK_UserPlusOne];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPlusOne]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlusOnes] DROP CONSTRAINT [FK_EventPlusOne];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_UserMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_EventMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_EventMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_ActivityEvent];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[PlusOnes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlusOnes];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[UserEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEvent];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [ActivityId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DayOfWeek] nvarchar(max)  NOT NULL,
    [Time] datetime  NOT NULL,
    [LocationId] int  NOT NULL,
    [Active] bit  NOT NULL,
    [PreferredNumberOfPlayers1] int  NOT NULL,
    [RequiredNumberOfPlayers1] int  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [LocationId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [EventId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ActivityId] int  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [EMail] nvarchar(max)  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'PlusOnes'
CREATE TABLE [dbo].[PlusOnes] (
    [PlusOneId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [EventId] int  NOT NULL,
    [Deleted] bit  NOT NULL
);
GO

-- Creating table 'UserEvent'
CREATE TABLE [dbo].[UserEvent] (
    [Users_UserId] int  NOT NULL,
    [Events_EventId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ActivityId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([ActivityId] ASC);
GO

-- Creating primary key on [LocationId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([LocationId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [PlusOneId] in table 'PlusOnes'
ALTER TABLE [dbo].[PlusOnes]
ADD CONSTRAINT [PK_PlusOnes]
    PRIMARY KEY CLUSTERED ([PlusOneId] ASC);
GO

-- Creating primary key on [MessageId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [Users_UserId], [Events_EventId] in table 'UserEvent'
ALTER TABLE [dbo].[UserEvent]
ADD CONSTRAINT [PK_UserEvent]
    PRIMARY KEY CLUSTERED ([Users_UserId], [Events_EventId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LocationId] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_ActivityLocation]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([LocationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityLocation'
CREATE INDEX [IX_FK_ActivityLocation]
ON [dbo].[Activities]
    ([LocationId]);
GO

-- Creating foreign key on [Users_UserId] in table 'UserEvent'
ALTER TABLE [dbo].[UserEvent]
ADD CONSTRAINT [FK_UserEvent_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Events_EventId] in table 'UserEvent'
ALTER TABLE [dbo].[UserEvent]
ADD CONSTRAINT [FK_UserEvent_Event]
    FOREIGN KEY ([Events_EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEvent_Event'
CREATE INDEX [IX_FK_UserEvent_Event]
ON [dbo].[UserEvent]
    ([Events_EventId]);
GO

-- Creating foreign key on [UserId] in table 'PlusOnes'
ALTER TABLE [dbo].[PlusOnes]
ADD CONSTRAINT [FK_UserPlusOne]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPlusOne'
CREATE INDEX [IX_FK_UserPlusOne]
ON [dbo].[PlusOnes]
    ([UserId]);
GO

-- Creating foreign key on [EventId] in table 'PlusOnes'
ALTER TABLE [dbo].[PlusOnes]
ADD CONSTRAINT [FK_EventPlusOne]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPlusOne'
CREATE INDEX [IX_FK_EventPlusOne]
ON [dbo].[PlusOnes]
    ([EventId]);
GO

-- Creating foreign key on [UserId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_UserMessage]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMessage'
CREATE INDEX [IX_FK_UserMessage]
ON [dbo].[Messages]
    ([UserId]);
GO

-- Creating foreign key on [EventId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_EventMessage]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventMessage'
CREATE INDEX [IX_FK_EventMessage]
ON [dbo].[Messages]
    ([EventId]);
GO

-- Creating foreign key on [ActivityId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_ActivityEvent]
    FOREIGN KEY ([ActivityId])
    REFERENCES [dbo].[Activities]
        ([ActivityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityEvent'
CREATE INDEX [IX_FK_ActivityEvent]
ON [dbo].[Events]
    ([ActivityId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------