CREATE DATABASE Aviazione
GO
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

CREATE TABLE Utenti(
	Passaporto varchar(9) primary key NOT NULL,
	Nome varchar(20)NOT NULL,
	Cognome varchar(20)NOT NULL,
	Datanascita date NOT NULL,
	Mail varchar(50) ,   
	PassBash binary(64) ,
	Salt UNIQUEIDENTIFIER,
	Tipo int NOT NULL,  /*0 ADMIN 1 CLIENTE 2 PASSEGGERO 3 CREW*/
	Credito float, 
)
GO

CREATE TABLE Tratte(
	Id_Tratta varchar(6) PRIMARY KEY NOT NULL,
	Aeroporto1 varchar(3) FOREIGN KEY REFERENCES Aeroporti(Codice) NOT NULL,
	Aeroporto2 varchar(3) FOREIGN KEY REFERENCES Aeroporti(Codice) NOT NULL,
	Distance float NOT NULL,
		 
)
GO

CREATE TABLE Voli (
	Id_volo varchar(17) primary key NOT NULL,
	Tratta varchar(6) FOREIGN KEY REFERENCES  Tratte(Id_tratta) NOT NULL,
	Data date NOT NULL,
	Prezzo float NOT NULL,     /*da prezzo base si calcola +20% business e +50% first*/ 
)
GO

CREATE TABLE Biglietti(
	PassaportoPasseggero varchar(9) FOREIGN KEY REFERENCES	Utenti(Passaporto) NOT NULL,
	CodVolo varchar(17)  FOREIGN KEY REFERENCES Voli(Id_volo) NOT NULL,
	Buyer  varchar(9) FOREIGN KEY REFERENCES	Utenti(Passaporto) NOT NULL,
	Classe int NULL,
	Prezzo float NOT NULL,
	PRIMARY KEY (PassaportoPasseggero,CodVolo)
)
GO

CREATE TABLE Equipaggio(
	Volo varchar(17) FOREIGN KEY REFERENCES Voli(Id_volo),     /*MANCANO I NOT NULL*/
	Passaporto varchar(9) FOREIGN KEY REFERENCES Utenti(Passaporto),
	Ruolo varchar(20)
	PRIMARY KEY (Volo, Passaporto)
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

CREATE PROCEDURE dbo.AddUtenti
    @pPassaporto VARCHAR(9),
	@pNome VARCHAR(20), 
    @pCognome VARCHAR(20),
	@pData DATE,
	@pMail VARCHAR(50), 
    @pPassword VARCHAR(50),
	@pTipo INT,
	@pCredito FLOAT,
   
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

         INSERT INTO dbo.[Utenti](Passaporto, Nome, Cognome , Datanascita , Mail , PassBash , Salt, Tipo,Credito)
        VALUES(@pPassaporto,@pNome,@pCognome,@pData,@pMail, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt, @pTipo,@pCredito)

        SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
GO

CREATE PROCEDURE dbo.Accesso
	@pMail NVARCHAR(50),
	@pPassword NVARCHAR(50),
	@userID NVARCHAR(9) OUTPUT,
	@responseMessage INT OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    /*DECLARE @userID NVARCHAR(9)*/

    IF EXISTS (SELECT TOP 1 Passaporto FROM [dbo].[Utenti] WHERE Mail=@pMail)
    BEGIN
        SET @userID=(SELECT Passaporto FROM [dbo].[Utenti] WHERE Mail= @pMail AND PassBash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @responseMessage=3/*'Incorrect password'*/
       ELSE 
           SET @responseMessage=1/*'User successfully logged in'*/
    END
    ELSE
       SET @responseMessage=2 /*'Invalid login'*/

END
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

CREATE PROCEDURE dbo.AddVoli
	@pTratta varchar(9),
	@pData date,
	@pPrezzo float ,
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
        INSERT INTO dbo.[Voli] (Id_volo,Tratta,Data,Prezzo)
        VALUES(@pId_volo,@pTratta,@pData,@pPrezzo)
       SET @responseMessage='Success'
    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO

CREATE PROCEDURE dbo.AddBiglietti  /*fa gia update credito */
	@pPasseggero varchar(9),
	@pVolo varchar(17),
	@pBuyer varchar(9),
	@pCLasse int = 0,   /*0 economy 1 business 2 first*/
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	DECLARE @pb float
	SELECT @pb =  v.Prezzo FROM Voli v WHERE v.Id_volo=@pVolo
	DECLARE @pf float
	SET @pf = @pb + ( @pb * 0.25 * @pCLasse)
	DECLARE @credito float
	SET @credito = (SELECT credito FROM Utenti WHERE Passaporto=@pBuyer)
    
    BEGIN TRY
		IF(@credito>=@pf)
			INSERT INTO dbo.Biglietti (PassaportoPasseggero,CodVolo,Buyer,Classe,Prezzo)
			VALUES(@pPasseggero,@pVolo,@pBuyer,@pCLasse,@pf)

			UPDATE Utenti SET Credito=(Credito-@pf) WHERE Passaporto=@pBuyer

			SET @responseMessage='Success';

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
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

GO
DECLARE @responseMessage NVARCHAR(250)
EXEC dbo.AddUtenti
	@pPassaporto ='MLDLALEX1',
	@pNome='Alex',
	@pCognome ='Mulder',
	@pData = '2000-10-22',
	@pMail = 'alex.mulder@studenti.unipr.it',
	@pPassword='SQLAlex.22',
	@pTipo=1,
	@pCredito = 1000,
	@responseMessage=@responseMessage OUTPUT
	SELECT @responseMessage
	GO

DECLARE @responseMessage NVARCHAR(250)
DECLARE @userID NVARCHAR(9)
EXEC dbo.Accesso
	@pMail ='alex.mulder@studenti.unipr.it',
	@pPassword = 'SQLAlex.22',
	@userID=@userID OUTPUT,
	@responseMessage=@responseMessage OUTPUT
	SELECT @responseMessage,@userID
GO
	/*SELECT * FROM Utenti WHERE Passaporto= 'MLDLALEX1'

	SELECT Codice,Stato,Citta,Nome,Coord.Long AS Long,Coord.Lat AS Lat FROM dbo.Aeroporti

	SELECT * FROM dbo.Biglietti

	SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Equipaggio ON Voli.Id_volo = Equipaggio.Volo*/