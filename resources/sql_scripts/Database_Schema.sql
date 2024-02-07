CREATE TABLE IF NOT EXISTS [Configurations] (
    [ID]			TEXT	NOT NULL,
    [Key]			TEXT	NOT NULL,
	[Value]			TEXT	NOT NULL,
	[CreatedAt]		TEXT	NOT NULL,
	[ModifiedAt]	TEXT	NULL,
	
	PRIMARY KEY ([ID]),
	
	CONSTRAINT [UQ_Configurations_Key] UNIQUE ([Key])
);

CREATE TABLE IF NOT EXISTS [DocumentFolders] (
    [ID]			TEXT	NOT NULL,
    [Label]			TEXT	NOT NULL,
	[FolderPath]	TEXT	NOT NULL,
	[Order]			INTEGER	NOT NULL	DEFAULT 0,
	[CreatedAt]		TEXT	NOT NULL,
	[ModifiedAt]	TEXT	NULL,
	
	PRIMARY KEY ([ID]),
	
	CONSTRAINT [UQ_DocumentFolders_FolderPath] UNIQUE ([FolderPath])
);

CREATE TABLE IF NOT EXISTS [Documents] (
    [ID]				TEXT	NOT NULL,
	[DocumentFolderID]	TEXT	NOT NULL,
	[FilePath]			TEXT	NOT NULL,
	[Content]			TEXT	NOT NULL,
	[RawFile]			BLOB	NULL,
	[LastIndexedAt]		TEXT	NULL,
	[CreatedAt]			TEXT	NOT NULL,
	[ModifiedAt]		TEXT	NULL,
	
	PRIMARY KEY ([ID]),
	
	FOREIGN KEY([DocumentFolderID]) REFERENCES [DocumentFolders]([ID]) ON DELETE CASCADE,
	
	CONSTRAINT [UQ_Documents_FilePath] UNIQUE ([FilePath])
);