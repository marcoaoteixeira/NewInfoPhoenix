using System.Data;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.FileProviders;
using Nameless.Autofac;
using Nameless.Data;
using Nameless.Data.SQLite;
using Nameless.Data.SQLite.Options;

namespace Nameless.InfoPhoenix {
    public static class ContainerBuilderExtension {
        #region Public Static Methods

        public static ContainerBuilder RegisterDatabase(this ContainerBuilder self) {
            const string DB_CONNECTION_FACTORY_KEY = "DB_CONNECTION_FACTORY_69cf6905-c799-402b-8416-2bfa5a81f7ab";

            self
                .Register(ctx => {
                    var options = ctx.GetOptions<SQLiteOptions>();

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
                .OnActivated(DatabaseStartUp)
                .As<IDatabase>()
                .SingleInstance();

            return self;
        }

        #endregion

        #region Private Static Methods

        private static void DatabaseStartUp(IActivatedEventArgs<Database> args) {
            var databaseSchemaFilePath = Path.Combine("sql_scripts", "Database_Schema.sql");
            var fileProvider = args.Context.Resolve<IFileProvider>();
            var databaseSchemaFile = fileProvider.GetFileInfo(databaseSchemaFilePath);

            using var stream = databaseSchemaFile.CreateReadStream();

            var databaseSchemaContent = stream.ToText();

            args.Instance.ExecuteNonQuery(databaseSchemaContent, CommandType.Text);
        }

        #endregion
    }
}
