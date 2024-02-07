SELECT
    [ID],
    [DocumentFolderID],
    [FilePath],
    [Content],
    NULL AS [RawFile],
    [LastIndexedAt],
    [CreatedAt],
    [ModifiedAt]
FROM [Documents]
WHERE
    [DocumentFolderID] = @DocumentFolderID;