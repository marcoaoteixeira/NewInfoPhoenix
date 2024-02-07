using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nameless.InfoPhoenix.Client.Views;
using Nameless.InfoPhoenix.Infrastructure;
using Nameless.Lucene.DependencyInjection;

namespace Nameless.InfoPhoenix.Client {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        #region Private Static Read-Only Fields

        private static readonly Assembly[] SupportAssemblies = [
            typeof(Nameless.InfoPhoenix.Client.App).Assembly,
            typeof(Nameless.InfoPhoenix.Root).Assembly
        ];

        #endregion

        #region Private Read-Only Fields

        private readonly IHost _host;

        #endregion

        #region Public Constructors

        public App() {
            _host = HostFactory
                .Create()
                .SetConfigureServices(ConfigureServices)
                .SetConfigureContainer(ConfigureContainerBuilder)
                .Build();
        }

        #endregion

        #region Protected Override Methods

        protected override async void OnStartup(StartupEventArgs e) {
            await _host.StartAsync();

            _host
                .Services
                .GetRequiredService<MainWindow>()
                .Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e) {
            await _host.StopAsync();

            base.OnExit(e);
        }

        #endregion

        #region Private Static Methods

        private static string EnsureBasePath() {
            var basePath = typeof(App).Assembly.GetDirectoryPath("App_Data");
            if (!Directory.Exists(basePath)) {
                Directory.CreateDirectory(basePath);
            }
            return basePath;
        }

        private static void ConfigureServices(IServiceCollection services) {
            services
                .RegisterApplicationContext(
                    useAppDataSpecialFolder: true,
                    appVersion: typeof(App).Assembly.GetName().Version ?? new()
                )
                .RegisterFileProvider()
                .RegisterNLog()
                .RegisterMediatR(SupportAssemblies)
                .RegisterOffice()
                .RegisterServices()
                .RegisterRepositories();

            services.AddSingleton<MainWindow>();
        }

        private static void ConfigureContainerBuilder(HostBuilderContext context, ContainerBuilder builder) {
            builder
                .RegisterDatabase()
                .RegisterLuceneModule();
        }

        #endregion
    }
}