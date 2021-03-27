ALTER TABLE [AspNetUsers] ADD [BUPNumber] nvarchar(max) NULL;

GO

ALTER TABLE [AspNetUsers] ADD [EmployeeRank] int NULL;

GO

ALTER TABLE [AspNetUsers] ADD [OfficeName] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210213194422_UserModify', N'2.2.6-servicing-10079');

GO

ALTER TABLE [OnSpotParent] ADD [IsApproved] bit NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210220194548_Approve', N'2.2.6-servicing-10079');

GO

