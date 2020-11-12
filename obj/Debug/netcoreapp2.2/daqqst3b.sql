CREATE TABLE [Office] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Office] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [SpecialMenuOrder] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    [StoreOutItemId] bigint NULL,
    [OfficeId] bigint NULL,
    [UnitOrdered] float NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    CONSTRAINT [PK_SpecialMenuOrder] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialMenuOrder_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialMenuOrder_Office_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [Office] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialMenuOrder_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialMenuOrder_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_SpecialMenuOrder_MealTypeId] ON [SpecialMenuOrder] ([MealTypeId]);

GO

CREATE INDEX [IX_SpecialMenuOrder_OfficeId] ON [SpecialMenuOrder] ([OfficeId]);

GO

CREATE INDEX [IX_SpecialMenuOrder_StoreOutItemId] ON [SpecialMenuOrder] ([StoreOutItemId]);

GO

CREATE INDEX [IX_SpecialMenuOrder_UserId] ON [SpecialMenuOrder] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191223055526_SpecialMenu', N'2.2.6-servicing-10079');

GO

ALTER TABLE [SpecialMenuOrder] DROP CONSTRAINT [FK_SpecialMenuOrder_MealType_MealTypeId];

GO

ALTER TABLE [SpecialMenuOrder] DROP CONSTRAINT [FK_SpecialMenuOrder_Office_OfficeId];

GO

ALTER TABLE [SpecialMenuOrder] DROP CONSTRAINT [FK_SpecialMenuOrder_AspNetUsers_UserId];

GO

DROP INDEX [IX_SpecialMenuOrder_OfficeId] ON [SpecialMenuOrder];

GO

DROP INDEX [IX_SpecialMenuOrder_UserId] ON [SpecialMenuOrder];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SpecialMenuOrder]') AND [c].[name] = N'OfficeId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SpecialMenuOrder] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [SpecialMenuOrder] DROP COLUMN [OfficeId];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SpecialMenuOrder]') AND [c].[name] = N'OrderDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [SpecialMenuOrder] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [SpecialMenuOrder] DROP COLUMN [OrderDate];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SpecialMenuOrder]') AND [c].[name] = N'UserId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SpecialMenuOrder] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [SpecialMenuOrder] DROP COLUMN [UserId];

GO

EXEC sp_rename N'[SpecialMenuOrder].[MealTypeId]', N'SpecialMenuParentId', N'COLUMN';

GO

EXEC sp_rename N'[SpecialMenuOrder].[IX_SpecialMenuOrder_MealTypeId]', N'IX_SpecialMenuOrder_SpecialMenuParentId', N'INDEX';

GO

CREATE TABLE [SpecialMenuParent] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    [OfficeId] bigint NULL,
    [OrderDate] datetime2 NOT NULL,
    CONSTRAINT [PK_SpecialMenuParent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SpecialMenuParent_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialMenuParent_Office_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [Office] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SpecialMenuParent_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_SpecialMenuParent_MealTypeId] ON [SpecialMenuParent] ([MealTypeId]);

GO

CREATE INDEX [IX_SpecialMenuParent_OfficeId] ON [SpecialMenuParent] ([OfficeId]);

GO

CREATE INDEX [IX_SpecialMenuParent_UserId] ON [SpecialMenuParent] ([UserId]);

GO

ALTER TABLE [SpecialMenuOrder] ADD CONSTRAINT [FK_SpecialMenuOrder_SpecialMenuParent_SpecialMenuParentId] FOREIGN KEY ([SpecialMenuParentId]) REFERENCES [SpecialMenuParent] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200101070351_SpclMod', N'2.2.6-servicing-10079');

GO

