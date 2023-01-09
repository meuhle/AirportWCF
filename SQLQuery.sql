USE Aviazione

DELETE  FROM Biglietti

DECLARE @responseMessage NVARCHAR(250) DECLARE @userID
NVARCHAR(9) EXEC dbo.Accesso @pMail = 'a',
@pPassword = 'a',@userID=@userID OUTPUT,
@responseMessage = @responseMessage OUTPUT SELECT @responseMessage,@userID;


DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddAeroporti
@pCodice = 'LIN',@pStato = 'Italy',
@pCitta = 'Milano',@pNome = 'Linate',
@plat = '45.455197',@plong = '9.275467',
@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;

DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddAeroporti
@pCodice = 'MXP',@pStato = 'Italy',
@pCitta = 'Milano',@pNome = 'Malpensa',
@plat = '45.629026',@plong = '8.725834',
@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;

DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddAeroporti
@pCodice = 'MEL',@pStato = 'Australia',
@pCitta = 'Melbourne',@pNome = 'Tullmarine',
@plat = '-37.672925',@plong = '144.843306',
@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;

DELETE FROM Aeroporti WHERE Codice = 'MEL'
SELECT * FROM  Aeroporti
SELECT * FROM Equipaggio
SELECT * FROM  Tratte
SELECT * FROM Voli
SELECT * FROM Utenti
SELECT * FROM Utenti
SELECT * FROM Utenti WHERE Tipo = 1
SELECT * FROM Voli
SELECT * FROM Biglietti
SELECT * FROM Utenti WHERE Passaporto='z'
INSERT INTO Utenti(Passaporto,Nome,Cognome,Datanascita,Tipo) VALUES ('uhj','uhj','uhg','2/1/2023',0)
DELETE FROM Aeroporti WHERE Codice='MEL' AND Codice NOT IN(SELECT Aeroporto1 FROM Tratte ) AND Codice NOT IN (SELECT Aeroporto2 FROM Tratte )
SELECT Aeroporto1 FROM Tratte 



DELETE FROM Utenti WHERE Passaporto = ''
DELETE FROM Aeroporti WHERE Codice='" + code + "' AND Codice NOT IN(SELECT Aeroporto1 FROM Tratte ) AND Codice NOT IN (SELECT Aeroporto2 FROM Tratte )
DELETE FROM Tratte WHERE Id_Tratta='MELMXP' AND Id_Tratta NOT IN(SELECT Id_Tratta FROM Voli)
DELETE FROM Utenti WHERE Passaporto = 'z' AND Passaporto NOT IN(SELECT PassaportoPasseggero FROM Biglietti) AND Passaporto NOT IN (SELECT Buyer FROM  Biglietti)
DELETE FROM Voli WHERE Id_volo ='MXPLIN20231112' AND Id_volo NOT IN(SELECT CodVolo FROM Biglietti)
DELETE FROM Equipaggio WHERE Passaporto = 'cccccc' AND Volo = 'MXPLIN20231112'

DELETE FROM Tratte WHERE Id_Tratta='MELLIN' AND Id_Tratta NOT IN(SELECT Tratta FROM Voli)

SELECT * FROM Biglietti WHERE Buyer = 'a' OR PassaportoPasseggero = 'a'

SELECT * FROM Voli INNER JOIN Biglietti ON Voli.Id_volo = Biglietti.CodVolo 
WHERE Voli.Data >= '9/1/2023' AND Biglietti.Buyer ='z' OR Voli.Data >='9/1/2023'
AND Biglietti.PassaportoPasseggero= 'z' ORDER BY Voli.Data ASC

DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddBiglietti
@pPasseggero = 'sasdsd',@pVolo = 'MXPLIN202312',
@pBuyer = 'z',@pCLasse = 0,
@responseMessage = @responseMessage OUTPUT;SELECT @responseMessage 

DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddEquipaggio
@pVolo = 'MXPLIN20231112',@pPassaporto = 'a',
@pRuolo = 'hostess',@responseMessage = @responseMessage OUTPUT SELECT @responseMessage;

SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Equipaggio ON Voli.Id_volo = Equipaggio.Volo 
WHERE Voli.Id_volo IN (SELECT Volo FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= 'a');

SELECT Prezzo FROM Voli  WHERE Id_volo= 'MXPLIN20231112'

SELECT * FROM dbo.Biglietti WHERE Biglietti.PassaportoPasseggero= 'a' OR Biglietti.Buyer=  'a'; 

SELECT * FROM dbo.Voli WHERE Id_volo IN (SELECT CodVolo FROM dbo.Biglietti WHERE Biglietti.PassaportoPasseggero= 'a'
OR Biglietti.Buyer=  'a');

SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Biglietti ON Voli.Id_volo = Biglietti.CodVolo WHERE
Biglietti.CodVolo='MXPLIN20231112' AND Biglietti.PassaportoPasseggero='a'


SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta
WHERE Voli.Data='2/1/2023' AND Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = 
'LIN' AND Aeroporto2 = 'MXP' OR  Aeroporto1 = 'MXP' 
AND Aeroporto2 = 'LIN');


SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE Voli.Data='2/1/2023'
AND Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = 'LIN' OR Aeroporto2 = 'LIN')


SELECT Voli.Id_volo,Equipaggio.Passaporto,Equipaggio.Ruolo,Voli.Data  FROM dbo.Voli INNER JOIN dbo.Equipaggio ON Voli.Id_volo = Equipaggio.Volo WHERE Voli.Id_volo IN
(SELECT Volo FROM dbo.Equipaggio WHERE Equipaggio.Passaporto= 'a')


SELECT * FROM dbo.Biglietti INNER JOIN dbo.Voli ON Biglietti.CodVolo=Voli.Id_volo  WHERE Voli.Data >= '5/2/2024' AND Biglietti.PassaportoPasseggero= 'a' OR Biglietti.Buyer=  'a'

SELECT * FROM Voli INNER JOIN Biglietti ON Voli.Id_volo = Biglietti.CodVolo  WHERE Voli.Data >='5/2/2023' 
AND Biglietti.Buyer = 'a' OR Voli.Data >='5/2/2023' AND Biglietti.PassaportoPasseggero= 'a' ORDER BY Voli.Data ASC


SELECT Voli.* FROM dbo.Voli INNER JOIN dbo.Tratte ON Voli.Tratta = Tratte.Id_Tratta WHERE  Voli.Tratta IN ( SELECT Id_Tratta FROM dbo.Tratte WHERE  Aeroporto1 = 'LIN' AND Aeroporto2 = 'MXP' OR  Aeroporto1 = 'MXP' AND Aeroporto2 = 'LIN') ORDER BY Voli.Prezzo ASC


SELECT * FROM Voli INNER JOIN Biglietti ON Voli.Id_volo = Biglietti.CodVolo  WHERE Voli.Data "+comparator+"'5/2/2023'
AND Biglietti.Buyer ='a' OR Voli.Data >='5/2/2023' AND Biglietti.PassaportoPasseggero= 'a' ORDER BY Voli.Data ASC

SELECT * FROM Tratte
SELECT * FROM dbo.Aeroporti
SELECT Codice,Stato,Citta,Nome,Coord.Long AS Long,Coord.Lat AS Lat FROM dbo.Aeroporti