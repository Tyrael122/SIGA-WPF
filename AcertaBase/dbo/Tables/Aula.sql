﻿CREATE TABLE [dbo].[Aula]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [IdDisciplina] INT NOT NULL, 
    [IdCurso] INT NULL, 
    [IdProfessor] INT NOT NULL,
    [IdHorario] INT NOT NULL, 
    [Data] DATE NOT NULL
)