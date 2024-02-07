using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Services {
    public interface IDocumentFolderService {
        #region Methods

        DocumentFolderResult Create(string label, string folderPath, int order);

        DocumentFolder? Get(Guid documentFolderID);

        DocumentFolder[] GetAll();

        void SetLabel(Guid documentFolderID, string label);

        void SetOrder(Guid documentFolderID, int order);

        Document[] GetDocuments(Guid documentFolderID, bool includeRawFile);

        Document[] GetDocumentsFromFileSystem(Guid documentFolderID, bool includeRawFile);

        #endregion
    }
}
