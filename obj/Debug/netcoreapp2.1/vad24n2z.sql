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

