USE master
GO
CREATE DATABASE KetoNificent
GO
USE KetoNificent
GO
CREATE SCHEMA Keto
GO
CREATE TABLE Keto.Product --(Final)Product
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (10000,1),
    [Name] VARCHAR,
    -- [UserId] INT FOREIGN KEY REFERENCES Drink.User(Id) -stretch goal
);
GO
CREATE TABLE Keto.Ingredent
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [NCarb] INT,
    [Fat] INT,
    [Protein] INT,
    [DefaultMeasurment] VARCHAR,
    [DefaultAmount] INT
);
GO
CREATE TABLE Keto.Serving --joining table
(
    [Id] int NOT NULL PRIMARY KEY IDENTITY (1,1),
    [Measurement] VARCHAR,
    [Amount] INT,
    [IngredentId] INT FOREIGN KEY REFERENCES KETO.Ingredent(Id),
    [ProductId] INT FOREIGN KEY REFERENCES KETO.PRODUCT(Id)
);