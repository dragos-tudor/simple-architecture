USE #database

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

IF OBJECT_ID(N'[Contacts]') IS NULL
BEGIN
CREATE TABLE [Contacts] (
    [ContactId] uniqueidentifier NOT NULL,
    [ContactEmail] nvarchar(50) NOT NULL,
    [ContactName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([ContactId])
);
END;

IF OBJECT_ID(N'[Messages]') IS NULL
BEGIN
CREATE TABLE [Messages] (
    [MessageId] uniqueidentifier NOT NULL,
    [MessageType] nvarchar(150) NOT NULL,
    [MessageVersion] int NOT NULL,
    [MessageDate] datetime2 NOT NULL,
    [MessageContent] nvarchar(max) NOT NULL,
    [ParentId] uniqueidentifier NULL,
    [TraceId] nvarchar(24) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([MessageId])
);
END;

IF OBJECT_ID(N'[PhoneNumbers]') IS NULL
BEGIN
CREATE TABLE [PhoneNumbers] (
    [Number] nvarchar(9) NOT NULL,
    [CountryCode] nvarchar(4) NOT NULL,
    [Extension] nvarchar(5) NULL,
    [NumberType] tinyint NOT NULL,
    [ContactId] uniqueidentifier NULL,
    CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY ([CountryCode], [Number]),
    CONSTRAINT [FK_PhoneNumbers_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([ContactId])
);

CREATE INDEX [IX_PhoneNumbers_ContactId] ON [PhoneNumbers] ([ContactId]);
END;
