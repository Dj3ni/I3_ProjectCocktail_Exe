CREATE TABLE [dbo].[User] (
    [User_Id]    UNIQUEIDENTIFIER NOT NULL  DEFAULT NEWID(),
    [First_Name] NVARCHAR  (64)   NOT NULL,
    [Last_Name]  NVARCHAR  (64)   NOT NULL,
    [Email]      NVARCHAR (320)   NOT NULL,
    [Password]   VARBINARY (64)   NOT NULL,
    [Salt]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt]  DATETIME2        NOT NULL  DEFAULT GETDATE(),
    [DisabledAt] DATETIME2, 
    [Role] NVARCHAR(8) NOT NULL DEFAULT 'User', 
    CONSTRAINT [PK_User] PRIMARY KEY ([User_Id]), 
    CONSTRAINT [UK_User_Email] UNIQUE ([Email]),
	CONSTRAINT [FK_User_Role] FOREIGN KEY ([Role]) REFERENCES [Role]([Role])
);

