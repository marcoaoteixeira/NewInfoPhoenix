namespace Nameless.InfoPhoenix {
    internal static class DirectoryInfoExtension {
        internal static void DeepCopy(this DirectoryInfo self, string destDir) {
            if (!Directory.Exists(destDir)) { Directory.CreateDirectory(destDir); }

            foreach (var dir in Directory.GetDirectories(self.FullName, "*", SearchOption.AllDirectories)) {
                var dirToCreate = dir.Replace(self.FullName, destDir);
                Directory.CreateDirectory(dirToCreate);
            }

            foreach (var newPath in Directory.GetFiles(self.FullName, "*.*", SearchOption.AllDirectories)) {
                File.Copy(newPath, newPath.Replace(self.FullName, destDir), overwrite: true);
            }
        }
    }
}