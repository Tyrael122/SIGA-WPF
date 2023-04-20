CREATE TABLE [dbo].[DisciplinasAluno]
(
	[IdDisciplina] INT NOT NULL, 
    [IdAluno] INT NOT NULL, 
    CONSTRAINT [IdDisciplina] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]), 
    CONSTRAINT [PK_DisciplinasAluno] PRIMARY KEY ([IdAluno])
)
