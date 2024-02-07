SELECT
    [ID],
    [DocumentFolderID],
    [FilePath],
    [Content],
    [RawFile],
    [LastIndexedAt],
    [CreatedAt],
    [ModifiedAt]
FROM [Documents]
WHERE
	[ID] = @ID;