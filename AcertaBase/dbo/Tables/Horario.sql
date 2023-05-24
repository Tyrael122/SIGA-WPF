CREATE TABLE [dbo].[Horario]
(
    [Id] INT NOT NULL IDENTITY, 
	[IdDisciplina] INT NOT NULL , 
    [IdCurso] INT NOT NULL, 
    [IdProfessor] INT NOT NULL, 
    [Semestre] INT NULL, 
    [DiaSemana] TINYINT NULL, 
    [HorarioInicio] TIME NULL, 
    [HorarioFim] TIME NULL
    CONSTRAINT [PK_Horario] PRIMARY KEY ([Id])
)
