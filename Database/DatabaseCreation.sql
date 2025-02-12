USE [master]
GO
CREATE DATABASE [TechnicalExercise]
 ON PRIMARY 
( 
    NAME = N'TechnicalExercise', 
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\Data\TechnicalExercise.mdf', 
    SIZE = 8192KB, 
    MAXSIZE = UNLIMITED, 
    FILEGROWTH = 65536KB 
)
 LOG ON 
( 
    NAME = N'TechnicalExercise_log', 
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\Data\TechnicalExercise_log.ldf', 
    SIZE = 8192KB, 
    MAXSIZE = 2048GB, 
    FILEGROWTH = 65536KB 
);
GO

-- Set the compatibility level, remove it if not needed for a specific version
--ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 100; -- SQL Server 2008/SQL Server 2008 R2
--ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 110; -- SQL Server 2012
--ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 120; -- SQL Server 2014
--ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 130; -- SQL Server 2016
--ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 140; -- SQL Server 2017
ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 150; -- SQL Server 2019
--ALTER DATABASE [TechnicalExercise] SET COMPATIBILITY_LEVEL = 160; -- SQL Server 2022
GO

-- Optional settings, enable only if features like full-text search are needed
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
    EXEC [TechnicalExercise].[dbo].[sp_fulltext_database] @action = 'enable';
END
GO

ALTER DATABASE [TechnicalExercise] SET AUTO_CLOSE OFF;
ALTER DATABASE [TechnicalExercise] SET AUTO_SHRINK OFF;
ALTER DATABASE [TechnicalExercise] SET RECOVERY SIMPLE; -- Change to FULL if transaction logs are critical
ALTER DATABASE [TechnicalExercise] SET MULTI_USER;
GO

USE [TechnicalExercise]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/12/2025 10:19:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](128) NULL,
	[FirstName] [varchar](128) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
 CONSTRAINT [Pk_dbo_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [Uq_dbo_Users_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [TechnicalExercise] SET  READ_WRITE 
GO