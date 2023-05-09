CREATE TABLE [dbo].[Horario]
(
	[IdDisciplina] INT NOT NULL PRIMARY KEY, 
    [IdCurso] NCHAR(10) NOT NULL, 
    [IdProfessor] NCHAR(10) NOT NULL, 
    [Semestre] NCHAR(10) NULL, 
    [DiaSemana] NCHAR(10) NULL
)
