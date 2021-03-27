ALTER TABLE [MenuItem] ADD [StoreOutItemId] bigint NULL;

GO

CREATE TABLE [CustomerDailyMenuChoice] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [quantity] float NOT NULL,
    [Day] int NOT NULL,
    [MenuItemId] bigint NULL,
    [OrderTypeId] bigint NOT NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    [ExtraItemId] bigint NULL,
    CONSTRAINT [PK_CustomerDailyMenuChoice] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerDailyMenuChoice_ExtraItem_ExtraItemId] FOREIGN KEY ([ExtraItemId]) REFERENCES [ExtraItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerDailyMenuChoice_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerDailyMenuChoice_MenuItem_MenuItemId] FOREIGN KEY ([MenuItemId]) REFERENCES [MenuItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerDailyMenuChoice_OrderType_OrderTypeId] FOREIGN KEY ([OrderTypeId]) REFERENCES [OrderType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerDailyMenuChoice_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_MenuItem_StoreOutItemId] ON [MenuItem] ([StoreOutItemId]);

GO

CREATE INDEX [IX_CustomerDailyMenuChoice_ExtraItemId] ON [CustomerDailyMenuChoice] ([ExtraItemId]);

GO

CREATE INDEX [IX_CustomerDailyMenuChoice_MealTypeId] ON [CustomerDailyMenuChoice] ([MealTypeId]);

GO

CREATE INDEX [IX_CustomerDailyMenuChoice_MenuItemId] ON [CustomerDailyMenuChoice] ([MenuItemId]);

GO

CREATE INDEX [IX_CustomerDailyMenuChoice_OrderTypeId] ON [CustomerDailyMenuChoice] ([OrderTypeId]);

GO

CREATE INDEX [IX_CustomerDailyMenuChoice_UserId] ON [CustomerDailyMenuChoice] ([UserId]);

GO

ALTER TABLE [MenuItem] ADD CONSTRAINT [FK_MenuItem_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210115054502_CustomerMenuChoice', N'2.2.6-servicing-10079');

GO

CREATE TABLE [UserDateChoiceMaster] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    CONSTRAINT [PK_UserDateChoiceMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserDateChoiceMaster_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserDateChoiceMaster_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [UserDateChoiceDetail] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserDateChoiceMasterId] bigint NULL,
    [Date] datetime2 NOT NULL,
    [IsOrderSet] bit NOT NULL,
    CONSTRAINT [PK_UserDateChoiceDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserDateChoiceDetail_UserDateChoiceMaster_UserDateChoiceMasterId] FOREIGN KEY ([UserDateChoiceMasterId]) REFERENCES [UserDateChoiceMaster] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_UserDateChoiceDetail_UserDateChoiceMasterId] ON [UserDateChoiceDetail] ([UserDateChoiceMasterId]);

GO

CREATE INDEX [IX_UserDateChoiceMaster_MealTypeId] ON [UserDateChoiceMaster] ([MealTypeId]);

GO

CREATE INDEX [IX_UserDateChoiceMaster_UserId] ON [UserDateChoiceMaster] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210115075827_UserDateChoice', N'2.2.6-servicing-10079');

GO

ALTER TABLE [CustomerChoiceV2] ADD [MenuItemId] bigint NULL DEFAULT CAST(0 AS bigint);

GO

CREATE INDEX [IX_CustomerChoiceV2_MenuItemId] ON [CustomerChoiceV2] ([MenuItemId]);

GO

ALTER TABLE [CustomerChoiceV2] ADD CONSTRAINT [FK_CustomerChoiceV2_MenuItem_MenuItemId] FOREIGN KEY ([MenuItemId]) REFERENCES [MenuItem] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210120095547_MenuAdd', N'2.2.6-servicing-10079');

GO

ALTER TABLE [CustomerChoiceV2] DROP CONSTRAINT [FK_CustomerChoiceV2_MenuItem_MenuItemId];

GO

ALTER TABLE [CustomerDailyMenuChoice] DROP CONSTRAINT [FK_CustomerDailyMenuChoice_MenuItem_MenuItemId];

GO

DROP INDEX [IX_CustomerDailyMenuChoice_MenuItemId] ON [CustomerDailyMenuChoice];

GO

DROP INDEX [IX_CustomerChoiceV2_MenuItemId] ON [CustomerChoiceV2];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerDailyMenuChoice]') AND [c].[name] = N'MenuItemId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CustomerDailyMenuChoice] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [CustomerDailyMenuChoice] DROP COLUMN [MenuItemId];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerChoiceV2]') AND [c].[name] = N'MenuItemId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CustomerChoiceV2] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [CustomerChoiceV2] DROP COLUMN [MenuItemId];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210120135332_RemoveMenu', N'2.2.6-servicing-10079');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210120191345_ExtChit', N'2.2.6-servicing-10079');

GO

CREATE TABLE [ExtraChitParent] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    CONSTRAINT [PK_ExtraChitParent] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210120192557_TestChit', N'2.2.6-servicing-10079');

GO

ALTER TABLE [CustomerChoiceV2] ADD [ExtraChitParentId] bigint NULL DEFAULT CAST(0 AS bigint);

GO

CREATE INDEX [IX_CustomerChoiceV2_ExtraChitParentId] ON [CustomerChoiceV2] ([ExtraChitParentId]);

GO

ALTER TABLE [CustomerChoiceV2] ADD CONSTRAINT [FK_CustomerChoiceV2_ExtraChitParent_ExtraChitParentId] FOREIGN KEY ([ExtraChitParentId]) REFERENCES [ExtraChitParent] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210120193804_ConsumerChit', N'2.2.6-servicing-10079');

GO

CREATE TABLE [OnSpotParent] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [OfficeId] bigint NULL,
    [BearerId] nvarchar(max) NULL,
    [Date] datetime2 NOT NULL,
    [IsOfficeOrder] bit NOT NULL,
    CONSTRAINT [PK_OnSpotParent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OnSpotParent_Office_OfficeId] FOREIGN KEY ([OfficeId]) REFERENCES [Office] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OnSpotParent_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_OnSpotParent_OfficeId] ON [OnSpotParent] ([OfficeId]);

GO

CREATE INDEX [IX_OnSpotParent_UserId] ON [OnSpotParent] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210121165421_OnSpotParent', N'2.2.6-servicing-10079');

GO

ALTER TABLE [CustomerChoiceV2] ADD [OnSpotParentId] bigint NULL DEFAULT CAST(0 AS bigint);

GO

CREATE INDEX [IX_CustomerChoiceV2_OnSpotParentId] ON [CustomerChoiceV2] ([OnSpotParentId]);

GO

ALTER TABLE [CustomerChoiceV2] ADD CONSTRAINT [FK_CustomerChoiceV2_OnSpotParent_OnSpotParentId] FOREIGN KEY ([OnSpotParentId]) REFERENCES [OnSpotParent] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210121165818_CustomerChoiceOnSpot', N'2.2.6-servicing-10079');

GO

