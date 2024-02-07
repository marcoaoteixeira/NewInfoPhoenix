using Nameless.Infrastructure;

namespace Nameless.InfoPhoenix {

    [SetUpFixture]
    public static class StartUp {
        private static void DeleteDirectories(IApplicationContext applicationContext) {
            // Delete everything inside ApplicationDataFolder
            var appDataDirectory = new DirectoryInfo(applicationContext.ApplicationDataFolderPath);

            // Delete everything inside sub-directories
            foreach (var inner in appDataDirectory.EnumerateDirectories()) {
                inner.Delete(recursive: true);
            }

            // Delete all files inside directory
            foreach (var file in appDataDirectory.GetFiles("*", SearchOption.TopDirectoryOnly)) {
                file.Delete();
            }
        }

        [OneTimeSetUp]
        public static async Task InitAsync() {
            await ServiceHelper.Instance.StartAsync();

            var applicationContext = ServiceHelper.Instance.GetService<IApplicationContext>();
            DeleteDirectories(applicationContext);

            var resourcesFolderPath = Utils.GetResourceFolderPath();

            // Copy files from resources/sql_scripts
            var sqlScriptsSrc = Path.Combine(resourcesFolderPath, "sql_scripts");
            var sqlScriptsDest = Path.Combine(applicationContext.ApplicationDataFolderPath, "sql_scripts");
            new DirectoryInfo(sqlScriptsSrc).DeepCopy(sqlScriptsDest);

            // Copy files from resources/sample_docs
            var docsSrc = Path.Combine(resourcesFolderPath, "sample_docs");
            var docsDest = Path.Combine(applicationContext.ApplicationDataFolderPath, "sample_docs");
            new DirectoryInfo(docsSrc).DeepCopy(docsDest);
        }

        [OneTimeTearDown]
        public static async Task CleanupAsync()
            => await ServiceHelper.Instance.StopAsync();
    }
}