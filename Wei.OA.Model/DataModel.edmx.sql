
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/09/2019 19:50:35
-- Generated from EDMX file: E:\vs项目\OA项目\Wei.OA\Wei.OA.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WeiOA];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO



-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'WF_Temp'
CREATE TABLE [dbo].[WF_Temp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TempName] nvarchar(32)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [TempForm] nvarchar(max)  NULL,
    [Remark] nvarchar(64)  NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ActivityType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WF_Instance'
CREATE TABLE [dbo].[WF_Instance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstanceName] nvarchar(128)  NOT NULL,
    [StartBy] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Level] smallint  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [Remark] nvarchar(128)  NULL,
    [DelFlag] smallint  NOT NULL,
    [FilePath] nvarchar(max)  NOT NULL,
    [WFInstanceId] uniqueidentifier  NOT NULL,
    [WF_TempId] int  NOT NULL
);
GO

-- Creating table 'FileInfo'
CREATE TABLE [dbo].[FileInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(128)  NOT NULL,
    [FileType] nvarchar(32)  NULL,
    [FilePath] nvarchar(max)  NULL,
    [FileSize] nvarchar(32)  NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(64)  NULL
);
GO

-- Creating table 'WF_Step'
CREATE TABLE [dbo].[WF_Step] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StepName] nvarchar(32)  NULL,
    [ProcessBy] int  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ProcessTime] datetime  NOT NULL,
    [ProcessResult] nvarchar(256)  NOT NULL,
    [ProcessComment] nvarchar(256)  NULL,
    [StepStatus] smallint  NOT NULL,
    [IsStartStep] bit  NOT NULL,
    [IsEndStep] bit  NOT NULL,
    [ParentStepId] int  NOT NULL,
    [WF_InstanceId] int  NOT NULL
);
GO

-- Creating table 'WF_InstanceFileInfo'
CREATE TABLE [dbo].[WF_InstanceFileInfo] (
    [WF_Instance_Id] int  NOT NULL,
    [FileInfo_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- Creating primary key on [Id] in table 'WF_Temp'
ALTER TABLE [dbo].[WF_Temp]
ADD CONSTRAINT [PK_WF_Temp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [PK_WF_Instance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileInfo'
ALTER TABLE [dbo].[FileInfo]
ADD CONSTRAINT [PK_FileInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [PK_WF_Step]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO


-- Creating primary key on [WF_Instance_Id], [FileInfo_Id] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [PK_WF_InstanceFileInfo]
    PRIMARY KEY CLUSTERED ([WF_Instance_Id], [FileInfo_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------




-- Creating foreign key on [WF_Instance_Id] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [FK_WF_InstanceFileInfo_WF_Instance]
    FOREIGN KEY ([WF_Instance_Id])
    REFERENCES [dbo].[WF_Instance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FileInfo_Id] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [FK_WF_InstanceFileInfo_FileInfo]
    FOREIGN KEY ([FileInfo_Id])
    REFERENCES [dbo].[FileInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceFileInfo_FileInfo'
CREATE INDEX [IX_FK_WF_InstanceFileInfo_FileInfo]
ON [dbo].[WF_InstanceFileInfo]
    ([FileInfo_Id]);
GO

-- Creating foreign key on [WF_TempId] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [FK_WF_TempWF_Instance]
    FOREIGN KEY ([WF_TempId])
    REFERENCES [dbo].[WF_Temp]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_TempWF_Instance'
CREATE INDEX [IX_FK_WF_TempWF_Instance]
ON [dbo].[WF_Instance]
    ([WF_TempId]);
GO

-- Creating foreign key on [WF_InstanceId] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [FK_WF_InstanceWF_Step]
    FOREIGN KEY ([WF_InstanceId])
    REFERENCES [dbo].[WF_Instance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceWF_Step'
CREATE INDEX [IX_FK_WF_InstanceWF_Step]
ON [dbo].[WF_Step]
    ([WF_InstanceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------