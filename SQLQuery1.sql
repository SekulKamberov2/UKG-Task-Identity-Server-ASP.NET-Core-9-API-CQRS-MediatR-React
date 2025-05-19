IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'IdentityServerDB') BEGIN CREATE DATABASE IdentityServerDB; END GO

USE IdentityServerDB; GO

BEGIN TRY BEGIN TRANSACTION;

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(255) NOT NULL UNIQUE,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        PhoneNumber NVARCHAR(256),
        DateCreated DATETIME DEFAULT GETDATE()
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Roles' AND xtype='U')
BEGIN
    CREATE TABLE Roles (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL UNIQUE,
        Description NVARCHAR(500),
        DateCreated DATETIME DEFAULT GETDATE()
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRoles' AND xtype='U')
BEGIN
    CREATE TABLE UserRoles (
        UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        RoleId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
        FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE,
        CONSTRAINT UC_UserRole UNIQUE (UserId, RoleId)
    );
END

SET IDENTITY_INSERT Roles ON;

IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = 1)
BEGIN
    INSERT INTO Roles (Id, Name, Description)
    VALUES (1, 'EMPLOYEE', 'Basic employee role');
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = 2)
BEGIN
    INSERT INTO Roles (Id, Name, Description)
    VALUES (2, 'MANAGER', 'Manager role');
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = 3)
BEGIN
    INSERT INTO Roles (Id, Name, Description)
    VALUES (3, 'HR ADMIN', 'Human Resources Admin role');
END

SET IDENTITY_INSERT Roles OFF;

COMMIT TRANSACTION;

END TRY BEGIN CATCH ROLLBACK TRANSACTION;

DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
RAISERROR(@ErrorMessage, 16, 1);

END CATCH;