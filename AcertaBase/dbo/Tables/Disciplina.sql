﻿CREATE TABLE [dbo].[Disciplina]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Semester] TINYINT NOT NULL, 
    [Workload] TINYINT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL 
)
