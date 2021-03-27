CREATE TABLE [NavigationMenu] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [Controller] nvarchar(max) NULL,
    [Action] nvarchar(max) NULL,
    [Name] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [ParentId] bigint NOT NULL,
    CONSTRAINT [PK_NavigationMenu] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [RoleMenu] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserIdentityRoleId] nvarchar(450) NULL,
    [NavigationMenuId] bigint NOT NULL,
    CONSTRAINT [PK_RoleMenu] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleMenu_NavigationMenu_NavigationMenuId] FOREIGN KEY ([NavigationMenuId]) REFERENCES [NavigationMenu] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RoleMenu_AspNetRoles_UserIdentityRoleId] FOREIGN KEY ([UserIdentityRoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_RoleMenu_NavigationMenuId] ON [RoleMenu] ([NavigationMenuId]);

GO

CREATE INDEX [IX_RoleMenu_UserIdentityRoleId] ON [RoleMenu] ([UserIdentityRoleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210208121951_RoleManagement', N'2.2.6-servicing-10079');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NavigationMenu]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [NavigationMenu] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [NavigationMenu] ALTER COLUMN [Name] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210209183736_ColumnUpdate', N'2.2.6-servicing-10079');

GO

ALTER TABLE [NavigationMenu] ADD [RouteVariable] nvarchar(max) NULL;

GO

ALTER TABLE [NavigationMenu] ADD [RouteVariableValue] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210209190706_NavMenuExtend', N'2.2.6-servicing-10079');

GO

CREATE TABLE [DailyOfferItem] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [StoreOutItemId] bigint NOT NULL,
    CONSTRAINT [PK_DailyOfferItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DailyOfferItem_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_DailyOfferItem_StoreOutItemId] ON [DailyOfferItem] ([StoreOutItemId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210211095235_DailyOffer', N'2.2.6-servicing-10079');

GO

ALTER TABLE [DailyOfferItem] ADD [Date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [DailyOfferItem] ADD [IsActive] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [DailyOfferItem] ADD [OrderLimit] bigint NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210211143400_DailyOfferExtend', N'2.2.6-servicing-10079');

GO

ALTER TABLE [CustomerChoiceV2] ADD [StoreOutItemId] bigint NULL;

GO

CREATE INDEX [IX_CustomerChoiceV2_StoreOutItemId] ON [CustomerChoiceV2] ([StoreOutItemId]);

GO

ALTER TABLE [CustomerChoiceV2] ADD CONSTRAINT [FK_CustomerChoiceV2_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210211185938_AddItemCustomerChoice', N'2.2.6-servicing-10079');

GO

ALTER TABLE [WarehouseStorage] ADD [StoreOutItemId] bigint NULL;

GO

CREATE INDEX [IX_WarehouseStorage_StoreOutItemId] ON [WarehouseStorage] ([StoreOutItemId]);

GO

ALTER TABLE [WarehouseStorage] ADD CONSTRAINT [FK_WarehouseStorage_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210212212236_AddSTOWarehouse', N'2.2.6-servicing-10079');

GO

