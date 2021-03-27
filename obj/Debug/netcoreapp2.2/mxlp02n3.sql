ALTER TABLE [Office] ADD [PIMSOfficeId] int NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210309054615_PIMSOfficeID', N'2.2.6-servicing-10079');

GO

