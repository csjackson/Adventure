ALTER TABLE [dbo].[GameObjects]
	ADD CONSTRAINT [ForeignKeyConstraint1] 
	FOREIGN KEY (Location_Id)
	REFERENCES GameObjects (GameObjectId)	

