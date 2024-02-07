using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Services {
    public sealed class DocumentFolderResult : ResultBase<DocumentFolder> {
        #region Public Static Methods

        public static DocumentFolderResult Success(DocumentFolder documentFolder)
            => new() { Entity = documentFolder };

        public static DocumentFolderResult Failure(string error)
            => new() { Error = error };

        #endregion
    }
}
