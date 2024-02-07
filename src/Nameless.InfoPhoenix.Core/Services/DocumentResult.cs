using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Services {
    public sealed class DocumentResult : ResultBase<Document> {
        #region Public Static Methods

        public static DocumentResult Success(Document document)
            => new() { Entity = document };

        public static DocumentResult Failure(string error)
            => new() { Error = error };

        #endregion
    }
}
