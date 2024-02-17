using System.Globalization;
using System.Windows.Data;
using Wpf.Ui.Appearance;

namespace Nameless.InfoPhoenix.Client.Helpers {
    public sealed class ThemeToIndexConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value is ApplicationTheme theme ? (int)theme : 0;

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value is int idx && Enum.IsDefined((ApplicationTheme)idx)
                ? (ApplicationTheme)idx
                : ApplicationTheme.Unknown;

        #endregion
    }
}
