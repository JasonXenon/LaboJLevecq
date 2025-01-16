﻿CREATE TABLE Coffret (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Titre NVARCHAR(255) NOT NULL,
    Bonus NVARCHAR(255) NULL,
    Prix DECIMAL(10, 2) NOT NULL,
    Quantite INT NOT NULL,
    Synopsis NVARCHAR(MAX) NULL,
    GenreId INT NOT NULL,
    AfficheUrl NVARCHAR(255) NULL,
    FOREIGN KEY (GenreId) REFERENCES Genre(Id)
);