using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Appearance;

namespace Nameless.InfoPhoenix.Client {
    /// <summary>
    /// This class was defined to be an entrypoint for this project assembly.
    /// 
    /// *** DO NOT IMPLEMENT ANYTHING HERE ***
    /// 
    /// But, it's allow to use this class as a repository for all constants or
    /// default values that we'll use throughout this project.
    /// </summary>
    public static class Root {
        #region Public Static Inner Classes

        public static class Themes {
            #region Public Constants

            public const string DARK = nameof(ApplicationTheme.Dark);
            public const string LIGHT = nameof(ApplicationTheme.Light);
            public const string HIGH_CONTRAST = nameof(ApplicationTheme.HighContrast);
            public const string SYSTEM = "System";

            #endregion
        }

        #endregion
    }
}
