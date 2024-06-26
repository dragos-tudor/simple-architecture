USE [#database]

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
    [CorrelationId] nvarchar(24) NULL,

    [FailureMessage] nvarchar(max) NULL,
    [FailureCounter] TINYINT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([MessageId])
);
END;

IF OBJECT_ID(N'[PhoneNumbers]') IS NULL
BEGIN
CREATE TABLE [PhoneNumbers] (
    [Number] BIGINT NOT NULL,
    [CountryCode] SMALLINT NOT NULL,
    [Extension] SMALLINT NULL,
    [NumberType] TINYINT NOT NULL,
    [ContactId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY ([CountryCode], [Number]),
    CONSTRAINT [FK_PhoneNumbers_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Contacts] ([ContactId])
);

CREATE INDEX [IX_PhoneNumbers_ContactId] ON [PhoneNumbers] ([ContactId]);
END;

-- TODO add migration record.