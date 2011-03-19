ALTER TABLE [dbo].[Items]
	ADD CONSTRAINT [ForeignKeyItemsToRooms] 
	FOREIGN KEY (RoomId)
	REFERENCES Rooms (RoomId)	

