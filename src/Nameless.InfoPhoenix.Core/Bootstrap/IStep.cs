namespace Nameless.InfoPhoenix.Bootstrap {
    public interface IStep {
        #region Properties

        int Order { get; }
        bool ThrowOnFailure { get; }

        #endregion

        #region Methods

        void Execute();

        #endregion
    }
}
