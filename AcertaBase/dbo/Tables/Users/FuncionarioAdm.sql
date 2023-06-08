CREATE TABLE [dbo].[FuncionarioAdm] (
    [Id]       INT           IDENTITY (0, 1) NOT NULL,
    [Login]    VARCHAR (255) NULL,
    [Password] VARCHAR (255) NULL,
    [Foto] VARBINARY(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

