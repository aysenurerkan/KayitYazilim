USE [master]
GO
/****** Object:  Database [GnyYazilimDB]    Script Date: 13.10.2023 10:08:15 ******/
CREATE DATABASE [GnyYazilimDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GnyYazilimDB', FILENAME = N'C:\GnyYazılım\GnyYazilim\GnyYazilim\bin\Debug\GnyYazilimDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GnyYazilimDB_log', FILENAME = N'C:\GnyYazılım\GnyYazilim\GnyYazilim\bin\Debug\GnyYazilimDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GnyYazilimDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GnyYazilimDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GnyYazilimDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GnyYazilimDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GnyYazilimDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GnyYazilimDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GnyYazilimDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GnyYazilimDB] SET  MULTI_USER 
GO
ALTER DATABASE [GnyYazilimDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GnyYazilimDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GnyYazilimDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GnyYazilimDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GnyYazilimDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GnyYazilimDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [GnyYazilimDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [GnyYazilimDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GnyYazilimDB]
GO
/****** Object:  Table [dbo].[Kaydedilen_Isler]    Script Date: 13.10.2023 10:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kaydedilen_Isler](
	[IsEmriNo] [varchar](20) NOT NULL,
	[ParcaNo] [varchar](20) NULL,
	[ParcaAdi] [varchar](20) NULL,
	[Kabin] [varchar](20) NULL,
	[TcNo] [char](11) NULL,
 CONSTRAINT [PK_Kaydedilen_Isler] PRIMARY KEY CLUSTERED 
(
	[IsEmriNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tamamlanan_Isler]    Script Date: 13.10.2023 10:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tamamlanan_Isler](
	[KullaniciAdi] [varchar](30) NULL,
	[IsEmriNo] [varchar](20) NULL,
	[ParcaNo] [varchar](20) NULL,
	[ParcaAdi] [varchar](20) NOT NULL,
	[Kabin] [varchar](20) NULL,
	[BaslangicTarih] [date] NULL,
	[BitisTarih] [date] NULL,
	[BaslangicSaat] [time](7) NULL,
	[BitisSaat] [time](7) NULL,
	[Sicaklik] [int] NULL,
	[Nem] [int] NULL,
	[TamamlananIsId] [int] IDENTITY(20,1) NOT NULL,
	[TcNo] [char](11) NULL,
 CONSTRAINT [PK_Tamamlanan_Isler] PRIMARY KEY CLUSTERED 
(
	[TamamlananIsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Yoneticiler]    Script Date: 13.10.2023 10:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Yoneticiler](
	[TcNo] [char](11) NOT NULL,
	[Ad] [varchar](30) NOT NULL,
	[Soyad] [varchar](30) NOT NULL,
	[Yetki] [varchar](15) NOT NULL,
	[KullaniciAdi] [varchar](30) NOT NULL,
	[Sifre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Yoneticiler_1] PRIMARY KEY CLUSTERED 
(
	[TcNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY000', N'A00', N'A', N'KURUTMA KABİNİ', N'22222222222')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY111', N'B11', N'B', N'ASTAR KABİNİ', N'11111111111')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY123', N'A55', N'A', N'BOYAMA KABİNİ', N'11111111111')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY222', N'C22', N'C', N'KURUTMA KABİNİ', N'22222222222')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY333', N'D33', N'D', N'BOYAMA KABİNİ', N'33333333333')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY444', N'E44', N'E', N'BOYAMA KABİNİ', N'44444444444')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY555', N'K55', N'K', N'BOYAMA KABİNİ', N'33333333333')
INSERT [dbo].[Kaydedilen_Isler] ([IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [TcNo]) VALUES (N'GNY777', N'H77', N'H', N'ASTAR KABİNİ', N'33333333333')
GO
SET IDENTITY_INSERT [dbo].[Tamamlanan_Isler] ON 

INSERT [dbo].[Tamamlanan_Isler] ([KullaniciAdi], [IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [BaslangicTarih], [BitisTarih], [BaslangicSaat], [BitisSaat], [Sicaklik], [Nem], [TamamlananIsId], [TcNo]) VALUES (N'ALİKAYA', N'GNY111', N'B11', N'B', N'ASTAR KABİNİ', CAST(N'2023-10-12' AS Date), CAST(N'2023-10-12' AS Date), CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), NULL, NULL, 334, N'11111111111')
INSERT [dbo].[Tamamlanan_Isler] ([KullaniciAdi], [IsEmriNo], [ParcaNo], [ParcaAdi], [Kabin], [BaslangicTarih], [BitisTarih], [BaslangicSaat], [BitisSaat], [Sicaklik], [Nem], [TamamlananIsId], [TcNo]) VALUES (N'GÜLAK', N'GNY333', N'D33', N'D', N'BOYAMA KABİNİ', CAST(N'2023-10-13' AS Date), CAST(N'2023-10-13' AS Date), CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), NULL, NULL, 335, N'33333333333')
SET IDENTITY_INSERT [dbo].[Tamamlanan_Isler] OFF
GO
INSERT [dbo].[Yoneticiler] ([TcNo], [Ad], [Soyad], [Yetki], [KullaniciAdi], [Sifre]) VALUES (N'11111111111', N'AHMET', N'MEHMET', N'Yönetici', N'AHMETMEHMET', N'12345')
INSERT [dbo].[Yoneticiler] ([TcNo], [Ad], [Soyad], [Yetki], [KullaniciAdi], [Sifre]) VALUES (N'22222222222', N'ALİ', N'KAYA', N'Kullanıcı', N'ALİKAYA', N'12345')
INSERT [dbo].[Yoneticiler] ([TcNo], [Ad], [Soyad], [Yetki], [KullaniciAdi], [Sifre]) VALUES (N'33333333333', N'GÜL', N'AK', N'Kullanıcı', N'GÜLAK', N'12345')
INSERT [dbo].[Yoneticiler] ([TcNo], [Ad], [Soyad], [Yetki], [KullaniciAdi], [Sifre]) VALUES (N'44444444444', N'AYŞENUR', N'ERKAN', N'Kullanıcı', N'AYŞENURERKAN', N'12345')
GO
ALTER TABLE [dbo].[Kaydedilen_Isler]  WITH CHECK ADD  CONSTRAINT [FK_Kaydedilen_Isler_Yoneticiler] FOREIGN KEY([TcNo])
REFERENCES [dbo].[Yoneticiler] ([TcNo])
GO
ALTER TABLE [dbo].[Kaydedilen_Isler] CHECK CONSTRAINT [FK_Kaydedilen_Isler_Yoneticiler]
GO
ALTER TABLE [dbo].[Tamamlanan_Isler]  WITH CHECK ADD  CONSTRAINT [FK_Tamamlanan_Isler_Kaydedilen_Isler] FOREIGN KEY([IsEmriNo])
REFERENCES [dbo].[Kaydedilen_Isler] ([IsEmriNo])
GO
ALTER TABLE [dbo].[Tamamlanan_Isler] CHECK CONSTRAINT [FK_Tamamlanan_Isler_Kaydedilen_Isler]
GO
ALTER TABLE [dbo].[Tamamlanan_Isler]  WITH CHECK ADD  CONSTRAINT [FK_Tamamlanan_Isler_Yoneticiler] FOREIGN KEY([TcNo])
REFERENCES [dbo].[Yoneticiler] ([TcNo])
GO
ALTER TABLE [dbo].[Tamamlanan_Isler] CHECK CONSTRAINT [FK_Tamamlanan_Isler_Yoneticiler]
GO
USE [master]
GO
ALTER DATABASE [GnyYazilimDB] SET  READ_WRITE 
GO
