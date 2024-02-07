UPDATE [Documents] SET
    [DocumentFolderID] = @DocumentFolderID,
    [FilePath] = @FilePath,
    [Content] = @Content,
    [RawFile] = @RawFile,
    [LastIndexedAt] = @LastIndexedAt,
    [CreatedAt] = @CreatedAt,
    [ModifiedAt] = @ModifiedAt
WHERE
	[ID] = @ID;