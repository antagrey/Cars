DECLARE @DatabaseFileLocation as varchar(255)
SET @DatabaseFileLocation = 'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER2017\MSSQL\DATA'


PRINT 'Create Database'
CREATE DATABASE [Cars]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cars', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER2017\MSSQL\DATA\Cars.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cars_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLSERVER2017\MSSQL\DATA\Cars_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cars].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Cars] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Cars] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Cars] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Cars] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Cars] SET ARITHABORT OFF 
GO

ALTER DATABASE [Cars] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Cars] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Cars] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Cars] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Cars] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Cars] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Cars] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Cars] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Cars] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Cars] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Cars] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Cars] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Cars] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Cars] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Cars] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Cars] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Cars] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Cars] SET RECOVERY FULL 
GO

ALTER DATABASE [Cars] SET  MULTI_USER 
GO

ALTER DATABASE [Cars] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Cars] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Cars] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Cars] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [Cars] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Cars] SET QUERY_STORE = OFF
GO

ALTER DATABASE [Cars] SET  READ_WRITE 
GO

USE [Cars]
GO

PRINT 'Create Login & User'
BEGIN TRY
	IF NOT EXISTS (SELECT 1 FROM [master].[sys].[syslogins] WHERE [name] = 'CarsApplicationUser')
	BEGIN
		CREATE LOGIN [CarsApplicationUser] WITH PASSWORD = 'p@ssw0rd'
	END
END TRY
BEGIN CATCH
   PRINT 'Instance login creation was not successful'
END CATCH
GO

IF NOT EXISTS (SELECT 1 FROM [sys].[database_principals] WHERE [name] = 'CarsApplicationUser')
BEGIN
	CREATE USER [CarsApplicationUser] FOR LOGIN [CarsApplicationUser] WITH DEFAULT_SCHEMA=[dbo]
END
GO

PRINT 'Create Role'

CREATE ROLE [Cars_ApplicationRole]
GO

EXEC sp_addrolemember N'Cars_ApplicationRole', N'CarsApplicationUser'
GO


PRINT 'Create Car table'

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Car](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Make] [nvarchar](255) NOT NULL,
	[Model] [nvarchar](255) NOT NULL,
	[Colour] [nvarchar](255) NOT NULL,
	[Year] [int] NOT NULL,
	[CreateDate] [datetime2] NOT NULL
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

GRANT DELETE
    ON OBJECT::[dbo].[Car] TO [Cars_ApplicationRole]
    AS [dbo];
GO

GRANT INSERT
    ON OBJECT::[dbo].[Car] TO [Cars_ApplicationRole]
    AS [dbo];
GO

GRANT SELECT
    ON OBJECT::[dbo].[Car] TO [Cars_ApplicationRole]
    AS [dbo];
GO

GRANT UPDATE
    ON OBJECT::[dbo].[Car] TO [Cars_ApplicationRole]
    AS [dbo];
GO

GRANT REFERENCES
    ON OBJECT::[dbo].[Car] TO [Cars_ApplicationRole]
    AS [dbo];
GO

CREATE PROCEDURE [dbo].[usp_Read_Car_By_Id]
	@CarId INT
AS
BEGIN
SET NOCOUNT ON;

    SELECT
        Id,
        Make,
		Model,
		Colour,
		[Year]
    FROM
        dbo.Car
    WHERE
        Id = @CarId

END
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[usp_Read_Car_By_Id] TO [Cars_ApplicationRole]
    AS [dbo];
GO

GRANT REFERENCES
    ON OBJECT::[dbo].[usp_Read_Car_By_Id] TO [Cars_ApplicationRole]
    AS [dbo];
GO