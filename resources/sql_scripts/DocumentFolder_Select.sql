SELECT
    [ID],
    [Label],
    [FolderPath],
	[Order],
    [CreatedAt],
    [ModifiedAt]
FROM [DocumentFolders]
WHERE
    [ID] = @ID;