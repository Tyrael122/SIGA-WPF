CREATE TABLE [dbo].[Solicitacao]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdAluno] INT NULL, 
    [Tipo] TINYINT NULL, 
    [Documento] VARBINARY(MAX) NULL, 
    [TituloDocumento] VARCHAR(MAX) NULL
)
