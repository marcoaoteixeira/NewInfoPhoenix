SELECT EXISTS(
	SELECT
		[ID]
	FROM [DocumentFolders]
	WHERE
		[ID] = @ID
);