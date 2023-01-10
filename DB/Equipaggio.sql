USE Aviazione
GO
CREATE TABLE Equipaggio(
	Volo varchar(17) FOREIGN KEY REFERENCES Voli(Id_volo),     /*MANCANO I NOT NULL*/
	Passaporto varchar(9) FOREIGN KEY REFERENCES Utenti(Passaporto),
	Ruolo varchar(20)
	PRIMARY KEY (Volo, Passaporto)
)
GO
CREATE PROCEDURE dbo.AddEquipaggio
	@pVolo varchar(6),
	@pPassaporto varchar(9),
	@pRuolo varchar(20)
AS
BEGIN
    SET NOCOUNT ON    
        INSERT INTO dbo.[Equipaggio] (Volo, Passaporto, Ruolo)
        VALUES(@pVolo,@pPassaporto,@pRuolo)
END
GO