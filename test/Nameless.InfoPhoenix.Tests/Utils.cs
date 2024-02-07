namespace Nameless.InfoPhoenix {
    public static class Utils {
        public static string GetResourceFolderPath(params string[] combineWith) {
            // We're at bin/Debug|Release/net6.0/
            var basePath = typeof(Utils).Assembly.GetDirectoryPath();

            // Let's go back 3 levels
            var directory = new DirectoryInfo(basePath);
            var pwd = directory?.Parent?.Parent?.Parent;

            // Now, combite pwd with "resources"
            var resourcesPath = Path.Combine(pwd?.FullName ?? string.Empty, "resources");

            // Finally, combine with params
            return combineWith.IsNullOrEmpty()
                ? resourcesPath
                : Path.Combine(combineWith.Prepend(resourcesPath).ToArray());
        }
    }
}
