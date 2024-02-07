using MediatR;
using Nameless.InfoPhoenix.Responses;

namespace Nameless.InfoPhoenix.Requests {
    public sealed record IndexDocumentsRequest : IRequest<IndexDocumentsResponse> {
        #region Public Properties

        public Guid DocumentFolderID { get; set; }

        #endregion
    }
}
