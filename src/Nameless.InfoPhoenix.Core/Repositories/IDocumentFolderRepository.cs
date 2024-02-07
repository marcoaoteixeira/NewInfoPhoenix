using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Repositories {
    public interface IDocumentFolderRepository {
        #region Methods

        bool Delete(Guid documentFolderID);

        bool Exists(Guid documentFolderID);

        bool Insert(DocumentFolder documentFolder);

        DocumentFolder? Select(Guid documentFolderID);

        DocumentFolder[] SelectAll();

        bool Update(DocumentFolder documentFolder);

        #endregion
    }
}
