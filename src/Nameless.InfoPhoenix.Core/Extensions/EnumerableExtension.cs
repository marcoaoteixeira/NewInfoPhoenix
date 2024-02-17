using System.Collections.ObjectModel;

namespace Nameless.InfoPhoenix.Extensions {
    public static class EnumerableExtension {
        #region Public Static Methods

        public static ObservableCollection<T> AsObservable<T>(this IEnumerable<T> self)
            => new(self);

        #endregion
    }
}
