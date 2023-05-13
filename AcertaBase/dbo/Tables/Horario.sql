CREATE TABLE [dbo].[Horario]
(
	[IdDisciplina] INT NOT NULL , 
    [IdCurso] INT NOT NULL, 
    [IdProfessor] INT NOT NULL, 
    [Semestre] INT NULL, 
    [DiaSemana] TINYINT NULL, 
    [HorarioInicio] TIME NULL, 
    [HorarioFim] TIME NULL, 
    [Id] INT NOT NULL IDENTITY, 
    CONSTRAINT [PK_Horario] PRIMARY KEY ([Id])
)
