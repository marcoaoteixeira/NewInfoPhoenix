using Microsoft.Extensions.DependencyInjection;
using Nameless.InfoPhoenix.Bootstrap.Impl;

namespace Nameless.InfoPhoenix.Bootstrap {
    public static class BootstrapBuilderExtension {
        #region Public Static Methods

        public static IBootstrapBuilder AddBootstrap(this IServiceCollection self)
            => new BootstrapBuilder(self);

        #endregion
    }
}
