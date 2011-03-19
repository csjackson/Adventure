CREATE TABLE [dbo].[Items]
(
	ItemId int NOT NULL IDENTITY (1,1) PRIMARY KEY,
	ItemName varchar(max) null,
	ItemDescription varchar(max) null,
	RoomId int not null,
)
