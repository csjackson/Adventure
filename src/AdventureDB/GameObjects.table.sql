CREATE TABLE [dbo].[GameObjects]
(
	GameObjectId int NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	Name varchar(max) null,
	[Description] varchar(max) null,
	Location int not null,
	Type varchar(max) not null, 
)
