DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MealType]') AND [c].[name] = N'PreOrderLastTime');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [MealType] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [MealType] DROP COLUMN [PreOrderLastTime];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190522080330_Delete_PreOrderLastTime_MealType', N'2.1.11-servicing-32099');

GO

ALTER TABLE [MealType] ADD [PreOrderLastTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190522081002_Add_PreOrderLastTime_MealType_V2', N'2.1.11-servicing-32099');

GO

ALTER TABLE [WarehouseStorage] ADD [PurchaseRate] float NOT NULL DEFAULT 0E0;

GO

ALTER TABLE [WarehouseStorage] ADD [TotalPurchasePrice] float NOT NULL DEFAULT 0E0;

GO

ALTER TABLE [RemainingBalanceAndWeightedPriceCalculation] ADD [Date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190523063900_AddedFieldInWarehouseAndRemainingBalance', N'2.1.11-servicing-32099');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WarehouseStorage]') AND [c].[name] = N'LastModifiedDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [WarehouseStorage] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [WarehouseStorage] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UnitType]') AND [c].[name] = N'LastModifiedDate');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [UnitType] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [UnitType] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StoreOutItemRecipe]') AND [c].[name] = N'LastModifiedDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [StoreOutItemRecipe] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [StoreOutItemRecipe] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StoreOutItemCategory]') AND [c].[name] = N'LastModifiedDate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [StoreOutItemCategory] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [StoreOutItemCategory] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StoreOutItem]') AND [c].[name] = N'LastModifiedDate');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [StoreOutItem] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [StoreOutItem] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StoreInItemCategory]') AND [c].[name] = N'LastModifiedDate');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [StoreInItemCategory] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [StoreInItemCategory] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[StoreInItem]') AND [c].[name] = N'LastModifiedDate');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [StoreInItem] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [StoreInItem] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SetMenuDetails]') AND [c].[name] = N'LastModifiedDate');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [SetMenuDetails] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [SetMenuDetails] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SetMenu]') AND [c].[name] = N'LastModifiedDate');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [SetMenu] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [SetMenu] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RemainingBalanceAndWeightedPriceCalculation]') AND [c].[name] = N'LastModifiedDate');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [RemainingBalanceAndWeightedPriceCalculation] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [RemainingBalanceAndWeightedPriceCalculation] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PreOrderSchedule]') AND [c].[name] = N'LastModifiedDate');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [PreOrderSchedule] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [PreOrderSchedule] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderHistory]') AND [c].[name] = N'LastModifiedDate');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [OrderHistory] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [OrderHistory] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MealType]') AND [c].[name] = N'LastModifiedDate');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [MealType] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [MealType] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ExtraItem]') AND [c].[name] = N'LastModifiedDate');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [ExtraItem] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [ExtraItem] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BillHistory]') AND [c].[name] = N'LastModifiedDate');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [BillHistory] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [BillHistory] ALTER COLUMN [LastModifiedDate] datetime2 NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190725084318_changeNullabbe', N'2.1.11-servicing-32099');

GO

ALTER TABLE [OrderHistory] ADD [ForwardRemarks] nvarchar(max) NULL;

GO

ALTER TABLE [OrderHistory] ADD [IsForwardedToOffice] bit NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729063828_[Attributes]', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729064642_[MaitenanceTable]', N'2.1.11-servicing-32099');

GO

CREATE TABLE [MaintenanceBillHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [Year] bigint NOT NULL,
    [Month] int NOT NULL,
    [BillAmount] float NOT NULL,
    [Remarks] nvarchar(max) NULL,
    CONSTRAINT [PK_MaintenanceBillHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MaintenanceBillHistory_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_MaintenanceBillHistory_UserId] ON [MaintenanceBillHistory] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729072100_PostMigration', N'2.1.11-servicing-32099');

GO

ALTER TABLE [PreOrderSchedule] ADD [SetMenuId] bigint NULL;

GO

CREATE INDEX [IX_PreOrderSchedule_SetMenuId] ON [PreOrderSchedule] ([SetMenuId]);

GO

ALTER TABLE [PreOrderSchedule] ADD CONSTRAINT [FK_PreOrderSchedule_SetMenu_SetMenuId] FOREIGN KEY ([SetMenuId]) REFERENCES [SetMenu] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190818071835_PreOrder', N'2.1.11-servicing-32099');

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ExtraItem]') AND [c].[name] = N'SetMenuId');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [ExtraItem] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [ExtraItem] ALTER COLUMN [SetMenuId] bigint NULL;

GO

ALTER TABLE [ExtraItem] ADD [MealTypeId] bigint NULL;

GO

ALTER TABLE [ExtraItem] ADD [MenuDate] datetime2 NULL;

GO

CREATE INDEX [IX_ExtraItem_MealTypeId] ON [ExtraItem] ([MealTypeId]);

GO

ALTER TABLE [ExtraItem] ADD CONSTRAINT [FK_ExtraItem_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190819085625_ExtraItem', N'2.1.11-servicing-32099');

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ExtraItem]') AND [c].[name] = N'MenuDate');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [ExtraItem] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [ExtraItem] ALTER COLUMN [MenuDate] datetime2 NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190819091105_date', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190819131204_xbdf', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190819133126_db', N'2.1.11-servicing-32099');

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderHistory]') AND [c].[name] = N'SetMenuId');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [OrderHistory] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [OrderHistory] ALTER COLUMN [SetMenuId] bigint NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190819135551_OrderHistory Update', N'2.1.11-servicing-32099');

GO

ALTER TABLE [StoreOutItem] ADD [Day] int NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190821060725_Add Day', N'2.1.11-servicing-32099');

GO

ALTER TABLE [StoreOutItem] ADD [MealTypeId] int NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190821061739_Meal Added', N'2.1.11-servicing-32099');

GO

ALTER TABLE [ExtraItem] ADD [Day] int NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190821081706_Day In ExtraItem', N'2.1.11-servicing-32099');

GO

ALTER TABLE [SetMenu] ADD [Day] int NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190821105630_updt', N'2.1.11-servicing-32099');

GO

CREATE TABLE [OrderType] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_OrderType] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Period] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [From] datetime2 NOT NULL,
    [To] datetime2 NOT NULL,
    [Active] bit NOT NULL,
    [MealTypeId] bigint NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_Period] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Period_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Period_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CustomerChoice] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Date] datetime2 NOT NULL,
    [SetMenuId] bigint NULL,
    [ExtraItemId] bigint NULL,
    [OrderTypeId] bigint NOT NULL,
    [UserId] nvarchar(450) NULL,
    [quantity] float NOT NULL,
    CONSTRAINT [PK_CustomerChoice] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerChoice_ExtraItem_ExtraItemId] FOREIGN KEY ([ExtraItemId]) REFERENCES [ExtraItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoice_OrderType_OrderTypeId] FOREIGN KEY ([OrderTypeId]) REFERENCES [OrderType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoice_SetMenu_SetMenuId] FOREIGN KEY ([SetMenuId]) REFERENCES [SetMenu] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoice_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_CustomerChoice_ExtraItemId] ON [CustomerChoice] ([ExtraItemId]);

GO

CREATE INDEX [IX_CustomerChoice_OrderTypeId] ON [CustomerChoice] ([OrderTypeId]);

GO

CREATE INDEX [IX_CustomerChoice_SetMenuId] ON [CustomerChoice] ([SetMenuId]);

GO

CREATE INDEX [IX_CustomerChoice_UserId] ON [CustomerChoice] ([UserId]);

GO

CREATE INDEX [IX_Period_MealTypeId] ON [Period] ([MealTypeId]);

GO

CREATE INDEX [IX_Period_UserId] ON [Period] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190919071628_c', N'2.1.11-servicing-32099');

GO

ALTER TABLE [CustomerChoice] ADD [MealTypeId] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

CREATE INDEX [IX_CustomerChoice_MealTypeId] ON [CustomerChoice] ([MealTypeId]);

GO

ALTER TABLE [CustomerChoice] ADD CONSTRAINT [FK_CustomerChoice_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190922124854_b', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190930055507_new mess', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191023065800_ExtraItemAddition', N'2.1.11-servicing-32099');

GO

ALTER TABLE [SetMenuDetails] ADD [ExtraItemId] bigint NULL;

GO

CREATE INDEX [IX_SetMenuDetails_ExtraItemId] ON [SetMenuDetails] ([ExtraItemId]);

GO

ALTER TABLE [SetMenuDetails] ADD CONSTRAINT [FK_SetMenuDetails_ExtraItem_ExtraItemId] FOREIGN KEY ([ExtraItemId]) REFERENCES [ExtraItem] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191024054329_Testing', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191030133206_Custom', N'2.1.11-servicing-32099');

GO

CREATE TABLE [MenuItem] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Day] int NOT NULL,
    [Price] float NOT NULL,
    [MealTypeId] bigint NULL,
    [ExtraItemId] bigint NULL,
    CONSTRAINT [PK_MenuItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MenuItem_ExtraItem_ExtraItemId] FOREIGN KEY ([ExtraItemId]) REFERENCES [ExtraItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_MenuItem_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_MenuItem_ExtraItemId] ON [MenuItem] ([ExtraItemId]);

GO

CREATE INDEX [IX_MenuItem_MealTypeId] ON [MenuItem] ([MealTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191105091208_try', N'2.1.11-servicing-32099');

GO

ALTER TABLE [StoreOutItem] ADD [Price] float NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191112105945_STO_Price', N'2.1.11-servicing-32099');

GO

CREATE TABLE [OrderHistoryVr2] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [SetMenuId] bigint NULL,
    [MealTypeId] bigint NOT NULL,
    [StoreOutItemId] bigint NULL,
    [OrderTypeId] bigint NULL,
    [UnitOrdered] float NOT NULL,
    [OrderAmount] float NOT NULL,
    [IsPreOrder] bit NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [IsForwardedToOffice] bit NULL,
    [ForwardRemarks] nvarchar(max) NULL,
    CONSTRAINT [PK_OrderHistoryVr2] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderHistoryVr2_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderHistoryVr2_OrderType_OrderTypeId] FOREIGN KEY ([OrderTypeId]) REFERENCES [OrderType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderHistoryVr2_SetMenu_SetMenuId] FOREIGN KEY ([SetMenuId]) REFERENCES [SetMenu] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderHistoryVr2_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderHistoryVr2_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_OrderHistoryVr2_MealTypeId] ON [OrderHistoryVr2] ([MealTypeId]);

GO

CREATE INDEX [IX_OrderHistoryVr2_OrderTypeId] ON [OrderHistoryVr2] ([OrderTypeId]);

GO

CREATE INDEX [IX_OrderHistoryVr2_SetMenuId] ON [OrderHistoryVr2] ([SetMenuId]);

GO

CREATE INDEX [IX_OrderHistoryVr2_StoreOutItemId] ON [OrderHistoryVr2] ([StoreOutItemId]);

GO

CREATE INDEX [IX_OrderHistoryVr2_UserId] ON [OrderHistoryVr2] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191113101706_lt', N'2.1.11-servicing-32099');

GO

CREATE TABLE [CustomerChoiceV2] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Date] datetime2 NOT NULL,
    [SetMenuId] bigint NULL,
    [ExtraItemId] bigint NULL,
    [OrderTypeId] bigint NOT NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    [quantity] float NOT NULL,
    CONSTRAINT [PK_CustomerChoiceV2] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerChoiceV2_ExtraItem_ExtraItemId] FOREIGN KEY ([ExtraItemId]) REFERENCES [ExtraItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoiceV2_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoiceV2_OrderType_OrderTypeId] FOREIGN KEY ([OrderTypeId]) REFERENCES [OrderType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoiceV2_SetMenu_SetMenuId] FOREIGN KEY ([SetMenuId]) REFERENCES [SetMenu] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerChoiceV2_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_CustomerChoiceV2_ExtraItemId] ON [CustomerChoiceV2] ([ExtraItemId]);

GO

CREATE INDEX [IX_CustomerChoiceV2_MealTypeId] ON [CustomerChoiceV2] ([MealTypeId]);

GO

CREATE INDEX [IX_CustomerChoiceV2_OrderTypeId] ON [CustomerChoiceV2] ([OrderTypeId]);

GO

CREATE INDEX [IX_CustomerChoiceV2_SetMenuId] ON [CustomerChoiceV2] ([SetMenuId]);

GO

CREATE INDEX [IX_CustomerChoiceV2_UserId] ON [CustomerChoiceV2] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191114082413_sa', N'2.1.11-servicing-32099');

GO

