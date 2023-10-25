IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Fornecedores] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Documento] varchar(14) NOT NULL,
    [TipoFornecedor] int NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Enderecos] (
    [Id] uniqueidentifier NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [Logradouro] varchar(200) NOT NULL,
    [Numero] varchar(50) NOT NULL,
    [Complemento] varchar(200) NULL,
    [Cep] varchar(8) NOT NULL,
    [Bairro] varchar(150) NOT NULL,
    [Cidade] varchar(50) NOT NULL,
    [Estado] varchar(100) NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enderecos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id])
);
GO

CREATE TABLE [Produtos] (
    [Id] uniqueidentifier NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Descricao] varchar(1000) NOT NULL,
    [Imagem] varchar(200) NOT NULL,
    [Valor] decimal(10,4) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produtos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Enderecos_FornecedorId] ON [Enderecos] ([FornecedorId]);
GO

CREATE INDEX [IX_Produtos_FornecedorId] ON [Produtos] ([FornecedorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230602002634_Criacao_tabelas', N'6.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Logradouro');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Logradouro] varchar(200) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230602021318_Correcao_logradouro', N'6.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Estado');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Estado] varchar(50) NOT NULL;
ALTER TABLE [Enderecos] ADD DEFAULT '' FOR [Estado];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Enderecos]') AND [c].[name] = N'Cidade');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Enderecos] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Enderecos] ALTER COLUMN [Cidade] varchar(150) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230602022147_Correcao_cidade', N'6.0.16');
GO

COMMIT;
GO

