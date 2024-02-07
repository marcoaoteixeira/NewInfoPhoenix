using MediatR;
using Nameless.InfoPhoenix.Entities;
using Nameless.InfoPhoenix.Requests;
using Nameless.InfoPhoenix.Responses;
using Nameless.InfoPhoenix.Services;
using Nameless.Lucene;
using IIndexDocument = Nameless.Lucene.IDocument;

namespace Nameless.InfoPhoenix.Handlers {
    public sealed class IndexDocumentsRequestHandler : IRequestHandler<IndexDocumentsRequest, IndexDocumentsResponse> {
        #region Private Read-Only Fields

        private readonly IDocumentFolderService _documentFolderService;
        private readonly IIndexingService _indexingService;

        #endregion

        #region Public Constructors

        public IndexDocumentsRequestHandler(IDocumentFolderService documentFolderService, IIndexingService indexingService) {
            _documentFolderService = Guard.Against.Null(documentFolderService, nameof(documentFolderService));
            _indexingService = Guard.Against.Null(indexingService, nameof(indexingService));
        }

        #endregion

        #region IRequestHandler<IndexDocumentsRequest, IndexDocumentsResponse> Members

        public Task<IndexDocumentsResponse> Handle(IndexDocumentsRequest request, CancellationToken cancellationToken) {
            //IndexDocumentsResponse result;

            var databaseDocs = _documentFolderService
                .GetDocuments(request.DocumentFolderID, includeRawFile: false)
                .Where(document => !document.Missing);

            var fileSystemDocs = _documentFolderService
                .GetDocumentsFromFileSystem(request.DocumentFolderID, includeRawFile: false);

            var toAdd = fileSystemDocs.Except(databaseDocs);

            //return Task.FromResult(result);
            throw new NotImplementedException();
        }

        #endregion
    }
}
