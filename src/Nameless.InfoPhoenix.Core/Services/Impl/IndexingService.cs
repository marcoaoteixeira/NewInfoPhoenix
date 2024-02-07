using Microsoft.Extensions.Logging;
using Nameless.InfoPhoenix.Entities;
using Nameless.Lucene;

namespace Nameless.InfoPhoenix.Services.Impl {
    public sealed class IndexingService : IIndexingService {
        #region Private Read-Only Fields

        private readonly IIndexManager _indexManager;
        private readonly ILogger _logger;

        #endregion

        #region Public Constructors

        public IndexingService(IIndexManager indexManager, ILogger logger) {
            _indexManager = Guard.Against.Null(indexManager, nameof(indexManager));
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        #endregion

        #region IIndexingService Members

        public void Index(Document[] documents) {
            try {
                var index = _indexManager.GetOrCreate(Root.Indexing.INDEX_NAME);
                var now = DateTime.Now;
                var documentsToIndex = new List<IDocument>();
                foreach (var document in documents) {
                    var indexDocument = index.NewDocument(document.ID.ToString());

                    document.LastIndexedAt = now;

                    indexDocument
                        .Set(nameof(Document.DocumentFolderID), document.DocumentFolderID.ToString(), FieldOptions.Store)
                        .Set(nameof(Document.FilePath), document.FilePath, FieldOptions.Store)
                        .Set(nameof(Document.Content), document.Content, FieldOptions.Store)
                        .Set(nameof(Document.LastIndexedAt), document.LastIndexedAt.Value.ToString("yyyy-MM-dd HH:mm:ss"), FieldOptions.Store)
                        .Set(nameof(Document.Missing), document.Missing, FieldOptions.Store)
                        .Set(nameof(Document.CreatedAt), document.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"), FieldOptions.Store);

                    documentsToIndex.Add(indexDocument);
                }
                index.StoreDocuments([.. documentsToIndex]);
            }
            catch (Exception ex) { _logger.LogError(ex, "Error while indexing documents."); }
        }

        #endregion
    }
}
