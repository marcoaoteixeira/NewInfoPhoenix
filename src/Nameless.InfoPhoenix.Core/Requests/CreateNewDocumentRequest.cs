using MediatR;
using Nameless.InfoPhoenix.Responses;

namespace Nameless.InfoPhoenix.Requests {
    public sealed record CreateNewDocumentRequest : IRequest<CreateNewDocumentResponse> {
        #region Public Properties

        public Guid DocumentFolderID { get; set; }
        public string FilePath { get; init; } = string.Empty;

        #endregion
    }
}
