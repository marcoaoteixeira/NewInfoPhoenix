using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Nameless.InfoPhoenix.Bootstrap.Impl {
    public sealed class BootstrapBuilder : IBootstrapBuilder {
        #region Private Constants

        private const string KEY = $"{nameof(BootstrapBuilder)}::27a48967-05b4-4b21-b8e4-d23242bce213";

        #endregion

        #region Private Read-Only Fields

        private readonly IServiceCollection _serviceCollection;

        #endregion

        #region Public Constructors

        public BootstrapBuilder(IServiceCollection serviceCollection) {
            _serviceCollection = Guard.Against.Null(serviceCollection, nameof(serviceCollection));
        }

        #endregion

        #region IBootstrapBuilder Members

        public IBootstrapBuilder AddStep<TStep>() where TStep : class, IStep {
            _serviceCollection.AddKeyedTransient<IStep, TStep>(KEY);

            return this;
        }

        public IServiceCollection Build()
            => _serviceCollection.AddTransient<IBootstrap>(provider
                => new Bootstrap(provider, KEY)
            );

        #endregion
    }
}
