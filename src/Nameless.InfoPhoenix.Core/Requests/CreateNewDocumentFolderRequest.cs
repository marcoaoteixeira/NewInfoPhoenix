using MediatR;
using Nameless.InfoPhoenix.Responses;

namespace Nameless.InfoPhoenix.Requests {
    public sealed record CreateNewDocumentFolderRequest : IRequest<CreateNewDocumentFolderResponse> {
        #region Public Properties

        public string Label { get; init; } = string.Empty;
        public string FolderPath { get; init; } = string.Empty;
        public int Order { get; init; }

        #endregion
    }
}
