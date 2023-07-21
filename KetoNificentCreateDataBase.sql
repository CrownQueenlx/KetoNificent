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
    [UserId] INT NOT NULL FOREIGN KEY REFERENCES KETO.User(Id) -stretch goal
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
    [IngredentId] INT FOREIGN KEY REFERENCES KETO.Ingredent(Id),
    [ProductId] INT FOREIGN KEY REFERENCES KETO.PRODUCT(Id)
);
GO
CREATE TABLE Keto.UserEntity --(Final)Product
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (10000,1),
    [Name] VARCHAR,
    
);
GO
CREATE TABLE Keto.UserEntity --(Final)Product
(
    [UserId] int NOT NULL PRIMARY KEY IDENTITY (100,1),
    [Name] VARCHAR ,
    [Password] VARCHAR,
    [DateCreated] int
);
GO