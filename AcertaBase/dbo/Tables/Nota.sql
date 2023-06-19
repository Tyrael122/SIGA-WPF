CREATE TABLE [dbo].[Nota]
(
    [IdAluno] INT NOT NULL, 
    [IdProva] INT NOT NULL, 
    [Nota] TINYINT NOT NULL, 
    [Id] INT NOT NULL IDENTITY, 
    CONSTRAINT [FK_Nota_ToAluno] FOREIGN KEY ([IdAluno]) REFERENCES [Aluno]([Id]), 
    CONSTRAINT [FK_Nota_ToProva] FOREIGN KEY ([IdProva]) REFERENCES [Prova]([Id]), 
    CONSTRAINT [PK_Nota] PRIMARY KEY ([Id])
)
