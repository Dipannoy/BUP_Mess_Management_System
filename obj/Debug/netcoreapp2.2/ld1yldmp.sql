CREATE TABLE [ConsumerMealWiseExtrachit] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [OrderTypeId] bigint NOT NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    [StoreOutItemId] bigint NOT NULL,
    [Remarks] nvarchar(max) NULL,
    [Attribute1] nvarchar(max) NULL,
    [Attribute2] nvarchar(max) NULL,
    [Attribute3] nvarchar(max) NULL,
    [quantity] float NULL,
    CONSTRAINT [PK_ConsumerMealWiseExtrachit] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerMealWiseExtrachit_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerMealWiseExtrachit_OrderType_OrderTypeId] FOREIGN KEY ([OrderTypeId]) REFERENCES [OrderType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerMealWiseExtrachit_StoreOutItem_StoreOutItemId] FOREIGN KEY ([StoreOutItemId]) REFERENCES [StoreOutItem] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerMealWiseExtrachit_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ConsumerMealWiseExtrachit_MealTypeId] ON [ConsumerMealWiseExtrachit] ([MealTypeId]);

GO

CREATE INDEX [IX_ConsumerMealWiseExtrachit_OrderTypeId] ON [ConsumerMealWiseExtrachit] ([OrderTypeId]);

GO

CREATE INDEX [IX_ConsumerMealWiseExtrachit_StoreOutItemId] ON [ConsumerMealWiseExtrachit] ([StoreOutItemId]);

GO

CREATE INDEX [IX_ConsumerMealWiseExtrachit_UserId] ON [ConsumerMealWiseExtrachit] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210125200029_ConsumerMealExtraChit', N'2.2.6-servicing-10079');

GO

ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [FK_ConsumerMealWiseExtrachit_MealType_MealTypeId];

GO

ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [FK_ConsumerMealWiseExtrachit_OrderType_OrderTypeId];

GO

ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [FK_ConsumerMealWiseExtrachit_AspNetUsers_UserId];

GO

DROP INDEX [IX_ConsumerMealWiseExtrachit_MealTypeId] ON [ConsumerMealWiseExtrachit];

GO

DROP INDEX [IX_ConsumerMealWiseExtrachit_UserId] ON [ConsumerMealWiseExtrachit];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerMealWiseExtrachit]') AND [c].[name] = N'Attribute1');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ConsumerMealWiseExtrachit] DROP COLUMN [Attribute1];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerMealWiseExtrachit]') AND [c].[name] = N'Attribute2');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ConsumerMealWiseExtrachit] DROP COLUMN [Attribute2];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerMealWiseExtrachit]') AND [c].[name] = N'Attribute3');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ConsumerMealWiseExtrachit] DROP COLUMN [Attribute3];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerMealWiseExtrachit]') AND [c].[name] = N'MealTypeId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ConsumerMealWiseExtrachit] DROP COLUMN [MealTypeId];

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerMealWiseExtrachit]') AND [c].[name] = N'Remarks');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [ConsumerMealWiseExtrachit] DROP COLUMN [Remarks];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerMealWiseExtrachit]') AND [c].[name] = N'UserId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerMealWiseExtrachit] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [ConsumerMealWiseExtrachit] DROP COLUMN [UserId];

GO

EXEC sp_rename N'[ConsumerMealWiseExtrachit].[OrderTypeId]', N'ConsumerMealWiseExtraChitParentId', N'COLUMN';

GO

EXEC sp_rename N'[ConsumerMealWiseExtrachit].[IX_ConsumerMealWiseExtrachit_OrderTypeId]', N'IX_ConsumerMealWiseExtrachit_ConsumerMealWiseExtraChitParentId', N'INDEX';

GO

CREATE TABLE [ConsumerMealWiseExtraChitParent] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [OrderTypeId] bigint NOT NULL,
    [UserId] nvarchar(450) NULL,
    [MealTypeId] bigint NOT NULL,
    [Remarks] nvarchar(max) NULL,
    [Attribute1] nvarchar(max) NULL,
    [Attribute2] nvarchar(max) NULL,
    [Attribute3] nvarchar(max) NULL,
    CONSTRAINT [PK_ConsumerMealWiseExtraChitParent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerMealWiseExtraChitParent_MealType_MealTypeId] FOREIGN KEY ([MealTypeId]) REFERENCES [MealType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerMealWiseExtraChitParent_OrderType_OrderTypeId] FOREIGN KEY ([OrderTypeId]) REFERENCES [OrderType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerMealWiseExtraChitParent_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ConsumerMealWiseExtraChitParent_MealTypeId] ON [ConsumerMealWiseExtraChitParent] ([MealTypeId]);

GO

CREATE INDEX [IX_ConsumerMealWiseExtraChitParent_OrderTypeId] ON [ConsumerMealWiseExtraChitParent] ([OrderTypeId]);

GO

CREATE INDEX [IX_ConsumerMealWiseExtraChitParent_UserId] ON [ConsumerMealWiseExtraChitParent] ([UserId]);

GO

ALTER TABLE [ConsumerMealWiseExtrachit] ADD CONSTRAINT [FK_ConsumerMealWiseExtrachit_ConsumerMealWiseExtraChitParent_ConsumerMealWiseExtraChitParentId] FOREIGN KEY ([ConsumerMealWiseExtraChitParentId]) REFERENCES [ConsumerMealWiseExtraChitParent] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210128123048_ExtraChitParent', N'2.2.6-servicing-10079');

GO

CREATE TABLE [ConsumerBillParent] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [UserId] nvarchar(450) NULL,
    [Attribute1] nvarchar(max) NULL,
    [Attribute2] nvarchar(max) NULL,
    [Attribute3] nvarchar(max) NULL,
    CONSTRAINT [PK_ConsumerBillParent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerBillParent_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [PaymentMethod] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [MethodName] nvarchar(max) NULL,
    CONSTRAINT [PK_PaymentMethod] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ConsumerPaymentInfo] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [PaymentMethodId] bigint NOT NULL,
    [ConsumerBillParentId] bigint NOT NULL,
    [BankName] nvarchar(max) NULL,
    [AccountNumber] nvarchar(max) NULL,
    [MobileNumber] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Attribute1] nvarchar(max) NULL,
    [Attribute2] nvarchar(max) NULL,
    [Attribute3] nvarchar(max) NULL,
    CONSTRAINT [PK_ConsumerPaymentInfo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerPaymentInfo_ConsumerBillParent_ConsumerBillParentId] FOREIGN KEY ([ConsumerBillParentId]) REFERENCES [ConsumerBillParent] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerPaymentInfo_PaymentMethod_PaymentMethodId] FOREIGN KEY ([PaymentMethodId]) REFERENCES [PaymentMethod] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ConsumerBillHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [ConsumerBillParentId] bigint NOT NULL,
    [ConsumerPaymentInfoId] bigint NOT NULL,
    [PaymentDate] datetime2 NOT NULL,
    [PaymentAmount] float NOT NULL,
    [Due] float NOT NULL,
    [IsPartial] bit NOT NULL,
    [TransactionID] nvarchar(max) NULL,
    [ReceivedBy] nvarchar(max) NULL,
    [Attribute1] nvarchar(max) NULL,
    [Attribute2] nvarchar(max) NULL,
    CONSTRAINT [PK_ConsumerBillHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerBillHistory_ConsumerBillParent_ConsumerBillParentId] FOREIGN KEY ([ConsumerBillParentId]) REFERENCES [ConsumerBillParent] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConsumerBillHistory_ConsumerPaymentInfo_ConsumerPaymentInfoId] FOREIGN KEY ([ConsumerPaymentInfoId]) REFERENCES [ConsumerPaymentInfo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ConsumerBillHistory_ConsumerBillParentId] ON [ConsumerBillHistory] ([ConsumerBillParentId]);

GO

CREATE INDEX [IX_ConsumerBillHistory_ConsumerPaymentInfoId] ON [ConsumerBillHistory] ([ConsumerPaymentInfoId]);

GO

CREATE INDEX [IX_ConsumerBillParent_UserId] ON [ConsumerBillParent] ([UserId]);

GO

CREATE INDEX [IX_ConsumerPaymentInfo_ConsumerBillParentId] ON [ConsumerPaymentInfo] ([ConsumerBillParentId]);

GO

CREATE INDEX [IX_ConsumerPaymentInfo_PaymentMethodId] ON [ConsumerPaymentInfo] ([PaymentMethodId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210129064317_NewBillTask', N'2.2.6-servicing-10079');

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ConsumerBillHistory]') AND [c].[name] = N'TransactionID');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ConsumerBillHistory] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [ConsumerBillHistory] DROP COLUMN [TransactionID];

GO

ALTER TABLE [ConsumerPaymentInfo] ADD [TransactionID] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210129181203_RemoveTrans', N'2.2.6-servicing-10079');

GO

ALTER TABLE [ConsumerBillHistory] ADD [Attachment] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210201094557_Attachment', N'2.2.6-servicing-10079');

GO

CREATE TABLE [ConsumerPaymentAttachment] (
    [Id] bigint NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModifiedDate] datetime2 NOT NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [RowVersion] rowversion NULL,
    [ConsumerBillParentId] bigint NOT NULL,
    [UploadDate] datetime2 NOT NULL,
    [Remarks] nvarchar(max) NULL,
    [Attachment] nvarchar(max) NULL,
    [Amount] float NULL,
    [EntryDone] bit NOT NULL,
    [Attribute1] nvarchar(max) NULL,
    [Attribute2] nvarchar(max) NULL,
    CONSTRAINT [PK_ConsumerPaymentAttachment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConsumerPaymentAttachment_ConsumerBillParent_ConsumerBillParentId] FOREIGN KEY ([ConsumerBillParentId]) REFERENCES [ConsumerBillParent] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ConsumerPaymentAttachment_ConsumerBillParentId] ON [ConsumerPaymentAttachment] ([ConsumerBillParentId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210201142528_ConsumerAttachment', N'2.2.6-servicing-10079');

GO

ALTER TABLE [ConsumerPaymentAttachment] ADD [ConsumerPaymentInfoId] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

CREATE INDEX [IX_ConsumerPaymentAttachment_ConsumerPaymentInfoId] ON [ConsumerPaymentAttachment] ([ConsumerPaymentInfoId]);

GO

ALTER TABLE [ConsumerPaymentAttachment] ADD CONSTRAINT [FK_ConsumerPaymentAttachment_ConsumerPaymentInfo_ConsumerPaymentInfoId] FOREIGN KEY ([ConsumerPaymentInfoId]) REFERENCES [ConsumerPaymentInfo] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210201211804_AttachmentExtend', N'2.2.6-servicing-10079');

GO

