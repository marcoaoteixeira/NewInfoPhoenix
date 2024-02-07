using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix {
    public static class GuardExtension {
        #region Public Static Methods

        public static void MissingID(this Guard self, Document document) {
            self.Null(document, nameof(document));

            if (document.ID == Guid.Empty) {
                throw new ArgumentException("Document missing ID", nameof(Document.ID));
            }
        }

        #endregion
    }
}
