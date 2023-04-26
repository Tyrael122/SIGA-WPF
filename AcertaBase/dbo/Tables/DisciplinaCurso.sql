CREATE TABLE [dbo].[DisciplinasCurso]
(
    [IdCurso] INT NOT NULL, 
	[IdDisciplina] INT NOT NULL, 
    CONSTRAINT [IdDisciplinaCurso] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]), 
    CONSTRAINT [PK_DisciplinasCurso] PRIMARY KEY ([IdCurso])
)
