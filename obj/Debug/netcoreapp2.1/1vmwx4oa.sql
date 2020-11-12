CREATE TABLE [DailySetMenu] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [SetMenuDate] datetime2 NULL,
    [Day] int NOT NULL,
    [MealTypeId] bigint NOT NULL,
    [SetMenuId] bigint NOT NULL,
    CONSTRAINT [PK_DailySetMenu] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DailySetMenu_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DailySetMenu_SetMenu_SetMenuId] FOREIGN KEY ([SetMenuId]) REFERENCES [SetMenu] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_DailySetMenu_MealTypeId] ON [DailySetMenu] ([MealTypeId]);

GO

CREATE INDEX [IX_DailySetMenu_SetMenuId] ON [DailySetMenu] ([SetMenuId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191128082136_DailySetMenuCreation', N'2.1.11-servicing-32099');

GO

CREATE TABLE [AccessoryBill] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Name] nvarchar(max) NULL,
    [Active] bit NOT NULL,
    [MinMeal] int NULL,
    [CostMinMeal] float NULL,
    [DefaultCost] float NOT NULL,
    CONSTRAINT [PK_AccessoryBill] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ConsumerMonthlyBillRecord] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [Month] int NOT NULL,
    [Year] int NOT NULL,
    [TotalAmount] float NOT NULL,
    [IsPaid] bit NOT NULL,
    CONSTRAINT [PK_ConsumerMonthlyBillRecord] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerMonthlyBillRecord_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ConsumerMonthlyBillRecord_UserId] ON [ConsumerMonthlyBillRecord] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191202090957_Accessory_MonthlyRecord', N'2.1.11-servicing-32099');

GO

