ALTER TABLE [dbo].[GameObjects]
	ADD CONSTRAINT [ForeignKeyDestinationToObject] 
	FOREIGN KEY (Destination)
	REFERENCES GameObjects (GameObjectId)	

