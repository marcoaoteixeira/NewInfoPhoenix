using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Responses {
    public sealed record FetchDocumentsForDocumentFolderResponse : ResponseWithEntityCollection<Document> {
        #region Public Constructors

        public FetchDocumentsForDocumentFolderResponse(Document[] documents)
            : base(documents) { }

        #endregion

        #region Public Static Methods

        public static FetchDocumentsForDocumentFolderResponse Success(Document[] documents)
            => new(documents);

        public static FetchDocumentsForDocumentFolderResponse Failure(string error)
            => new([]) { Error = error };

        #endregion
    }
}
