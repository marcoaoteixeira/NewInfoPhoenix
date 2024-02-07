UPDATE [DocumentFolders] SET
    [Label] = @Label,
    [FolderPath] = @FolderPath,
	[Order] = @Order,
    [CreatedAt] = @CreatedAt,
    [ModifiedAt] = @ModifiedAt
WHERE
	[ID] = @ID;