namespace Nameless.InfoPhoenix.Client.ViewModels {
    public partial class HomeViewModel : ObservableObject {
        #region Private Fields

        private bool _initialized;

        #endregion

        #region Public Properties (Observable)

        [ObservableProperty]
        private string _title = string.Empty;

        #endregion

        #region Public Constructors

        public HomeViewModel()
        {
            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize() {
            if (_initialized) { return; }

            Title = "Home";

            _initialized = true;
        }

        #endregion
    }
}
