using System.Windows.Controls;
using Nameless.InfoPhoenix.Client.ViewModels;

namespace Nameless.InfoPhoenix.Client.Views.Pages {
    public partial class DocumentFolderDialog : Page {
        #region Public Properties

        public DocumentFolderViewModel ViewModel { get; }

        #endregion

        #region Public Constructors
        
        public DocumentFolderDialog(DocumentFolderViewModel viewModel) {
            ViewModel = Guard.Against.Null(viewModel, nameof(viewModel));

            DataContext = this;

            InitializeComponent();
        }

        #endregion
    }
}
