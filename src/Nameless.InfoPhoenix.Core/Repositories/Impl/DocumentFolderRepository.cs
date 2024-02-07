using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nameless.Data;
using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Repositories.Impl {
    public sealed class DocumentFolderRepository : RepositoryBase, IDocumentFolderRepository {
        #region Public Constructors

        public DocumentFolderRepository(IDatabase database, IFileProvider fileProvider)
            : this(database, fileProvider, NullLogger.Instance) { }

        public DocumentFolderRepository(IDatabase database, IFileProvider fileProvider, ILogger logger)
            : base(database, fileProvider, logger) { }

        #endregion

        #region IDocumentFolderRepository Members

        public bool Delete(Guid documentID) {
            var sql = GetSqlScript<DocumentFolder>(nameof(Delete));

            var parameters = new[] {
                new Parameter(
                    name: "@ID",
                    value: documentID
                )
            };

            return ExecuteNonQueryOverTransaction(sql, parameters) == SUCCESS;
        }

        public bool Exists(Guid documentID) {
            var sql = GetSqlScript<DocumentFolder>(nameof(Exists));

            var parameters = new[] {
                new Parameter(
                    name: "@ID",
                    value: documentID
                )
            };

            return Database.ExecuteScalar<long>(sql, parameters: parameters) == SUCCESS;
        }

        public bool Insert(DocumentFolder documentFolder) {
            var sql = GetSqlScript<DocumentFolder>(nameof(Insert));

            documentFolder.ID = Guid.NewGuid();
            documentFolder.CreatedAt = DateTime.Now;
            documentFolder.ModifiedAt = null;

            var parameters = documentFolder.ToParameters();

            return ExecuteNonQueryOverTransaction(sql, parameters) == SUCCESS;
        }

        public DocumentFolder? Select(Guid documentFolderID) {
            var sql = GetSqlScript<DocumentFolder>(nameof(Select));

            var parameters = new[] {
                new Parameter(
                    name: "@ID",
                    value: documentFolderID
                )
            };

            return Database.ExecuteReader(
                text: sql,
                mapper: DocumentFolder.MapFrom,
                parameters: parameters
            ).SingleOrDefault();
        }

        public DocumentFolder[] SelectAll() {
            var sql = GetSqlScript<DocumentFolder>(nameof(SelectAll));

            return Database.ExecuteReader(
                text: sql,
                mapper: DocumentFolder.MapFrom
            ).ToArray();
        }

        public bool Update(DocumentFolder documentFolder) {
            var sql = GetSqlScript<DocumentFolder>(nameof(Update));

            documentFolder.ModifiedAt = DateTime.Now;

            var parameters = documentFolder.ToParameters();

            return ExecuteNonQueryOverTransaction(sql, parameters) == SUCCESS;
        }

        #endregion
    }
}
