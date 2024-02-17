using Nameless.Infrastructure;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Nameless.InfoPhoenix.Client.ViewModels {
    public partial class SettingsViewModel : ObservableObject, INavigationAware {
        #region Private Read-Only Fields

        private readonly IApplicationContext _applicationContext;

        #endregion

        #region Private Fields

        private bool _initialized;

        #endregion

        #region Private Properties (Observable)

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _appVersion = string.Empty;

        [ObservableProperty]
        private ApplicationTheme _currentAppTheme = ApplicationTheme.Unknown;

        #endregion

        #region Public Constructors

        public SettingsViewModel(IApplicationContext applicationContext) {
            _applicationContext = Guard.Against.Null(applicationContext, nameof(applicationContext));
        }

        #endregion

        #region Private Methods

        private void Initialize() {
            if (_initialized) { return; }

            Title = "Configurações";
            AppVersion = _applicationContext.SemVer;
            CurrentAppTheme = ApplicationThemeManager.GetAppTheme();

            _initialized = true;
        }

        #endregion

        #region Public Methods

        partial void OnCurrentAppThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue) {
            //CurrentAppTheme = theme switch {
            //    nameof(ApplicationTheme.HighContrast) => ApplicationTheme.HighContrast,
            //    nameof(ApplicationTheme.Dark) => ApplicationTheme.Dark,
            //    nameof(ApplicationTheme.Light) => ApplicationTheme.Light,
            //    _ => ApplicationTheme.Unknown,
            //};

            ApplicationThemeManager.Apply(newValue);
        }

        #endregion

        #region INavigationAware Members

        public void OnNavigatedFrom() {

        }

        public void OnNavigatedTo()
            => Initialize();

        #endregion
    }
}
