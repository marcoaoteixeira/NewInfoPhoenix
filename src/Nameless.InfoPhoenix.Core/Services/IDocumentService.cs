using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Services {
    public interface IDocumentService {
        #region Methods

        void SetLastIndexedAt(Document document, DateTime lastIndexedAt);

        Document? Get(Guid documentID, bool includeRawFile);

        Document[] GetAll(Guid documentFolderID, bool includeRawFile);

        DocumentResult Save(Document document);

        #endregion
    }
}
