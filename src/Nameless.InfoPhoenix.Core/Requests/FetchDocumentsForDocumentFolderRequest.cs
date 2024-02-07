using MediatR;
using Nameless.InfoPhoenix.Responses;

namespace Nameless.InfoPhoenix.Requests {
    public sealed record FetchDocumentsForDocumentFolderRequest : IRequest<FetchDocumentsForDocumentFolderResponse> {
        #region Public Properties

        public Guid DocumentFolderID { get; init; }

        #endregion
    }
}
