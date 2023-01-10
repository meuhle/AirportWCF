USE Aviazione
GO
CREATE TABLE Biglietti(
	PassaportoPasseggero varchar(9) FOREIGN KEY REFERENCES	Utenti(Passaporto) NOT NULL,
	CodVolo varchar(17)  FOREIGN KEY REFERENCES Voli(Id_volo) NOT NULL,
	Buyer  varchar(9) FOREIGN KEY REFERENCES	Utenti(Passaporto) NOT NULL,
	Classe int NULL,
	Prezzo int NOT NULL,
	PRIMARY KEY (PassaportoPasseggero,CodVolo)
)
GO
CREATE PROCEDURE dbo.AddBiglietti
	@pPasseggero varchar(9),
	@pVolo varchar(17),
	@pBuyer varchar(9),
	@pCLasse int = 0,   /*0 economy 1 business 2 first*/
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @pb int
	SELECT @pb =  v.Prezzo FROM Voli v WHERE v.Id_volo=@pVolo
	DECLARE @pf float
	SET @pf = @pb + ( @pb * 0.25 * @pCLasse)
    
    BEGIN TRY

        INSERT INTO dbo.Biglietti (PassaportoPasseggero,CodVolo,Buyer,Classe,Prezzo)
        VALUES(@pPasseggero,@pVolo,@pBuyer,@pCLasse,@pf)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
GO