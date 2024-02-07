namespace Nameless.InfoPhoenix.Responses {
    public abstract record ResponseBase {
        #region Public Properties

        public string? Error { get; init; }

        public bool Succeeded => Error is null;

        #endregion
    }
}
