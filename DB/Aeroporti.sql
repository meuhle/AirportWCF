USE Aviazione
GO
CREATE TABLE Aeroporti(
	Codice varchar(3) primary key NOT NULL,
	Stato varchar(20) NOT NULL,
	Citta varchar(20) NOT NULL,
	Nome varchar(100) NOT NULL,
	Coord geography NOT NULL /*calcola un po sbagliate le distanza 0.25% SCARTO*/
)
GO
CREATE PROCEDURE dbo.AddAeroporti  /* ha 1 argomento in piu che la tabella*/
	@pCodice varchar(3),
	@pStato varchar(20),
	@pCitta varchar(20),
	@pNome varchar(100),
	@plat float ,
	@plong float,
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO dbo.[Aeroporti] (Codice,Stato,Citta,Nome,Coord)
        VALUES(@pCodice,@pStato,@pCitta,@pNome,geography::Point(@plat,@plong,4326))
       SET @responseMessage='Success'
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO