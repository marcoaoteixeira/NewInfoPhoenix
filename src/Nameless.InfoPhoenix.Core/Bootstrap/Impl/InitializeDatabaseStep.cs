using System.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nameless.Data;

namespace Nameless.InfoPhoenix.Bootstrap.Impl {
    public sealed class InitializeDatabaseStep : IStep {
        #region Private Read-Only Fields

        private readonly IDatabase _database;
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        #endregion

        #region Public Constructors

        public InitializeDatabaseStep(IDatabase database, IFileProvider fileProvider)
            : this(database, fileProvider, NullLogger.Instance) { }

        public InitializeDatabaseStep(IDatabase database, IFileProvider fileProvider, ILogger logger) {
            _database = Guard.Against.Null(database, nameof(database));
            _fileProvider = Guard.Against.Null(fileProvider, nameof(fileProvider));
            _logger = Guard.Against.Null(logger, nameof(logger)); ;
        }

        #endregion

        #region IStep Members

        public int Order => 0;

        public bool ThrowOnFailure => true;

        public void Execute() {
            var databaseSchemaFilePath = Path.Combine("sql_scripts", "Database_Schema.sql");
            var databaseSchemaFile = _fileProvider.GetFileInfo(databaseSchemaFilePath);

            try {
                using var stream = databaseSchemaFile.CreateReadStream();

                var databaseSchemaContent = stream.ToText();

                _database.ExecuteNonQuery(databaseSchemaContent, CommandType.Text);
            }
            catch (Exception ex) {
                _logger.LogError(
                    exception: ex,
                    message: $"Error while executing step #{Order}",
                    args: Order
                );

                if (ThrowOnFailure) {
                    throw;
                }
            }
        }

        #endregion
    }
}
