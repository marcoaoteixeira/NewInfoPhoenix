using MediatR;
using Nameless.InfoPhoenix.Requests;
using Nameless.InfoPhoenix.Responses;
using Nameless.InfoPhoenix.Services;

namespace Nameless.InfoPhoenix.Handlers {
    public sealed class CreateNewDocumentFolderRequestHandler : IRequestHandler<CreateNewDocumentFolderRequest, CreateNewDocumentFolderResponse> {
        #region Private Read-Only Fields

        private readonly IDocumentFolderService _documentFolderService;

        #endregion

        #region Public Constructors

        public CreateNewDocumentFolderRequestHandler(IDocumentFolderService documentFolderService) {
            _documentFolderService = Guard.Against.Null(documentFolderService, nameof(documentFolderService));
        }

        #endregion

        #region IRequestHandler<CreateNewDocumentFolderRequest, CreateNewDocumentFolderResponse> Members

        public Task<CreateNewDocumentFolderResponse> Handle(CreateNewDocumentFolderRequest request, CancellationToken cancellationToken) {
            var result = _documentFolderService.Create(
                request.Label,
                request.FolderPath,
                request.Order
            );

            var response = new CreateNewDocumentFolderResponse {
                Entity = result.Entity,
                Error = result.Error,
            };

            return Task.FromResult(response);
        }

        #endregion
    }
}
