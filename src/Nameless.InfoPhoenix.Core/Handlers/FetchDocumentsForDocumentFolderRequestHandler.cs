using MediatR;
using Nameless.InfoPhoenix.Requests;
using Nameless.InfoPhoenix.Responses;
using Nameless.InfoPhoenix.Services;

namespace Nameless.InfoPhoenix.Handlers {
    public sealed class FetchDocumentsForDocumentFolderRequestHandler : IRequestHandler<FetchDocumentsForDocumentFolderRequest, FetchDocumentsForDocumentFolderResponse> {
        #region Private Read-Only Fields

        private readonly IDocumentFolderService _documentFolderService;
        private readonly IDocumentService _documentService;

        #endregion

        #region Public Constructors

        public FetchDocumentsForDocumentFolderRequestHandler(IDocumentFolderService documentFolderService, IDocumentService documentService) {
            _documentFolderService = Guard.Against.Null(documentFolderService, nameof(documentFolderService));
            _documentService = Guard.Against.Null(documentService, nameof(documentService));
        }

        #endregion

        #region IRequestHandler<FetchDocumentsForDocumentFolderRequest, FetchDocumentsForDocumentFolderResponse> Members

        public Task<FetchDocumentsForDocumentFolderResponse> Handle(FetchDocumentsForDocumentFolderRequest request, CancellationToken cancellationToken) {
            var documentFolder = _documentFolderService.Get(request.DocumentFolderID);

            if (documentFolder is null) {
                return Task.FromResult(
                    FetchDocumentsForDocumentFolderResponse.Failure("Document folder not found.")
                );
            }

            if (!Directory.Exists(documentFolder.FolderPath)) {
                return Task.FromResult(
                    FetchDocumentsForDocumentFolderResponse.Failure("Directory for document folder does not exists.")
                );
            }

            var databaseDocs = _documentFolderService
                .GetDocuments(request.DocumentFolderID, includeRawFile: false)
                .Where(document => !document.Missing)
                .ToArray();

            var fileSystemDocs = _documentFolderService
                .GetDocumentsFromFileSystem(request.DocumentFolderID, includeRawFile: false);

            var documents = fileSystemDocs
                .Except(databaseDocs)
                .ToArray();

            foreach (var document in documents) {
                _documentService.Save(document);
            }

            return Task.FromResult(
                FetchDocumentsForDocumentFolderResponse.Success(documents)
            );
        }

        #endregion
    }
}
