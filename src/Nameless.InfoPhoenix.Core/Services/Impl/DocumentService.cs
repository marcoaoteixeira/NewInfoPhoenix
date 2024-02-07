using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nameless.InfoPhoenix.Entities;
using Nameless.InfoPhoenix.Office;
using Nameless.InfoPhoenix.Repositories;

namespace Nameless.InfoPhoenix.Services.Impl {
    public sealed class DocumentService : IDocumentService {
        #region Private Read-Only Fields

        private readonly IDocumentRepository _documentRepository;
        private readonly IOfficeSuite _officeSuite;
        private readonly ILogger _logger;

        #endregion

        #region Public Constructors

        public DocumentService(IDocumentRepository documentRepository, IOfficeSuite officeSuite)
            : this(documentRepository, officeSuite, NullLogger.Instance) { }

        public DocumentService(IDocumentRepository documentRepository, IOfficeSuite officeSuite, ILogger logger) {
            _documentRepository = Guard.Against.Null(documentRepository, nameof(documentRepository));
            _officeSuite = Guard.Against.Null(officeSuite, nameof(officeSuite));
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        #endregion

        #region Private Methods

        private bool TryFetchContent(string filePath, [NotNullWhen(returnValue: true)] out string? content) {
            content = null;

            try {
                content = _officeSuite.GetWordDocumentContent(filePath, formatted: true);
                return true;
            }
            catch (Exception ex) { _logger.LogError(ex, "Error while reading WORD document. ({FilePath})", args: filePath); }

            return false;
        }

        private bool TryFetchRawFile(string filePath, [NotNullWhen(returnValue: true)] out byte[]? rawFile) {
            rawFile = null;

            try {
                rawFile = File.ReadAllBytes(filePath);
                return true;
            }
            catch (Exception ex) { _logger.LogError(ex, "Error while retrieving bytes. ({FilePath})", args: filePath); }

            return false;
        }

        #endregion

        #region IDocumentService Members

        public void SetLastIndexedAt(Document document, DateTime lastIndexedAt) {
            Guard.Against.MissingID(document);

            document.LastIndexedAt = lastIndexedAt.ToSQLiteDateTime();

            _documentRepository.Update(document);
        }

        public Document? Get(Guid documentID, bool includeRawFile)
            => _documentRepository.Select(documentID, includeRawFile);

        public Document[] GetAll(Guid documentFolderID, bool includeRawFile)
            => _documentRepository.SelectAll(documentFolderID, includeRawFile);

        public DocumentResult Save(Document document) {
            Guard.Against.Null(document, nameof(document));

            if (document.Missing is false) {
                document.Content = TryFetchContent(document.FilePath, out var content)
                    ? content
                    : string.Empty;

                document.RawFile = TryFetchRawFile(document.FilePath, out var rawFile)
                    ? rawFile
                    : [];
            }

            var success = document.ID == Guid.Empty
                ? _documentRepository.Insert(document)
                : _documentRepository.Update(document);

            return success
                ? DocumentResult.Success(document)
                : DocumentResult.Failure("Unable to persist document entry into database.");
        }

        #endregion
    }
}
