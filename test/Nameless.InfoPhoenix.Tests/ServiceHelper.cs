using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nameless.InfoPhoenix.Infrastructure;
using Nameless.Lucene.DependencyInjection;

namespace Nameless.InfoPhoenix {
    public sealed class ServiceHelper {
        private static readonly Assembly[] SupportAssemblies = new[] {
            typeof(Root).Assembly
        };

        public static ServiceHelper Instance { get; } = new();

        static ServiceHelper() {
        }

        private ServiceHelper() {
            _host = HostFactory
                .Create("--applicationName=InfoPhoenix_IntegrationTests")
                .SetConfigureServices(services
                    => services
                        .RegisterApplicationContext(useAppDataSpecialFolder: false)
                        .RegisterFileProvider()
                        .RegisterNLog()
                        .RegisterMediatR(SupportAssemblies)
                        .RegisterOffice()
                        .RegisterServices()
                        .RegisterRepositories()
                )
                .SetConfigureContainer((_, builder)
                    => builder
                        .RegisterDatabase()
                        .RegisterLuceneModule()
                )
                .Build();
        }

        private readonly IHost _host;

        public Task StartAsync()
            => _host.StartAsync();

        public Task StopAsync()
            => _host.StopAsync();

        public T GetService<T>() where T : notnull
            => _host.Services.GetRequiredService<T>();
    }
}