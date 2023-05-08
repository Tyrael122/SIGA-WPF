CREATE TABLE [dbo].[CursoDisciplina]
(
    [IdCurso] INT NOT NULL, 
	[IdDisciplina] INT NOT NULL, 
    CONSTRAINT [IdDisciplinaCurso] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]) 
)
