using System.Collections.ObjectModel;
using Nameless.InfoPhoenix.Client.Views.Pages;
using Nameless.Infrastructure;
using Wpf.Ui.Controls;

namespace Nameless.InfoPhoenix.Client.ViewModels {
    public partial class MainWindowViewModel : ObservableObject {
        #region Private Read-Only Fields

        private readonly IApplicationContext _applicationContext;

        #endregion

        #region Private Fields

        private bool _initialized;

        #endregion

        #region Private Properties (Observable)

        [ObservableProperty]
        private string _applicationTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<object> _sidebarNavigationItems = [];

        [ObservableProperty]
        private ObservableCollection<object> _footerNavigationItems = [];

        #endregion

        #region Public Constructors

        public MainWindowViewModel(IApplicationContext applicationContext) {
            _applicationContext = applicationContext;

            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize() {
            if (_initialized) { return; }

            ApplicationTitle = $"{_applicationContext.ApplicationName} {_applicationContext.SemVer}";

            SidebarNavigationItems = [
                new NavigationViewItem {
                    Content = "Home",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                    TargetPageType = typeof(HomePage)
                },
                new NavigationViewItem {
                    Content = "Diretórios",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Folder24 },
                    TargetPageType = typeof(DocumentFolderPage)
                }
            ];

            FooterNavigationItems = [
                new NavigationViewItem {
                    Content = "Configurações",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(SettingsPage)
                }
            ];

            _initialized = true;
        }

        #endregion
    }
}
