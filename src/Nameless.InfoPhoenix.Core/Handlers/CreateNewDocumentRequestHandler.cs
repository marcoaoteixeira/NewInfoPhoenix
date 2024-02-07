using MediatR;
using Nameless.InfoPhoenix.Entities;
using Nameless.InfoPhoenix.Requests;
using Nameless.InfoPhoenix.Responses;
using Nameless.InfoPhoenix.Services;

namespace Nameless.InfoPhoenix.Handlers {
    public sealed class CreateNewDocumentRequestHandler : IRequestHandler<CreateNewDocumentRequest, CreateNewDocumentResponse> {
        #region Private Read-Only Fields

        private readonly IDocumentService _documentService;

        #endregion

        #region Public Constructors

        public CreateNewDocumentRequestHandler(IDocumentService documentService) {
            _documentService = Guard.Against.Null(documentService, nameof(documentService));
        }

        #endregion

        #region IRequestHandler<CreateNewDocumentRequest, CreateNewDocumentResponse> Members

        public Task<CreateNewDocumentResponse> Handle(CreateNewDocumentRequest request, CancellationToken cancellationToken) {
            var document = new Document {
                DocumentFolderID = request.DocumentFolderID,
                FilePath = request.FilePath
            };

            var result = _documentService.Save(document);

            var response = new CreateNewDocumentResponse {
                Entity = result.Entity,
                Error = result.Error
            };

            return Task.FromResult(response);
        }

        #endregion
    }
}
