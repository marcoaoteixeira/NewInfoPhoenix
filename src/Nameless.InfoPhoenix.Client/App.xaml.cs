using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nameless.InfoPhoenix.Bootstrap;
using Nameless.InfoPhoenix.Client.Infrastructure;
using Nameless.InfoPhoenix.Client.ViewModels;
using Nameless.InfoPhoenix.Client.Views;
using Nameless.InfoPhoenix.Client.Views.Pages;
using Nameless.InfoPhoenix.Infrastructure;
using Nameless.Lucene.DependencyInjection;
using Wpf.Ui;

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
                .Create("--applicationName=INFO PHOENIX")
                .SetConfigureServices(ConfigureServices)
                .SetConfigureContainer(ConfigureContainerBuilder)
                .Build();
        }

        #endregion

        #region Protected Override Methods

        protected override void OnStartup(StartupEventArgs e) {
            _host.Start();

            // Execute bootstrap steps.
            _host.Services.GetRequiredService<IBootstrap>().Run();

            // Open the main window.
            _host.Services.GetRequiredService<IWindow>().Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e) {
            _host.Dispose();

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
                .RegisterRepositories()
                .RegisterBootstrap();

            // WPF-UI Services
            services.AddSingleton<INavigationService, NavigationService>();

            // Window & Pages
            services.AddSingleton<IWindow, MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<HomePage>();
            services.AddSingleton<HomeViewModel>();

            services.AddSingleton<DocumentFolderPage>();
            services.AddSingleton<DocumentFolderViewModel>();

            services.AddSingleton<SettingsPage>();
            services.AddSingleton<SettingsViewModel>();
        }

        private static void ConfigureContainerBuilder(HostBuilderContext context, ContainerBuilder builder) {
            builder
                .RegisterDatabase()
                .RegisterLuceneModule();
        }

        #endregion
    }
}