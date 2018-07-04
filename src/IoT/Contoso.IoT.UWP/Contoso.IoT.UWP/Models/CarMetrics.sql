/****** Object:  Table [dbo].[CarMetrics]    Script Date: 6/27/2018 9:54:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CarMetrics](
	[TripId] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[TripPointId] [nvarchar](50) NULL,
	[Latitude] [decimal](18, 10) NULL,
	[Longitude] [decimal](18, 10) NULL,
	[Speed] [decimal](18, 10) NULL,
	[RecordedTimeStamp] [datetime] NULL,
	[Sequence] [int] NULL,
	[EngineRPM] [decimal](18, 10) NULL,
	[ShortTermFuelBank] [decimal](18, 10) NULL,
	[LargeTermFuelBank] [decimal](18, 10) NULL,
	[ThrottlePosition] [decimal](18, 10) NULL,
	[RelativeTrottlePosition] [decimal](18, 10) NULL,
	[Runtime] [int] NULL,
	[DistanceWithMIL] [decimal](18, 10) NULL,
	[EngineLoad] [decimal](18, 10) NULL,
	[MAFFlowRate] [decimal](18, 10) NULL,
	[OutsideTemperarure] [decimal](18, 10) NULL,
	[EngineFuelRate] [decimal](18, 10) NULL,
	[vin] [nvarchar](50) NULL
) ON [PRIMARY]
GO


