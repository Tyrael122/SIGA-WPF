CREATE TABLE [dbo].[AlunoDisciplina]
(
    [IdAluno] INT NOT NULL, 
	[IdDisciplina] INT NOT NULL, 
    CONSTRAINT [IdDisciplinaAluno] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]) 
)
