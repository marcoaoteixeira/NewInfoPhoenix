using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Nameless.InfoPhoenix.Client.Models;
using Nameless.InfoPhoenix.Extensions;
using Nameless.InfoPhoenix.Services;

namespace Nameless.InfoPhoenix.Client.ViewModels {
    public partial class DocumentFolderViewModel : ObservableObject {
        #region Private Read-Only Fields

        private readonly IDocumentFolderService _documentFolderService;
        private readonly ILogger _logger;

        #endregion

        #region Private Fields

        private bool _initialized;

        #endregion

        #region Private Properties (Observable)

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private ObservableCollection<DocumentFolderModel> _documentFolders = [];

        #endregion

        #region Public Constructors

        public DocumentFolderViewModel(IDocumentFolderService documentFolderService)
            : this(documentFolderService, NullLogger.Instance) { }

        public DocumentFolderViewModel(IDocumentFolderService documentFolderService, ILogger logger) {
            _documentFolderService = Guard.Against.Null(documentFolderService, nameof(documentFolderService));
            _logger = Guard.Against.Null(logger, nameof(logger));

            Initialize();
        }

        #endregion

        #region Commands

        [RelayCommand]
        private void EditDocumentFolder() {
            Console.Write("OK");
        }

        #endregion

        public void CreateNewDocumentFolder(string label, string folderPath, int order) {
            var result = _documentFolderService.Create(
                Guard.Against.NullOrWhiteSpace(label, nameof(label)),
                Guard.Against.NullOrWhiteSpace(folderPath, nameof(folderPath)),
                order
            );

            if (result.Succeeded) {
                DocumentFolders.Add(
                    DocumentFolderModel.Convert(result.Entity)
                );
            }
            else {
                _logger.LogError(
                    message: "Could not create document folder. Error: {error}",
                    args: result.Error
                );
            }
        }

        #region Private Methods

        private void Initialize() {
            if (_initialized) { return; }

            Title = "Diretórios";

            DocumentFolders = _documentFolderService
                .GetAll()
                .Select(DocumentFolderModel.Convert)
                .AsObservable();

            _initialized = true;
        }

        #endregion
    }
}
