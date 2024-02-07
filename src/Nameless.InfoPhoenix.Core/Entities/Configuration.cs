using System.Data;
using Nameless.Data;

namespace Nameless.InfoPhoenix.Entities {
    public sealed class Configuration : EntityBase {
        #region Public Properties

        public string Key { get; set; } = string.Empty;
        public string? Value { get; set; }

        #endregion

        #region Public Static Methods

        public static Configuration MapFrom(IDataRecord record)
            => new() {
                ID = record.GetGuid(nameof(ID)),
                Key = record.GetString(nameof(Key)),
                Value = record.GetString(nameof(Value)),
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
                new Parameter(nameof(Key), Key),
                new Parameter(nameof(Value), Value),
                new Parameter(nameof(CreatedAt), CreatedAt.ToSQLiteDateTime()),
                new Parameter(nameof(ModifiedAt), ModifiedAt.HasValue
                    ? ModifiedAt.GetValueOrDefault().ToSQLiteDateTime()
                    : null
                ),
            ];

        public Configuration Clone()
            => new() {
                ID = ID,
                Key = Key,
                Value = Value,
                CreatedAt = CreatedAt,
                ModifiedAt = ModifiedAt
            };

        #endregion

        #region Public Override Methods

        public override bool Equals(object? obj)
            => obj is Configuration value &&
                value.Key == Key;

        public override int GetHashCode()
            => HashCode.Combine(Key);

        #endregion
    }
}
