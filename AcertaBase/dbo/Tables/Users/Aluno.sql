﻿CREATE TABLE [dbo].[Aluno] (
    [Id]       INT           IDENTITY (0, 1) NOT NULL,
    [Login]    VARCHAR (255) NULL,
    [Password] VARCHAR (255) NULL,
    [Curso] INT NULL, 
    [SemestreInicio] TINYINT NULL, 
    [Foto] VARBINARY(MAX) NULL, 
    [Email] VARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

