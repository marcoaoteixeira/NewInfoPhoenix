using System.Data;
using Nameless.Data;

namespace Nameless.InfoPhoenix.Entities {
    public sealed class Document : EntityBase {
        #region Public Properties

        public Guid DocumentFolderID { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public byte[] RawFile { get; set; } = [];

        public DateTime? LastIndexedAt { get; set; }

        public bool Missing => !File.Exists(FilePath);

        #endregion

        #region Public Static Methods

        public static Document MapFrom(IDataRecord record)
            => new() {
                ID = record.GetGuid(nameof(ID)),
                DocumentFolderID = record.GetGuid(nameof(DocumentFolderID)),
                FilePath = record.GetString(nameof(FilePath)),
                Content = record.GetString(nameof(Content)),
                RawFile = record.GetBlob(nameof(RawFile)),
                LastIndexedAt = record.TryGet<DateTime?>(nameof(LastIndexedAt), out var lastIndexedAt)
                    ? lastIndexedAt
                    : null,
                CreatedAt = record.GetDateTime(nameof(CreatedAt)),
                ModifiedAt = record.TryGet<DateTime?>(nameof(ModifiedAt), out var modifiedAt)
                    ? modifiedAt
                    : null,
            };

        #endregion

        #region Public Methods

        public Parameter[] ToParameters()
            => [
                new Parameter(nameof(ID), ID),
                new Parameter(nameof(DocumentFolderID), DocumentFolderID),
                new Parameter(nameof(FilePath), FilePath),
                new Parameter(nameof(Content), Content),
                new Parameter(nameof(RawFile), RawFile),
                new Parameter(nameof(LastIndexedAt), LastIndexedAt.HasValue
                    ? LastIndexedAt.GetValueOrDefault().ToSQLiteDateTime()
                    : null
                ),
                new Parameter(nameof(CreatedAt), CreatedAt.ToSQLiteDateTime()),
                new Parameter(nameof(ModifiedAt), ModifiedAt.HasValue
                    ? ModifiedAt.GetValueOrDefault().ToSQLiteDateTime()
                    : null
                ),
            ];

        public Document Clone() {
            var result = new Document {
                ID = ID,
                DocumentFolderID = DocumentFolderID,
                FilePath = FilePath,
                Content = Content,
                RawFile = new byte[RawFile.Length],
                LastIndexedAt = LastIndexedAt,
                CreatedAt = CreatedAt,
                ModifiedAt = ModifiedAt
            };

            Array.Copy(RawFile, result.RawFile, RawFile.Length);

            return result;
        }

        #endregion

        #region Public Override Methods

        public override bool Equals(object? obj)
            => obj is Document value &&
                value.DocumentFolderID == DocumentFolderID &&
                value.FilePath == FilePath &&
                value.ModifiedAt == ModifiedAt;

        public override int GetHashCode()
            => HashCode.Combine(DocumentFolderID, FilePath, ModifiedAt);

        #endregion
    }
}
