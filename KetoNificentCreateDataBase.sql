USE master
GO
CREATE DATABASE KetoNificent
GO
USE KetoNificent
GO
CREATE SCHEMA Keto
GO
CREATE TABLE Keto.ProductEntity --(Final)Product
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (10000,1),
    [Name] VARCHAR,
    [User] INT NOT NULL FOREIGN KEY REFERENCES KETO.UserEntity(UserId) --stretch goal
);
GO
CREATE TABLE Keto.IngredentEntity
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [NCarb] NVARCHAR,
    [NCarbCt] INT,
    [Fat] INT,
    [Protein] INT,
    [DefaultMeasurement] VARCHAR,
    [DefaultAmount] INT
);
GO
CREATE TABLE Keto.ServingEntity --joining table
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (1,1),
    [Measurement] VARCHAR,
    [Amount] INT,
    [IngredientId] INT FOREIGN KEY REFERENCES KETO.IngredentEntity(Id),
    [ProductId] INT FOREIGN KEY REFERENCES KETO.ProductEntity(Id)
);
GO
USE KetoNificent
GO
CREATE TABLE Keto.UserEntity 
(
    [UserId] int NOT NULL PRIMARY KEY IDENTITY (100,1),
    [Name] VARCHAR,
    [Password] VARCHAR,
    [DateCreated] int    
);
GO