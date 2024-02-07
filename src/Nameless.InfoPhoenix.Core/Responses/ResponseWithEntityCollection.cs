using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Responses {
    public abstract record ResponseWithEntityCollection<TEntity> : ResponseBase
        where TEntity : EntityBase {
        #region Public Properties

        public TEntity[] Entities { get; } = [];

        #endregion

        #region Protected Constructors

        protected ResponseWithEntityCollection(TEntity[] entities) {
            Entities = entities;
        }

        #endregion
    }
}
