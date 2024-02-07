using System.Collections.Concurrent;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nameless.Data;
using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Repositories {
    public abstract class RepositoryBase {
        #region Protected Constants

        protected const long SUCCESS = 1L;

        #endregion

        #region Protected Properties

        protected IDatabase Database { get; }
        protected ILogger Logger { get; }

        #endregion

        #region Private Properties

        private IFileProvider FileProvider { get; }
        private ConcurrentDictionary<string, string> Cache { get; } = [];

        #endregion

        #region Protected Constructors

        protected RepositoryBase(IDatabase database, IFileProvider fileProvider)
            : this(database, fileProvider, NullLogger.Instance) { }

        protected RepositoryBase(IDatabase database, IFileProvider fileProvider, ILogger logger) {
            Database = Guard.Against.Null(database, nameof(database));
            FileProvider = Guard.Against.Null(fileProvider, nameof(fileProvider));
            Logger = Guard.Against.Null(logger, nameof(logger));
        }

        #endregion

        #region Protected Methods

        protected string GetSqlScript<TEntity>(string actionName) where TEntity : EntityBase
            => Cache.GetOrAdd(
                key: $"{typeof(TEntity).Name}_{actionName}",
                valueFactory: InnerGetSqlScript
            );

        protected int ExecuteNonQueryOverTransaction(string sql, params Parameter[] parameters) {
            var result = 0;

            using var transaction = Database.BeginTransaction();
            try {
                result = Database.ExecuteNonQuery(sql, parameters: parameters);
                transaction.Commit();
            }
            catch (Exception ex) {
                transaction.Rollback();
                Logger.LogError(ex, "Error while executing non-query.");
            }

            return result;
        }

        #endregion

        #region Private Methods

        private string InnerGetSqlScript(string scriptName) {
            var result = string.Empty;
            var path = Path.Combine("sql_scripts", $"{scriptName}.sql");
            var file = FileProvider.GetFileInfo(path);

            if (!file.Exists) {
                Logger.LogWarning(
                    message: "SQL script file missing. File: {FilePath}",
                    args: path
                );

                return result;
            }

            using var stream = file.CreateReadStream();
            return stream.ToText();
        }

        #endregion
    }
}
