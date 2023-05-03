CREATE TABLE [dbo].[ProfessorDisciplina]
(
    [IdProfessor] INT NOT NULL, 
	[IdDisciplina] INT NOT NULL, 
    CONSTRAINT [IdDisciplinaProfessor] FOREIGN KEY ([IdDisciplina]) REFERENCES [Disciplina]([Id]), 
    CONSTRAINT [IdProfessorDisciplina] FOREIGN KEY ([IdProfessor]) REFERENCES [Professor]([Id]) 
)
