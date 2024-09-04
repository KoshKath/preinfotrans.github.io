IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [Brands] (
        [Id] int NOT NULL IDENTITY,
        [BrandName] nvarchar(max) NULL,
        [BrandCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Brands] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [Carrier] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Unp] nvarchar(9) NULL,
        [Country] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [EpiId] int NOT NULL,
        CONSTRAINT [PK_Carrier] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [Countries] (
        [Id] int NOT NULL IDENTITY,
        [CountryName] nvarchar(max) NULL,
        [CountryCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [Epi] (
        [Id] int NOT NULL IDENTITY,
        [CreatorId] nvarchar(max) NULL,
        [DirectionIn] bit NOT NULL,
        [TransportationType] int NULL,
        [DocName] nvarchar(7) NULL,
        [DocDate] datetime2 NOT NULL,
        [RegEndDate] datetime2 NULL,
        [RegNumTDTS] nvarchar(max) NULL,
        [RegDateTime] datetime2 NULL,
        [RegNumOutTDTS] nvarchar(max) NULL,
        [Result] int NULL,
        [Route] nvarchar(max) NULL,
        [RouteCountry] nvarchar(max) NULL,
        [IsPassengers] int NOT NULL,
        [IsCrew] int NOT NULL,
        [IsSupplies] bit NOT NULL,
        [IsGoods] bit NOT NULL,
        [Targets] int NULL,
        [Description] nvarchar(max) NULL,
        [RegCompleteTDTS] nvarchar(max) NULL,
        [RegComleteDateTime] datetime2 NULL,
        [TemporaryInDate] datetime2 NULL,
        [SpareParts] nvarchar(max) NULL,
        [CancelReason] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [TsmpFormatedString] nvarchar(max) NULL,
        CONSTRAINT [PK_Epi] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [Owner] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Passport] nvarchar(max) NOT NULL,
        [IdNumber] nvarchar(14) NULL,
        [DateIssue] datetime2 NULL,
        [Country] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [EpiId] int NOT NULL,
        CONSTRAINT [PK_Owner] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [TsmpTypes] (
        [Id] int NOT NULL IDENTITY,
        [Code] int NOT NULL,
        [TypeCode] int NOT NULL,
        [TypeName] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_TsmpTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE TABLE [Tsmp] (
        [Id] int NOT NULL IDENTITY,
        [Brand] nvarchar(max) NULL,
        [Model] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [TypeCode] int NOT NULL,
        [RegNum] nvarchar(max) NOT NULL,
        [RegCountry] nvarchar(max) NOT NULL,
        [VinCode] nvarchar(17) NULL,
        [EpiDocName] nvarchar(max) NULL,
        [EpiId] int NULL,
        CONSTRAINT [PK_Tsmp] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tsmp_Epi_EpiId] FOREIGN KEY ([EpiId]) REFERENCES [Epi] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BrandCode', N'BrandName') AND [object_id] = OBJECT_ID(N'[Brands]'))
        SET IDENTITY_INSERT [Brands] ON;
    EXEC(N'INSERT INTO [Brands] ([Id], [BrandCode], [BrandName])
    VALUES (1, N''58'', N''BELAZ''),
    (2, N''70'', N''BMW''),
    (3, N''80'', N''BRIAB''),
    (4, N''128'', N''CITROEN''),
    (5, N''137'', N''DAF''),
    (6, N''138'', N''DAIHATSU''),
    (7, N''139'', N''DAIMLER''),
    (8, N''178'', N''FIAT''),
    (9, N''186'', N''FORD''),
    (10, N''200'', N''GEELY''),
    (11, N''201'', N''GENERAL MOTORS''),
    (12, N''202'', N''GENERIC''),
    (13, N''257'', N''HONDA''),
    (14, N''272'', N''HYUNDAI''),
    (15, N''274'', N''IKARBUS''),
    (16, N''291'', N''IVECO''),
    (17, N''298'', N''JEEP''),
    (18, N''329'', N''KIA''),
    (19, N''346'', N''LADA''),
    (20, N''370'', N''LIFAN''),
    (21, N''406'', N''MAZ''),
    (22, N''407'', N''MAZDA''),
    (23, N''455'', N''NISSAN''),
    (24, N''483'', N''PEUGEOT'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'BrandCode', N'BrandName') AND [object_id] = OBJECT_ID(N'[Brands]'))
        SET IDENTITY_INSERT [Brands] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] ON;
    EXEC(N'INSERT INTO [Countries] ([Id], [CountryCode], [CountryName])
    VALUES (1, N''AM'', N''АРМЕНИЯ''),
    (2, N''AT'', N''АВСТРИЯ''),
    (3, N''AZ'', N''АЗЕРБАЙДЖАН''),
    (4, N''BG'', N''БОЛГАРИЯ''),
    (5, N''BY'', N''БЕЛАРУСЬ''),
    (6, N''CH'', N''ШВЕЙЦАРИЯ''),
    (7, N''CN'', N''КИТАЙ''),
    (8, N''CZ'', N''ЧЕХИЯ''),
    (9, N''DE'', N''ГЕРМАНИЯ''),
    (10, N''EE'', N''ЭСТОНИЯ''),
    (11, N''FR'', N''ФРАНЦИЯ''),
    (12, N''GE'', N''ГРУЗИЯ''),
    (13, N''HU'', N''ВЕНГРИЯ''),
    (14, N''IT'', N''ИТАЛИЯ''),
    (15, N''JP'', N''ЯПОНИЯ''),
    (16, N''KG'', N''КЫРГЫЗСТАН''),
    (17, N''KZ'', N''КАЗАХСТАН''),
    (18, N''LT'', N''ЛИТВА'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] ON;
    EXEC(N'INSERT INTO [Countries] ([Id], [CountryCode], [CountryName])
    VALUES (19, N''LV'', N''ЛАТВИЯ''),
    (20, N''MD'', N''МОЛДОВА, РЕСПУБЛИКА''),
    (21, N''PL'', N''ПОЛЬША''),
    (22, N''PT'', N''ПОРТУГАЛИЯ''),
    (23, N''RU'', N''РОССИЯ''),
    (24, N''TM'', N''ТУРКМЕНИСТАН''),
    (25, N''TR'', N''ТУРЦИЯ''),
    (26, N''UA'', N''УКРАИНА'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CountryCode', N'CountryName') AND [object_id] = OBJECT_ID(N'[Countries]'))
        SET IDENTITY_INSERT [Countries] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CancelReason', N'CreatorId', N'Description', N'DirectionIn', N'DocDate', N'DocName', N'IsCrew', N'IsDeleted', N'IsGoods', N'IsPassengers', N'IsSupplies', N'RegComleteDateTime', N'RegCompleteTDTS', N'RegDateTime', N'RegEndDate', N'RegNumOutTDTS', N'RegNumTDTS', N'Result', N'Route', N'RouteCountry', N'SpareParts', N'Targets', N'TemporaryInDate', N'TransportationType', N'TsmpFormatedString') AND [object_id] = OBJECT_ID(N'[Epi]'))
        SET IDENTITY_INSERT [Epi] ON;
    EXEC(N'INSERT INTO [Epi] ([Id], [CancelReason], [CreatorId], [Description], [DirectionIn], [DocDate], [DocName], [IsCrew], [IsDeleted], [IsGoods], [IsPassengers], [IsSupplies], [RegComleteDateTime], [RegCompleteTDTS], [RegDateTime], [RegEndDate], [RegNumOutTDTS], [RegNumTDTS], [Result], [Route], [RouteCountry], [SpareParts], [Targets], [TemporaryInDate], [TransportationType], [TsmpFormatedString])
    VALUES (1, NULL, NULL, NULL, CAST(1 AS bit), ''2024-03-15T22:05:00.0000000'', N''0000001'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), NULL, N'''', NULL, NULL, N'''', N'''', 1, NULL, NULL, NULL, 2, NULL, NULL, N''AM65432''),
    (2, NULL, NULL, NULL, CAST(0 AS bit), ''2024-03-15T22:06:00.0000000'', N''0000002'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), NULL, N'''', NULL, NULL, N'''', N'''', 2, NULL, NULL, NULL, 3, NULL, NULL, N''RT5432/RT543''),
    (3, NULL, NULL, NULL, CAST(0 AS bit), ''2024-03-15T22:07:00.0000000'', N''0000003'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), NULL, N'''', NULL, NULL, N'''', N'''', 3, NULL, NULL, NULL, 3, NULL, NULL, N''AM65432''),
    (4, NULL, NULL, NULL, CAST(1 AS bit), ''2024-03-15T22:08:00.0000000'', N''0000004'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), NULL, N'''', ''2024-03-15T22:10:00.0000000'', NULL, N'''', N''11206604/150324/301234567'', 5, NULL, NULL, NULL, 0, NULL, NULL, N''AM65432/ВВ1232''),
    (5, NULL, NULL, NULL, CAST(1 AS bit), ''2024-03-15T22:09:00.0000000'', N''0000005'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), NULL, N'''', ''2024-03-15T22:11:00.0000000'', NULL, N'''', N'''', 6, NULL, NULL, NULL, 0, NULL, NULL, N''ВВ5555''),
    (6, NULL, NULL, NULL, CAST(0 AS bit), ''2024-03-15T22:10:00.0000000'', N''0000006'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), NULL, N'''', NULL, NULL, N'''', N'''', 4, NULL, NULL, NULL, 1, NULL, NULL, N''ВЕ12345678''),
    (7, NULL, NULL, NULL, CAST(0 AS bit), ''2024-03-15T22:11:00.0000000'', N''0000007'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), ''2024-03-15T22:25:00.0000000'', N'''', ''2024-03-15T22:20:00.0000000'', NULL, N'''', N''11206604/150324/307777777'', 7, NULL, NULL, NULL, 1, NULL, NULL, N''345МС00''),
    (8, NULL, NULL, NULL, CAST(1 AS bit), ''2024-03-15T22:13:00.0000000'', N''0000008'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), ''2024-03-15T22:41:00.0000000'', N'''', ''2024-03-15T22:30:00.0000000'', NULL, N'''', N''11206604/150324/307788888'', 9, NULL, NULL, NULL, 0, NULL, NULL, N''345МС34''),
    (9, NULL, NULL, NULL, CAST(1 AS bit), ''2024-03-15T22:13:00.0000000'', N''0000009'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), ''2024-03-15T22:50:00.0000000'', N'''', ''2024-03-15T22:31:00.0000000'', NULL, N''16412/31255555'', N''11206604/150324/306666666'', 8, NULL, NULL, NULL, 0, NULL, NULL, N''АА321321''),
    (10, NULL, NULL, NULL, CAST(1 AS bit), ''2024-03-15T22:14:00.0000000'', N''0000010'', 0, CAST(0 AS bit), CAST(0 AS bit), 0, CAST(0 AS bit), ''2024-03-15T22:23:00.0000000'', N''11206604/160323/301234567'', ''2024-03-15T22:20:00.0000000'', ''2024-03-16T08:00:00.0000000'', N''16412/31234567'', N''11206604/150324/306549888'', 8, NULL, NULL, NULL, 0, NULL, NULL, N''АА32100/ММ65432'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CancelReason', N'CreatorId', N'Description', N'DirectionIn', N'DocDate', N'DocName', N'IsCrew', N'IsDeleted', N'IsGoods', N'IsPassengers', N'IsSupplies', N'RegComleteDateTime', N'RegCompleteTDTS', N'RegDateTime', N'RegEndDate', N'RegNumOutTDTS', N'RegNumTDTS', N'Result', N'Route', N'RouteCountry', N'SpareParts', N'Targets', N'TemporaryInDate', N'TransportationType', N'TsmpFormatedString') AND [object_id] = OBJECT_ID(N'[Epi]'))
        SET IDENTITY_INSERT [Epi] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Brand', N'EpiDocName', N'EpiId', N'Model', N'RegCountry', N'RegNum', N'Type', N'TypeCode', N'VinCode') AND [object_id] = OBJECT_ID(N'[Tsmp]'))
        SET IDENTITY_INSERT [Tsmp] ON;
    EXEC(N'INSERT INTO [Tsmp] ([Id], [Brand], [EpiDocName], [EpiId], [Model], [RegCountry], [RegNum], [Type], [TypeCode], [VinCode])
    VALUES (1, N''SCANIA'', N''0000001'', NULL, N'''', N'''', N''AM65432'', N'''', 30, N''1FTCR10A4PTA70496''),
    (2, N''VOLVO'', N''0000002'', NULL, N'''', N'''', N''RT5432'', N'''', 30, N''1GCHK23U74F122056''),
    (3, N''LADA'', N''0000002'', NULL, N'''', N'''', N''RT543'', N'''', 30, N''JHLRM3H73CC097070''),
    (4, N''SCANIA'', N''0000003'', NULL, N'''', N'''', N''AM65432'', N'''', 30, N''1FTCR10A4PTA70496''),
    (5, N''SCANIA'', N''0000004'', NULL, N'''', N'''', N''AM65432'', N'''', 30, N''1FTCR10A4PTA70496''),
    (6, N''RENAULT'', N''0000004'', NULL, N'''', N'''', N''ВВ1232'', N'''', 30, N''1N4AL3AP7EN305245''),
    (7, N''МАЗ'', N''0000005'', NULL, N'''', N'''', N''ВВ5555'', N'''', 30, N''2C4GP54L35R273796''),
    (8, N''BELGEE'', N''0000006'', NULL, N'''', N'''', N''ВЕ12345678'', N'''', 30, N''2CNDL13F166195441''),
    (9, N''DONGFENG'', N''0000007'', NULL, N'''', N'''', N''345МС00'', N'''', 30, N''2B3KA43G47H859864''),
    (10, N''NISSAN'', N''0000008'', NULL, N'''', N'''', N''345МС34'', N'''', 30, N''2HSCESBR15C086125''),
    (11, N''LADA'', N''0000009'', NULL, N'''', N'''', N''АА321321'', N'''', 30, N''1HGCR2F59FA048733''),
    (12, N''LADA'', N''0000010'', NULL, N'''', N'''', N''АА32100'', N'''', 30, N''1J8HG58276C176514''),
    (13, N''КАМАЗ'', N''0000010'', NULL, N'''', N'''', N''ММ65432'', N'''', 30, N''2C4RDGCG9FR690732'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Brand', N'EpiDocName', N'EpiId', N'Model', N'RegCountry', N'RegNum', N'Type', N'TypeCode', N'VinCode') AND [object_id] = OBJECT_ID(N'[Tsmp]'))
        SET IDENTITY_INSERT [Tsmp] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'TypeCode', N'TypeName') AND [object_id] = OBJECT_ID(N'[TsmpTypes]'))
        SET IDENTITY_INSERT [TsmpTypes] ON;
    EXEC(N'INSERT INTO [TsmpTypes] ([Id], [Code], [Name], [TypeCode], [TypeName])
    VALUES (1, 100, N''водное судно'', 10, N''морской/речной транспорт''),
    (2, 203, N''электропоезд'', 20, N''железнодорожный транспорт''),
    (3, 204, N''тепловоз'', 20, N''железнодорожный транспорт''),
    (4, 210, N''цистерна'', 20, N''железнодорожный транспорт''),
    (5, 212, N''платформа'', 20, N''железнодорожный транспорт''),
    (6, 298, N''прочий вагон'', 20, N''железнодорожный транспорт''),
    (7, 303, N''грузовой автомобиль общего назначения'', 30, N''автодорожный транспорт''),
    (8, 306, N''автомобиль-тягач'', 30, N''автодорожный транспорт''),
    (9, 307, N''седельный тягач'', 30, N''автодорожный транспорт''),
    (10, 312, N''грузовой прицеп общего назначения'', 30, N''автодорожный транспорт''),
    (11, 313, N''специальный прицеп'', 30, N''автодорожный транспорт'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'TypeCode', N'TypeName') AND [object_id] = OBJECT_ID(N'[TsmpTypes]'))
        SET IDENTITY_INSERT [TsmpTypes] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'TypeCode', N'TypeName') AND [object_id] = OBJECT_ID(N'[TsmpTypes]'))
        SET IDENTITY_INSERT [TsmpTypes] ON;
    EXEC(N'INSERT INTO [TsmpTypes] ([Id], [Code], [Name], [TypeCode], [TypeName])
    VALUES (12, 319, N''грузовой полуприцеп общего назначения'', 30, N''автодорожный транспорт''),
    (13, 321, N''автобус общего назначения'', 30, N''автодорожный транспорт''),
    (14, 322, N''специальный автобус'', 30, N''автодорожный транспорт''),
    (15, 400, N''воздушное судно'', 40, N''воздушный транспорт''),
    (16, 901, N''контейнер'', 40, N''воздушный транспорт'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Code', N'Name', N'TypeCode', N'TypeName') AND [object_id] = OBJECT_ID(N'[TsmpTypes]'))
        SET IDENTITY_INSERT [TsmpTypes] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    CREATE INDEX [IX_Tsmp_EpiId] ON [Tsmp] ([EpiId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240613001344_EpiInit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240613001344_EpiInit', N'6.0.29');
END;
GO

COMMIT;
GO

