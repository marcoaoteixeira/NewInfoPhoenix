namespace Nameless.InfoPhoenix.Responses {
    public sealed record IndexDocumentsResponse : ResponseBase {
        #region Public Static Methods

        public static IndexDocumentsResponse Success()
            => new() { };

        public static IndexDocumentsResponse Failure(string error)
            => new() { Error = error };

        #endregion
    }
}
