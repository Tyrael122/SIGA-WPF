CREATE TABLE [dbo].[Prova]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Data] DATE NOT NULL, 
    [Tipo] TINYINT NOT NULL, 
    [IdDisciplina] INT NOT NULL, 
    [IdProfessor] NCHAR(10) NULL, 
    CONSTRAINT [FK_Prova_ToDisciplina] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id])
)
