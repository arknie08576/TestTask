﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE TABLE [ApprovalRequests] (
        [Id] int NOT NULL IDENTITY,
        [Approver] int NULL,
        [LeaveRequest] int NULL,
        [Status] int NOT NULL,
        [Comment] nvarchar(100) NULL,
        CONSTRAINT [PK_ApprovalRequests] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE TABLE [Employes] (
        [Id] int NOT NULL IDENTITY,
        [Username] nvarchar(100) NOT NULL,
        [PasswordHash] nvarchar(100) NOT NULL,
        [Salt] nvarchar(100) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [Subdivision] int NOT NULL,
        [Position] int NOT NULL,
        [Status] int NOT NULL,
        [PeoplePartner] int NULL,
        [Out_of_OfficeBalance] int NOT NULL,
        [Photo] nvarchar(100) NULL,
        [AssignedProject] int NULL,
        CONSTRAINT [PK_Employes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Employes_Employes_PeoplePartner] FOREIGN KEY ([PeoplePartner]) REFERENCES [Employes] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE TABLE [LeaveRequests] (
        [Id] int NOT NULL IDENTITY,
        [Employee] int NULL,
        [AbsenceReason] int NOT NULL,
        [StartDate] date NOT NULL,
        [EndDate] date NOT NULL,
        [Comment] nvarchar(100) NULL,
        [Status] int NOT NULL,
        CONSTRAINT [PK_LeaveRequests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LeaveRequests_Employes_Employee] FOREIGN KEY ([Employee]) REFERENCES [Employes] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [ProjectType] int NOT NULL,
        [StartDate] date NOT NULL,
        [EndDate] date NULL,
        [ProjectManager] int NULL,
        [Comment] nvarchar(100) NULL,
        [ProjectStatus] int NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Projects_Employes_ProjectManager] FOREIGN KEY ([ProjectManager]) REFERENCES [Employes] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE INDEX [IX_ApprovalRequests_Approver] ON [ApprovalRequests] ([Approver]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE INDEX [IX_ApprovalRequests_LeaveRequest] ON [ApprovalRequests] ([LeaveRequest]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE INDEX [IX_Employes_AssignedProject] ON [Employes] ([AssignedProject]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE INDEX [IX_Employes_PeoplePartner] ON [Employes] ([PeoplePartner]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE INDEX [IX_LeaveRequests_Employee] ON [LeaveRequests] ([Employee]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    CREATE INDEX [IX_Projects_ProjectManager] ON [Projects] ([ProjectManager]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    ALTER TABLE [ApprovalRequests] ADD CONSTRAINT [FK_ApprovalRequests_Employes_Approver] FOREIGN KEY ([Approver]) REFERENCES [Employes] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    ALTER TABLE [ApprovalRequests] ADD CONSTRAINT [FK_ApprovalRequests_LeaveRequests_LeaveRequest] FOREIGN KEY ([LeaveRequest]) REFERENCES [LeaveRequests] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    ALTER TABLE [Employes] ADD CONSTRAINT [FK_Employes_Projects_AssignedProject] FOREIGN KEY ([AssignedProject]) REFERENCES [Projects] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240620162548_1'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240620162548_1', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624115503_2'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employes]') AND [c].[name] = N'Photo');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Employes] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Employes] ALTER COLUMN [Photo] varbinary(100) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624115503_2'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240624115503_2', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624115723_3'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employes]') AND [c].[name] = N'Photo');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Employes] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Employes] ALTER COLUMN [Photo] varbinary(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624115723_3'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240624115723_3', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240630153028_4'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240630153028_4', N'8.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    ALTER TABLE [ApprovalRequests] DROP CONSTRAINT [FK_ApprovalRequests_LeaveRequests_LeaveRequest];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    ALTER TABLE [LeaveRequests] DROP CONSTRAINT [FK_LeaveRequests_Employes_Employee];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    DROP INDEX [IX_LeaveRequests_Employee] ON [LeaveRequests];
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LeaveRequests]') AND [c].[name] = N'Employee');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [LeaveRequests] DROP CONSTRAINT [' + @var2 + '];');
    EXEC(N'UPDATE [LeaveRequests] SET [Employee] = 0 WHERE [Employee] IS NULL');
    ALTER TABLE [LeaveRequests] ALTER COLUMN [Employee] int NOT NULL;
    ALTER TABLE [LeaveRequests] ADD DEFAULT 0 FOR [Employee];
    CREATE INDEX [IX_LeaveRequests_Employee] ON [LeaveRequests] ([Employee]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    DROP INDEX [IX_ApprovalRequests_LeaveRequest] ON [ApprovalRequests];
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ApprovalRequests]') AND [c].[name] = N'LeaveRequest');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ApprovalRequests] DROP CONSTRAINT [' + @var3 + '];');
    EXEC(N'UPDATE [ApprovalRequests] SET [LeaveRequest] = 0 WHERE [LeaveRequest] IS NULL');
    ALTER TABLE [ApprovalRequests] ALTER COLUMN [LeaveRequest] int NOT NULL;
    ALTER TABLE [ApprovalRequests] ADD DEFAULT 0 FOR [LeaveRequest];
    CREATE INDEX [IX_ApprovalRequests_LeaveRequest] ON [ApprovalRequests] ([LeaveRequest]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    ALTER TABLE [ApprovalRequests] ADD CONSTRAINT [FK_ApprovalRequests_LeaveRequests_LeaveRequest] FOREIGN KEY ([LeaveRequest]) REFERENCES [LeaveRequests] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    ALTER TABLE [LeaveRequests] ADD CONSTRAINT [FK_LeaveRequests_Employes_Employee] FOREIGN KEY ([Employee]) REFERENCES [Employes] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240704094412_5'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240704094412_5', N'8.0.6');
END;
GO

COMMIT;
GO

