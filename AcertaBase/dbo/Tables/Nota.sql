CREATE TABLE [dbo].[Nota]
(
    [IdAluno] INT NOT NULL, 
    [IdProva] INT NOT NULL, 
    [Nota] TINYINT NOT NULL, 
    CONSTRAINT [FK_Nota_ToAluno] FOREIGN KEY ([IdAluno]) REFERENCES [Aluno]([Id]), 
    CONSTRAINT [FK_Nota_ToProva] FOREIGN KEY ([IdProva]) REFERENCES [Prova]([Id])
)
