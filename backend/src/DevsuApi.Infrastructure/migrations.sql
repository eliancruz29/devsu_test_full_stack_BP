
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DevsuAppDb')
BEGIN
    CREATE DATABASE DevsuAppDb;
    PRINT 'Database DevsuAppDb created successfully.';
END
ELSE
BEGIN
    PRINT 'Database DevsuAppDb already exists.';
END

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
CREATE TABLE [Accounts] (
    [Id] uniqueidentifier NOT NULL,
    [AccountNumber] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [OpeningBalance] int NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id])
);

CREATE TABLE [Clients] (
    [Id] uniqueidentifier NOT NULL,
    [ClientId] uniqueidentifier NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Gender] int NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Identification] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
);

CREATE TABLE [Tranfers] (
    [Id] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Amount] int NOT NULL,
    [Balance] int NOT NULL,
    CONSTRAINT [PK_Tranfers] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250727133324_InitialCreate', N'9.0.7');

DROP TABLE [Tranfers];

ALTER TABLE [Accounts] ADD [ClientId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE TABLE [Transfers] (
    [Id] uniqueidentifier NOT NULL,
    [AccountId] uniqueidentifier NOT NULL,
    [Date] datetime2 NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Amount] int NOT NULL,
    [Balance] int NOT NULL,
    CONSTRAINT [PK_Transfers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transfers_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Accounts_ClientId] ON [Accounts] ([ClientId]);

CREATE INDEX [IX_Transfers_AccountId] ON [Transfers] ([AccountId]);

ALTER TABLE [Accounts] ADD CONSTRAINT [FK_Accounts_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250728195625_AddingRelations_AND_SolveTypoError', N'9.0.7');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clients]') AND [c].[name] = N'Gender');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Clients] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Clients] ALTER COLUMN [Gender] nvarchar(max) NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250728230956_AddingMissingEnumTranslation', N'9.0.7');

ALTER TABLE [Transfers] ADD [Status] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250804045131_AddingMissingStatusProperty', N'9.0.7');

COMMIT;
GO

