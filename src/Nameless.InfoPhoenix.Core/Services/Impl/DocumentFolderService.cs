using Nameless.InfoPhoenix.Entities;
using Nameless.InfoPhoenix.Repositories;

namespace Nameless.InfoPhoenix.Services.Impl {
    public sealed class DocumentFolderService : IDocumentFolderService {
        #region Private Read-Only Fields

        private readonly IDocumentFolderRepository _documentFolderRepository;
        private readonly IDocumentRepository _documentRepository;

        #endregion

        #region Public Constructors

        public DocumentFolderService(IDocumentFolderRepository documentFolderRepository, IDocumentRepository documentRepository) {
            _documentFolderRepository = Guard.Against.Null(documentFolderRepository, nameof(documentFolderRepository));
            _documentRepository = Guard.Against.Null(documentRepository, nameof(documentRepository));
        }

        #endregion

        #region Private Static Methods

        private static Document CreateDocumentFromFile(FileInfo file, Guid documentFolderID, bool includeRawFile)
            => new() {
                ID = Guid.Empty,
                DocumentFolderID = documentFolderID,
                FilePath = file.FullName,
                Content = string.Empty,
                RawFile = includeRawFile ? File.ReadAllBytes(file.FullName) : [],
                LastIndexedAt = DateTime.MinValue,
                CreatedAt = DateTime.MinValue,
                ModifiedAt = file.LastWriteTime.Date
            };

        #endregion

        #region IDocumentFolderRepository Members

        public DocumentFolderResult Create(string label, string folderPath, int order) {
            var documentFolder = new DocumentFolder {
                ID = Guid.NewGuid(),
                Label = Guard.Against.NullOrWhiteSpace(label, nameof(label)),
                FolderPath = Guard.Against.NullOrWhiteSpace(folderPath, nameof(folderPath)),
                Order = order
            };

            return _documentFolderRepository.Insert(documentFolder)
                ? DocumentFolderResult.Success(documentFolder)
                : DocumentFolderResult.Failure("Unable to create new document folder entity.");
        }

        public DocumentFolder? Get(Guid documentFolderID)
            => _documentFolderRepository.Select(documentFolderID);

        public DocumentFolder[] GetAll()
            => _documentFolderRepository.SelectAll();

        public void SetLabel(Guid documentFolderID, string label) {
            var documentFolder = _documentFolderRepository.Select(documentFolderID);

            if (documentFolder is not null) {
                documentFolder.Label = Guard.Against.NullOrWhiteSpace(label, nameof(label));
                _documentFolderRepository.Update(documentFolder);
            }
        }

        public void SetOrder(Guid documentFolderID, int order) {
            var documentFolder = _documentFolderRepository.Select(documentFolderID);

            if (documentFolder is not null) {
                documentFolder.Order = order;
                _documentFolderRepository.Update(documentFolder);
            }
        }

        public Document[] GetDocuments(Guid documentFolderID, bool includeRawFile = false)
            => _documentRepository.SelectAll(documentFolderID, includeRawFile);

        public Document[] GetDocumentsFromFileSystem(Guid documentFolderID, bool includeRawFile = false) {
            var result = new List<Document>();
            var documentFolder = _documentFolderRepository.Select(documentFolderID);

            if (documentFolder is not null && Directory.Exists(documentFolder.FolderPath)) {
                var directory = new DirectoryInfo(documentFolder.FolderPath);
                result = directory
                    .EnumerateFiles("*", SearchOption.AllDirectories)
                    .Where(file => Root.Defaults.ValidDocumentExtensions.Contains(file.Extension))
                    .Select(file => CreateDocumentFromFile(file, documentFolderID, includeRawFile))
                    .ToList();
            }

            return [.. result];
        }

        #endregion
    }
}
