namespace Nameless.InfoPhoenix {
    public static class DateTimeExtension {
        #region Public Static Methods

        public static DateTime ToSQLiteDateTime(this DateTime self)
            => self.AddMilliseconds(-self.Millisecond);

        #endregion
    }
}
