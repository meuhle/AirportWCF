USE Aviazione
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
	@responseMessage INT OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userID NVARCHAR(9)

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
