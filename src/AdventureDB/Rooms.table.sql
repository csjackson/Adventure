CREATE TABLE [dbo].[Rooms]
(
	RoomId int NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	RoomName varchar(max) null,
	[Description] varchar(max) null
)
