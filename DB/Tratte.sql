USE Aviazione
GO
CREATE TABLE Tratte(
	Id_Tratta varchar(6) PRIMARY KEY NOT NULL,
	Aeroporto1 varchar(3) FOREIGN KEY REFERENCES Aeroporti(Codice) NOT NULL,
	Aeroporto2 varchar(3) FOREIGN KEY REFERENCES Aeroporti(Codice) NOT NULL,
	Distance float NOT NULL,
		 
)
GO

CREATE PROCEDURE dbo.AddTratte	
	@pAeroport1 varchar(3),
	@pAeroport2 varchar(3),
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
		DECLARE @pCoord1 geography
		SET @pCoord1 = (SELECT Aeroporti.Coord FROM Aeroporti WHERE Aeroporti.Codice= @pAeroport1)
		DECLARE @pCoord2 geography
		SET @pCoord2 = (SELECT Aeroporti.Coord FROM Aeroporti WHERE Aeroporti.Codice= @pAeroport2)
		DECLARE @pDistance float
		SET @pDistance = (SELECT @pCoord1.STDistance(@pCoord2)/1000)
		DECLARE @pId_tratta varchar(6)
		SET @pId_Tratta = (SELECT CONCAT (@pAeroport1,@pAeroport2))
        INSERT INTO dbo.[Tratte] (Id_Tratta,Aeroporto1,Aeroporto2,Distance)
        VALUES(@pId_tratta,@pAeroport1,@pAeroport2,@pDistance)
       SET @responseMessage='Success'
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO
