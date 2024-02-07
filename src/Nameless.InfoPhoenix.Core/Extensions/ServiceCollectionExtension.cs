using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Nameless.InfoPhoenix.Office;
using Nameless.InfoPhoenix.Office.Impl;
using Nameless.InfoPhoenix.Repositories;
using Nameless.InfoPhoenix.Repositories.Impl;
using Nameless.InfoPhoenix.Services;
using Nameless.InfoPhoenix.Services.Impl;
using Nameless.Infrastructure;
using NLog.Extensions.Logging;

namespace Nameless.InfoPhoenix {
    public static class ServiceCollectionExtension {
        #region Public Static Methods

        public static IServiceCollection RegisterFileProvider(this IServiceCollection self)
            => self
                .AddSingleton<IFileProvider>(provider => {
                    var applicationContext = provider.GetRequiredService<IApplicationContext>();
                    var root = applicationContext.ApplicationDataFolderPath;
                    return new PhysicalFileProvider(root);
                });

        public static IServiceCollection RegisterNLog(this IServiceCollection self)
            => self
                .AddLogging(configure => {
                    configure.ClearProviders();
                    configure.AddNLog();
                });

        public static IServiceCollection RegisterMediatR(this IServiceCollection self, params Assembly[] supportAssemblies)
            => self
                .AddMediatR(setup
                    => setup.RegisterServicesFromAssemblies(supportAssemblies)
                );

        public static IServiceCollection RegisterRepositories(this IServiceCollection self)
            => self
                .AddSingleton<IDocumentFolderRepository, DocumentFolderRepository>()
                .AddSingleton<IDocumentRepository, DocumentRepository>();

        public static IServiceCollection RegisterOffice(this IServiceCollection self)
            => self
                .AddKeyedSingleton<IWordApplication, WordApplication>("WORD")
                .AddSingleton<IOfficeSuite>(provider => {
                    var wordApplication = provider.GetRequiredKeyedService<IWordApplication>("WORD");
                    var logger = provider
                        .GetRequiredService<ILoggerFactory>()
                        .CreateLogger<OfficeSuite>();

                    return new OfficeSuite(wordApplication, logger);
                });

        public static IServiceCollection RegisterServices(this IServiceCollection self)
            => self
                .AddSingleton<IDocumentFolderService, DocumentFolderService>()
                .AddSingleton<IDocumentService, DocumentService>()
                .AddSingleton<IIndexingService, IndexingService>();

        #endregion
    }
}
