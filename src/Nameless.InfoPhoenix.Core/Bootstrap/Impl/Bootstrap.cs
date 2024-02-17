using Microsoft.Extensions.DependencyInjection;

namespace Nameless.InfoPhoenix.Bootstrap.Impl {
    public sealed class Bootstrap : IBootstrap {
        #region Private Read-Only Fields

        private readonly IServiceProvider _serviceProvider;
        private readonly string _key;

        #endregion

        #region Public Constructors

        public Bootstrap(IServiceProvider serviceProvider, string key) {
            _serviceProvider = Guard.Against.Null(serviceProvider, nameof(serviceProvider));
            _key = Guard.Against.NullOrWhiteSpace(key, nameof(key));
        }

        #endregion

        #region IBootstrap Members

        public void Run() {
            var steps = _serviceProvider
                .GetRequiredKeyedService<IEnumerable<IStep>>(_key)
                .OrderBy(step => step.Order);

            foreach (var step in steps) {
                step.Execute();
            }
        }

        #endregion
    }
}
