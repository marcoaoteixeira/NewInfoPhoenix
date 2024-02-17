using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nameless.Data;
using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Repositories.Impl {
    public sealed class DocumentRepository : RepositoryBase, IDocumentRepository {
        #region Public Constructors

        public DocumentRepository(IDatabase database, IFileProvider fileProvider)
            : this(database, fileProvider, NullLogger.Instance) { }

        public DocumentRepository(IDatabase database, IFileProvider fileProvider, ILogger logger)
            : base(database, fileProvider, logger) { }

        #endregion

        #region IDocumentRepository Members

        public bool Delete(Guid documentID) {
            var sql = GetSqlScript<Document>(nameof(Delete));

            var parameters = new[] {
                new Parameter(
                    name: "@ID",
                    value: documentID
                )
            };

            return ExecuteNonQueryOverTransaction(sql, parameters) == SUCCESS;
        }

        public bool Exists(Guid documentID) {
            var sql = GetSqlScript<Document>(nameof(Exists));

            var parameters = new[] {
                new Parameter(
                    name: "@ID",
                    value: documentID
                )
            };

            return Database.ExecuteScalar<long>(sql, parameters: parameters) == SUCCESS;
        }

        public bool Insert(Document document) {
            var sql = GetSqlScript<Document>(nameof(Insert));

            document.LastIndexedAt = null;
            document.CreatedAt = DateTime.Now;
            document.ModifiedAt = null;

            var parameters = document.ToParameters();

            return ExecuteNonQueryOverTransaction(sql, parameters) == SUCCESS;
        }

        public Document? Select(Guid documentID, bool includeRawFile = false) {
            var sql = includeRawFile
                ? GetSqlScript<Document>(nameof(Select))
                : GetSqlScript<Document>($"{nameof(Select)}_Without_RawFile");

            var parameters = new[] {
                new Parameter(
                    name: "@ID",
                    value: documentID
                )
            };

            return Database.ExecuteReader(
                text: sql,
                mapper: Document.MapFrom,
                parameters: parameters
            ).SingleOrDefault();
        }

        public Document[] SelectAll(Guid documentFolderID, bool includeRawFile = false) {
            var sql = includeRawFile
                ? GetSqlScript<Document>(nameof(SelectAll))
                : GetSqlScript<Document>($"{nameof(SelectAll)}_Without_RawFile");

            var parameters = new[] {
                new Parameter(
                    name: "@DocumentFolderID",
                    value: documentFolderID
                )
            };

            return Database.ExecuteReader(
                text: sql,
                mapper: Document.MapFrom,
                parameters: parameters
            ).ToArray();
        }

        public bool Update(Document document) {
            var sql = GetSqlScript<Document>(nameof(Update));

            document.ModifiedAt = DateTime.Now;

            var parameters = document.ToParameters();

            return ExecuteNonQueryOverTransaction(sql, parameters) == SUCCESS;
        }

        #endregion
    }
}
