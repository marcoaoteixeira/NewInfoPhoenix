using System.Windows.Controls;
using Nameless.InfoPhoenix.Client.ViewModels;

namespace Nameless.InfoPhoenix.Client.Views.Pages {
    public partial class HomePage : Page {
        #region Public Properties

        public HomeViewModel ViewModel { get; }

        #endregion

        #region Public Constructors

        public HomePage(HomeViewModel viewModel) {
            ViewModel = Guard.Against.Null(viewModel, nameof(viewModel));

            DataContext = this;

            InitializeComponent();
        }

        #endregion
    }
}
