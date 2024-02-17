using Autofac;
using Nameless.Autofac;
using Nameless.Data;
using Nameless.Data.SQLite;
using Nameless.Data.SQLite.Options;
using Nameless.Infrastructure;

namespace Nameless.InfoPhoenix {
    public static class ContainerBuilderExtension {
        #region Public Static Methods

        public static ContainerBuilder RegisterDatabase(this ContainerBuilder self) {
            const string DB_CONNECTION_FACTORY_KEY = $"{nameof(DbConnectionFactory)}::69cf6905-c799-402b-8416-2bfa5a81f7ab";

            self
                .Register(ctx => {
                    var applicationContext = ctx.Resolve<IApplicationContext>();
                    var options = ctx.GetOptions<SQLiteOptions>();
                    var databasePath = Path.Combine(applicationContext.ApplicationDataFolderPath, "database.db");

                    options.DatabaseName = databasePath;

                    return new DbConnectionFactory(options);
                })
                .Named<IDbConnectionFactory>(DB_CONNECTION_FACTORY_KEY)
                .SingleInstance();

            self
                .Register(ctx => {
                    var dbConnectionFactory = ctx
                        .ResolveNamed<IDbConnectionFactory>(DB_CONNECTION_FACTORY_KEY);
                    var logger = ctx.GetLogger<Database>();

                    return new Database(dbConnectionFactory, logger);
                })
                .As<IDatabase>()
                .SingleInstance();

            return self;
        }

        #endregion
    }
}
