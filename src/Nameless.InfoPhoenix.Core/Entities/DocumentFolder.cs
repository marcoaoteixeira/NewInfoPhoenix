using System.Data;
using Nameless.Data;

namespace Nameless.InfoPhoenix.Entities {
    public sealed class DocumentFolder : EntityBase {
        #region Public Properties

        public string Label { get; set; } = string.Empty;

        public string FolderPath { get; set; } = string.Empty;

        public int Order { get; set; }

        #endregion

        #region Public Static Methods

        public static DocumentFolder MapFrom(IDataRecord record)
            => new() {
                ID = record.GetGuid(nameof(ID)),
                Label = record.GetString(nameof(Label)),
                FolderPath = record.GetString(nameof(FolderPath)),
                Order = record.GetInt32(nameof(Order)),
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
                new Parameter(nameof(Label), Label),
                new Parameter(nameof(FolderPath), FolderPath),
                new Parameter(nameof(Order), Order),
                new Parameter(nameof(CreatedAt), CreatedAt.ToSQLiteDateTime()),
                new Parameter(nameof(ModifiedAt), ModifiedAt.HasValue
                    ? ModifiedAt.GetValueOrDefault().ToSQLiteDateTime()
                    : null
                ),
            ];

        public DocumentFolder Clone()
            => new() {
                ID = ID,
                Label = Label,
                FolderPath = FolderPath,
                Order = Order,
                CreatedAt = CreatedAt,
                ModifiedAt = ModifiedAt
            };

        #endregion

        #region Public Override Methods

        public override bool Equals(object? obj)
            => obj is DocumentFolder value &&
                value.ID == ID &&
                value.FolderPath == FolderPath;

        public override int GetHashCode()
            => HashCode.Combine(ID, FolderPath);

        #endregion
    }
}
