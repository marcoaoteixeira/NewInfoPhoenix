using System.Collections.ObjectModel;
using Nameless.InfoPhoenix.Client.Views.Pages;
using Nameless.Infrastructure;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace Nameless.InfoPhoenix.Client.ViewModels {
    public partial class MainViewModel : ObservableObject {
        #region Private Read-Only Fields

        private readonly IApplicationContext _applicationContext;

        #endregion

        #region Observable Fields

        [ObservableProperty]
        private string _applicationTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<object> _sidebarNavigationItems = [];

        [ObservableProperty]
        private ObservableCollection<object> _footerNavigationItems = [];

        #endregion

        #region Public Constructors

        public MainViewModel(IApplicationContext applicationContext) {
            _applicationContext = applicationContext;

            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize() {
            ApplicationTitle = $"{_applicationContext.ApplicationName} {_applicationContext.SemVer}";

            SidebarNavigationItems = [
                new NavigationItem {
                    Content = "Home",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(DashboardPage)
                },
                new NavigationItem {
                    Content = "Diretórios",
                    Icon = SymbolRegular.Folder24,
                    PageType = typeof(DocumentFolderPage)
                }
            ];

            FooterNavigationItems = [
                new NavigationItem {
                    Content = "Configurações",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(ConfigurationPage)
                }
            ];
        }

        #endregion
    }
}
