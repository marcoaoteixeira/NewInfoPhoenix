using Microsoft.Extensions.DependencyInjection;

namespace Nameless.InfoPhoenix.Bootstrap {
    public interface IBootstrapBuilder {
        #region Methods

        IBootstrapBuilder AddStep<TStep>()
            where TStep : class, IStep;

        IServiceCollection Build();

        #endregion
    }
}
