using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Services {
    public interface IIndexingService {
        #region Methods

        void Index(Document[] documents);

        #endregion
    }
}
