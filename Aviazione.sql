USE [master]
GO
/****** Object:  Database [Aviazione]    Script Date: 11/05/2022 17:32:42 ******/
CREATE DATABASE [Aviazione]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Aviazione', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Aviazione.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Aviazione_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Aviazione_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Aviazione] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Aviazione].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Aviazione] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Aviazione] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Aviazione] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Aviazione] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Aviazione] SET ARITHABORT OFF 
GO
ALTER DATABASE [Aviazione] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Aviazione] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Aviazione] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Aviazione] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Aviazione] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Aviazione] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Aviazione] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Aviazione] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Aviazione] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Aviazione] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Aviazione] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Aviazione] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Aviazione] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Aviazione] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Aviazione] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Aviazione] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Aviazione] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Aviazione] SET RECOVERY FULL 
GO
ALTER DATABASE [Aviazione] SET  MULTI_USER 
GO
ALTER DATABASE [Aviazione] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Aviazione] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Aviazione] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Aviazione] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Aviazione] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Aviazione] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Aviazione', N'ON'
GO
ALTER DATABASE [Aviazione] SET QUERY_STORE = OFF
GO
USE [Aviazione]
GO
/****** Object:  Table [dbo].[Aeroporti]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aeroporti](
	[Codice] [varchar](3) NOT NULL,
	[Stato] [varchar](20) NOT NULL,
	[Citta] [varchar](20) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Coord] [geography] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Biglietti]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Biglietti](
	[PassaportoPasseggero] [varchar](9) NOT NULL,
	[CodVolo] [varchar](17) NOT NULL,
	[Buyer] [varchar](9) NOT NULL,
	[Classe] [int] NULL,
	[Prezzo] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PassaportoPasseggero] ASC,
	[CodVolo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipaggio]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipaggio](
	[Volo] [varchar](17) NOT NULL,
	[Passaporto] [varchar](9) NOT NULL,
	[Ruolo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Volo] ASC,
	[Passaporto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tratte]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tratte](
	[Id_Tratta] [varchar](6) NOT NULL,
	[Aeroporto1] [varchar](3) NOT NULL,
	[Aeroporto2] [varchar](3) NOT NULL,
	[Distance] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Tratta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utenti]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utenti](
	[Passaporto] [varchar](9) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
	[Datanascita] [date] NOT NULL,
	[Mail] [varchar](50) NULL,
	[PassBash] [binary](64) NULL,
	[Salt] [uniqueidentifier] NULL,
	[Tipo] [int] NOT NULL,
	[Credito] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Passaporto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voli]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voli](
	[Id_volo] [varchar](17) NOT NULL,
	[Tratta] [varchar](6) NOT NULL,
	[Data] [date] NOT NULL,
	[Prezzo] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_volo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Biglietti]  WITH CHECK ADD FOREIGN KEY([Buyer])
REFERENCES [dbo].[Utenti] ([Passaporto])
GO
ALTER TABLE [dbo].[Biglietti]  WITH CHECK ADD FOREIGN KEY([CodVolo])
REFERENCES [dbo].[Voli] ([Id_volo])
GO
ALTER TABLE [dbo].[Biglietti]  WITH CHECK ADD FOREIGN KEY([PassaportoPasseggero])
REFERENCES [dbo].[Utenti] ([Passaporto])
GO
ALTER TABLE [dbo].[Equipaggio]  WITH CHECK ADD FOREIGN KEY([Passaporto])
REFERENCES [dbo].[Utenti] ([Passaporto])
GO
ALTER TABLE [dbo].[Equipaggio]  WITH CHECK ADD FOREIGN KEY([Volo])
REFERENCES [dbo].[Voli] ([Id_volo])
GO
ALTER TABLE [dbo].[Tratte]  WITH CHECK ADD FOREIGN KEY([Aeroporto1])
REFERENCES [dbo].[Aeroporti] ([Codice])
GO
ALTER TABLE [dbo].[Tratte]  WITH CHECK ADD FOREIGN KEY([Aeroporto2])
REFERENCES [dbo].[Aeroporti] ([Codice])
GO
ALTER TABLE [dbo].[Voli]  WITH CHECK ADD FOREIGN KEY([Tratta])
REFERENCES [dbo].[Tratte] ([Id_Tratta])
GO
/****** Object:  StoredProcedure [dbo].[Accesso]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Accesso]
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
/****** Object:  StoredProcedure [dbo].[AddAeroporti]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAeroporti]  /* ha 1 argomento in piu che la tabella*/
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
/****** Object:  StoredProcedure [dbo].[AddBiglietti]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBiglietti]  /*fa gia update credito */
	@pPasseggero varchar(9),
	@pVolo varchar(17),
	@pBuyer varchar(9),
	@pCLasse int = 0,   /*0 economy 1 business 2 first*/
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON
	
    
    BEGIN TRY
	DECLARE @pb float
	SET @pb =  (SELECT Prezzo FROM Voli  WHERE Id_volo=@pVolo)
	DECLARE @pf float
	SET @pf = @pb + ( @pb * 0.25 * @pCLasse)
	DECLARE @credito float
	SET @credito = (SELECT credito FROM Utenti WHERE Passaporto=@pBuyer)
		IF (@credito>=@pf)
		BEGIN
			INSERT INTO dbo.Biglietti (PassaportoPasseggero,CodVolo,Buyer,Classe,Prezzo)
			VALUES(@pPasseggero,@pVolo,@pBuyer,@pCLasse,@pf)

			UPDATE Utenti SET Credito=(Credito-@pf) WHERE Passaporto=@pBuyer
			SET @responseMessage='Success';
			END
		ELSE
			SET @responseMessage='Credito insufficente';

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[AddEquipaggio]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEquipaggio]
	@pVolo varchar(17),
	@pPassaporto varchar(9),
	@pRuolo varchar(20),
	@responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON   
	BEGIN TRY
        INSERT INTO dbo.[Equipaggio] (Volo, Passaporto, Ruolo)
        VALUES(@pVolo,@pPassaporto,@pRuolo)
		SET @responseMessage='Success'
	END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddTratte]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTratte]	
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
/****** Object:  StoredProcedure [dbo].[AddUtenti]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUtenti]
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
/****** Object:  StoredProcedure [dbo].[AddVoli]    Script Date: 11/05/2022 17:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddVoli]
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
USE [master]
GO
ALTER DATABASE [Aviazione] SET  READ_WRITE 
GO


DECLARE @responseMessage NVARCHAR(250) EXEC dbo.AddUtenti
@pPassaporto = 'admin',@pNome = 'admin',
@pCognome = 'admin',@pData = '1/1/2000',
@pMail = 'admin@admin.com',@pPassword = 'admin',
@pTipo = 2,@pCredito = 999999 ,
@responseMessage = @responseMessage OUTPUT SELECT @responseMessage; 
GO