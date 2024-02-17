using System.Windows.Controls;
using Nameless.InfoPhoenix.Client.ViewModels;

namespace Nameless.InfoPhoenix.Client.Views.Pages {
    public partial class SettingsPage : Page {
        #region Public Properties

        public SettingsViewModel ViewModel { get; }

        #endregion

        #region Public Constructors

        public SettingsPage(SettingsViewModel viewModel) {
            ViewModel = Guard.Against.Null(viewModel, nameof(viewModel));

            DataContext = this;

            InitializeComponent();
        }

        #endregion
    }
}
