CREATE TABLE [dbo].[DisciplinaCurso]
(
    [IdCurso] INT NOT NULL, 
	[IdDisciplina] INT NOT NULL, 
    CONSTRAINT [IdDisciplinaCurso] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]) 
)
