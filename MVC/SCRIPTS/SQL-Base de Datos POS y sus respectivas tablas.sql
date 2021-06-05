/****** Object:  Database [POS]    Script Date: 4/6/2021 17:03:39 ******/
DROP DATABASE [POS]
GO

/****** Object:  Database [POS]    Script Date: 4/6/2021 17:03:39 ******/
CREATE DATABASE [POS]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 1 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO

ALTER DATABASE [POS] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [POS] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [POS] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [POS] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [POS] SET ARITHABORT OFF 
GO

ALTER DATABASE [POS] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [POS] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [POS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [POS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [POS] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [POS] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [POS] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [POS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [POS] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO

ALTER DATABASE [POS] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [POS] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [POS] SET  MULTI_USER 
GO

ALTER DATABASE [POS] SET ENCRYPTION ON
GO

ALTER DATABASE [POS] SET QUERY_STORE = ON
GO

ALTER DATABASE [POS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO

-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO

ALTER DATABASE [POS] SET  READ_WRITE 
GO


/****** Object:  Table [dbo].[Articulo]    Script Date: 4/6/2021 22:11:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Articulo]') AND type in (N'U'))
DROP TABLE [dbo].[Articulo]
GO

/****** Object:  Table [dbo].[Articulo]    Script Date: 4/6/2021 22:11:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*===================================================================
 Author:			Erick Marcia
 Create date:		05 de junio de 2021
 Description:		Tabla de Articulo, aqui se guardan todos los artículos
 Notas:			   
 Modificacion: 
 Descripcion: 
 ===================================================================*/
CREATE TABLE [dbo].[Articulo](
	[idArticulo] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [nvarchar](25) NULL,
	[nombre] [nvarchar](255) NULL,
	[precio] [decimal](18, 0) NULL,
	[iva] [decimal](18, 0) NULL,
	[aplicaIva] [bit] NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_Articulo] PRIMARY KEY CLUSTERED 
(
	[idArticulo] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[Factura]    Script Date: 4/6/2021 23:01:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
DROP TABLE [dbo].[Factura]
GO

/****** Object:  Table [dbo].[Factura]    Script Date: 4/6/2021 23:01:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*===================================================================
 Author:			Erick Marcia
 Create date:		05 de junio de 2021
 Description:		Tabla de Factura, aqui se guardan todas el encabezado de todas las Facturas
 Notas:			   
 Modificacion: 
 Descripcion: 
 ===================================================================*/
CREATE TABLE [dbo].[Factura](
	[idFactura] [int] IDENTITY(1,1) NOT NULL,
	[codigoFactura] [nvarchar](25) NULL,
	[fecha] [datetime] NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Detalle]    Script Date: 4/6/2021 23:01:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detalle]') AND type in (N'U'))
DROP TABLE [dbo].[Detalle]
GO

/****** Object:  Table [dbo].[Detalle]    Script Date: 4/6/2021 23:01:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*===================================================================
 Author:			Erick Marcia
 Create date:		05 de junio de 2021
 Description:		Tabla de Detalle se guardan todo el detalle de todas las Facturas
 Notas:			   
 Modificacion: 
 Descripcion: 
 ===================================================================*/
CREATE TABLE [dbo].[Detalle](
	[idDetalle] [int] IDENTITY(1,1) NOT NULL,
	[codArticulo] [nvarchar](25) NULL,
	[nombre] [nvarchar](255) NULL,
	[precio] [decimal](18, 0) NULL,
	[cantidad] [decimal](18, 0) NULL,
	[IVA] [decimal](18, 0) NULL,
	[total] [decimal](18, 0) NULL,
	[idFactura] [int] NULL,
	[idArticulo] [int] NULL,
 CONSTRAINT [PK_Detalle] PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

