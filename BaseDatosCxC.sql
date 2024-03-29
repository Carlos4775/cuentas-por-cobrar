USE [master]
GO
/****** Object:  Database [BaseDatosCxC]    Script Date: 3/23/2020 12:09:24 PM ******/
CREATE DATABASE [BaseDatosCxC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BaseDatosCxC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BaseDatosCxC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BaseDatosCxC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BaseDatosCxC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BaseDatosCxC] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BaseDatosCxC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BaseDatosCxC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET ARITHABORT OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BaseDatosCxC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BaseDatosCxC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BaseDatosCxC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BaseDatosCxC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET RECOVERY FULL 
GO
ALTER DATABASE [BaseDatosCxC] SET  MULTI_USER 
GO
ALTER DATABASE [BaseDatosCxC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BaseDatosCxC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BaseDatosCxC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BaseDatosCxC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BaseDatosCxC] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BaseDatosCxC', N'ON'
GO
ALTER DATABASE [BaseDatosCxC] SET QUERY_STORE = OFF
GO
USE [BaseDatosCxC]
GO
/****** Object:  Table [dbo].[Balances]    Script Date: 3/23/2020 12:09:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Balances](
	[Id_balance] [int] IDENTITY(1,1) NOT NULL,
	[Id_cliente] [int] NOT NULL,
	[Fecha_corte] [date] NOT NULL,
	[Antiguedad_promedio_saldos] [int] NOT NULL,
	[Monto] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_Balances_1] PRIMARY KEY CLUSTERED 
(
	[Id_balance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 3/23/2020 12:09:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_cliente] [varchar](50) NOT NULL,
	[Cedula] [varchar](15) NOT NULL,
	[LimiteCredito] [numeric](10, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Documentos]    Script Date: 3/23/2020 12:09:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Documentos](
	[Id_tipoDocumento] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Cuenta_contable] [varchar](35) NOT NULL,
	[Estado] [varchar](12) NOT NULL,
 CONSTRAINT [PK_Tipo_Documentos] PRIMARY KEY CLUSTERED 
(
	[Id_tipoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 3/23/2020 12:09:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[Id_transaccion] [int] IDENTITY(1,1) NOT NULL,
	[Tipo_movimiento] [varchar](12) NOT NULL,
	[Id_tipoDocumento] [int] NOT NULL,
	[Numero_documento] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Id_cliente] [int] NOT NULL,
	[Monto] [numeric](10, 2) NOT NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[Id_transaccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Balances] ON 

INSERT [dbo].[Balances] ([Id_balance], [Id_cliente], [Fecha_corte], [Antiguedad_promedio_saldos], [Monto]) VALUES (3, 6, CAST(N'2020-04-09' AS Date), 3, CAST(5000.00 AS Numeric(10, 2)))
INSERT [dbo].[Balances] ([Id_balance], [Id_cliente], [Fecha_corte], [Antiguedad_promedio_saldos], [Monto]) VALUES (4, 8, CAST(N'2020-08-10' AS Date), 2, CAST(50000.00 AS Numeric(10, 2)))
INSERT [dbo].[Balances] ([Id_balance], [Id_cliente], [Fecha_corte], [Antiguedad_promedio_saldos], [Monto]) VALUES (5, 8, CAST(N'2020-03-19' AS Date), 2, CAST(2000.00 AS Numeric(10, 2)))
INSERT [dbo].[Balances] ([Id_balance], [Id_cliente], [Fecha_corte], [Antiguedad_promedio_saldos], [Monto]) VALUES (6, 9, CAST(N'2020-03-12' AS Date), 5, CAST(4500.00 AS Numeric(10, 2)))
SET IDENTITY_INSERT [dbo].[Balances] OFF
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([Id_cliente], [Nombre_cliente], [Cedula], [LimiteCredito], [Estado]) VALUES (5, N'Victor Santos', N'402-3382831-4', CAST(20000.00 AS Numeric(10, 2)), 1)
INSERT [dbo].[Clientes] ([Id_cliente], [Nombre_cliente], [Cedula], [LimiteCredito], [Estado]) VALUES (6, N'Carlos Torres', N'402-0037842-6', CAST(80000.00 AS Numeric(10, 2)), 1)
INSERT [dbo].[Clientes] ([Id_cliente], [Nombre_cliente], [Cedula], [LimiteCredito], [Estado]) VALUES (8, N'Ximena', N'402-1524910-9', CAST(4000.00 AS Numeric(10, 2)), 1)
INSERT [dbo].[Clientes] ([Id_cliente], [Nombre_cliente], [Cedula], [LimiteCredito], [Estado]) VALUES (9, N'Fernando', N'000-0000000-0', CAST(7000.00 AS Numeric(10, 2)), 1)
INSERT [dbo].[Clientes] ([Id_cliente], [Nombre_cliente], [Cedula], [LimiteCredito], [Estado]) VALUES (11, N'Alex', N'402-3382831-4', CAST(7500.00 AS Numeric(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
SET IDENTITY_INSERT [dbo].[Tipo_Documentos] ON 

INSERT [dbo].[Tipo_Documentos] ([Id_tipoDocumento], [Descripcion], [Cuenta_contable], [Estado]) VALUES (1, N'Factura ', N'1', N'Activo')
INSERT [dbo].[Tipo_Documentos] ([Id_tipoDocumento], [Descripcion], [Cuenta_contable], [Estado]) VALUES (2, N'Recibo', N'2', N'En tramite')
SET IDENTITY_INSERT [dbo].[Tipo_Documentos] OFF
SET IDENTITY_INSERT [dbo].[Transacciones] ON 

INSERT [dbo].[Transacciones] ([Id_transaccion], [Tipo_movimiento], [Id_tipoDocumento], [Numero_documento], [Fecha], [Id_cliente], [Monto]) VALUES (2, N'Credito', 2, 2, CAST(N'2020-03-18' AS Date), 6, CAST(5000.00 AS Numeric(10, 2)))
INSERT [dbo].[Transacciones] ([Id_transaccion], [Tipo_movimiento], [Id_tipoDocumento], [Numero_documento], [Fecha], [Id_cliente], [Monto]) VALUES (3, N'Debito', 1, 8, CAST(N'2020-03-23' AS Date), 5, CAST(4000.00 AS Numeric(10, 2)))
INSERT [dbo].[Transacciones] ([Id_transaccion], [Tipo_movimiento], [Id_tipoDocumento], [Numero_documento], [Fecha], [Id_cliente], [Monto]) VALUES (4, N'Debito', 2, 2, CAST(N'2020-03-19' AS Date), 9, CAST(9000.00 AS Numeric(10, 2)))
SET IDENTITY_INSERT [dbo].[Transacciones] OFF
ALTER TABLE [dbo].[Balances]  WITH CHECK ADD  CONSTRAINT [FK_Balances_Clientes1] FOREIGN KEY([Id_cliente])
REFERENCES [dbo].[Clientes] ([Id_cliente])
GO
ALTER TABLE [dbo].[Balances] CHECK CONSTRAINT [FK_Balances_Clientes1]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Clientes] FOREIGN KEY([Id_cliente])
REFERENCES [dbo].[Clientes] ([Id_cliente])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Clientes]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Tipo_Documentos] FOREIGN KEY([Id_tipoDocumento])
REFERENCES [dbo].[Tipo_Documentos] ([Id_tipoDocumento])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Tipo_Documentos]
GO
USE [master]
GO
ALTER DATABASE [BaseDatosCxC] SET  READ_WRITE 
GO
