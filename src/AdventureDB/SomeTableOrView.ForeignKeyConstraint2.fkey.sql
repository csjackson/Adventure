ALTER TABLE [dbo].[ExitAliases]
	ADD CONSTRAINT [ForeignKeyExitToAlias] 
	FOREIGN KEY (ExitId)
	REFERENCES GameObjects (GameObjectId)	

