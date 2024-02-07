using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Repositories {
    public interface IDocumentRepository {
        #region Methods

        bool Delete(Guid documentID);

        bool Exists(Guid documentID);

        bool Insert(Document document);

        Document? Select(Guid documentID, bool includeRawFile);

        Document[] SelectAll(Guid documentFolderID, bool includeRawFile);

        bool Update(Document document);

        #endregion
    }
}
