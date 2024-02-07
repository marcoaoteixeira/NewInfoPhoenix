using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Responses {
    public abstract record ResponseWithEntity<TEntity> : ResponseBase
        where TEntity : EntityBase {
        #region Public Properties

        public TEntity? Entity { get; init; }

        #endregion
    }
}
