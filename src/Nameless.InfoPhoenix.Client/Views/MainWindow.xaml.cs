using Nameless.InfoPhoenix.Client.Infrastructure;
using Nameless.InfoPhoenix.Client.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace Nameless.InfoPhoenix.Client.Views {
    public partial class MainWindow : IWindow {
        #region Public Properties

        public MainWindowViewModel ViewModel { get; }

        #endregion

        #region Public Constructors

        public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService) {
            Guard.Against.Null(navigationService, nameof(navigationService));

            ViewModel = Guard.Against.Null(viewModel, nameof(viewModel));

            DataContext = this;

            SystemThemeWatcher.Watch(this);

            InitializeComponent();

            navigationService.SetNavigationControl(RootNavigationView);
        }

        #endregion
    }
}
