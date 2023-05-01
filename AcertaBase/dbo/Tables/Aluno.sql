CREATE TABLE [dbo].[Aluno] (
    [ID]       INT           IDENTITY (0, 1) NOT NULL,
    [Login]    VARCHAR (255) NULL,
    [Password] VARCHAR (255) NULL,
    [Curso] INT NULL, 
    [SemestreInicio] TINYINT NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC), 
    CONSTRAINT [IdCursoAluno] FOREIGN KEY ([Curso]) REFERENCES [Curso]([Id])
);

