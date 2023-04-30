CREATE TABLE [dbo].[DisciplinasAluno]
(
    [IdAluno] INT NOT NULL, 
	[IdDisciplina] INT NOT NULL, 
    CONSTRAINT [IdDisciplinaAluno] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]) 
)
