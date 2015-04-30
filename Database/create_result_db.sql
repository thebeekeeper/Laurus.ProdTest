USE [master]
GO

/****** Object:  Database [TyTestDev]    Script Date: 03/14/2012 22:05:10 ******/
CREATE DATABASE [TyTestDev] ON  PRIMARY 
( NAME = N'TyTestDev', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\TyTestDev.mdf' , SIZE = 1280KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TyTestDev_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\TyTestDev_log.LDF' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [TyTestDev] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TyTestDev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TyTestDev] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TyTestDev] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TyTestDev] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TyTestDev] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TyTestDev] SET ARITHABORT OFF 
GO

ALTER DATABASE [TyTestDev] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [TyTestDev] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [TyTestDev] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TyTestDev] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TyTestDev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TyTestDev] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TyTestDev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TyTestDev] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TyTestDev] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TyTestDev] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TyTestDev] SET  ENABLE_BROKER 
GO

ALTER DATABASE [TyTestDev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TyTestDev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TyTestDev] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TyTestDev] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TyTestDev] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TyTestDev] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TyTestDev] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TyTestDev] SET  READ_WRITE 
GO

ALTER DATABASE [TyTestDev] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TyTestDev] SET  MULTI_USER 
GO

ALTER DATABASE [TyTestDev] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TyTestDev] SET DB_CHAINING OFF 
GO


