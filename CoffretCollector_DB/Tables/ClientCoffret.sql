CREATE TABLE [dbo].[ClientCoffret]
(
	ClientId INT NOT NULL,
    CoffretId INT NOT NULL,
    Quantité INT NOT NULL,
    DateAchat DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (ClientId, CoffretId),
    FOREIGN KEY (ClientId) REFERENCES Client(Id),
    FOREIGN KEY (CoffretId) REFERENCES Coffret(Id)
)
