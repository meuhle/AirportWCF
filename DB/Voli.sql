USE Aviazione
GO
CREATE TABLE Voli (
	Id_volo varchar(17) primary key NOT NULL,
	Tratta varchar(6) FOREIGN KEY REFERENCES  Tratte(Id_tratta) NOT NULL,
	Data date NOT NULL,
	Prezzo int NOT NULL,     /*da prezzo base si calcola +20% business e +50% first*/ 
)

GO
CREATE PROCEDURE dbo.AddVoli
	@pTratta varchar(9),
	@pData date,
	@pPrezzo int ,
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON    
    BEGIN TRY
		DECLARE @pintdata varchar(8)
		DECLARE @pyear int
		DECLARE @pmonth int
		DECLARE @pday int
		SET @pyear = (SELECT YEAR(@pData))
		SET @pmonth = (SELECT MONTH(@pData))
		SET @pday = (SELECT DAY(@pData))
		SET @pintdata = (SELECT CONCAT(@pyear,@pmonth,@pday))
		DECLARE @pId_Volo varchar(17)
		SET @pId_Volo = (SELECT CONCAT (@pTratta,@pintdata))
        INSERT INTO dbo.[Voli2] (Id_volo,Tratta,Data,Prezzo)
        VALUES(@pId_volo,@pTratta,@pData,@pPrezzo)
       SET @responseMessage='Success'
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO

	