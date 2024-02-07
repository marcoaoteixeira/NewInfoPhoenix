namespace Nameless.InfoPhoenix {
    /// <summary>
    /// This class was defined to be an entrypoint for this project assembly.
    /// 
    /// *** DO NOT IMPLEMENT ANYTHING HERE ***
    /// 
    /// But, it's allow to use this class as a repository for all constants or
    /// default values that we'll use throughout this project.
    /// </summary>
    public static class Root {
        #region Public Static Inner Classes

        public static class Defaults {
            #region Public Constants

            public const string GUID = "00000000-0000-0000-0000-000000000000";

            #endregion

            #region Public Static Read-Only Properties

            /// <summary>
            /// Gets the valid documents extensions: .doc, .docx, .rtf
            /// </summary>
            public static string[] ValidDocumentExtensions { get; } = [".doc", ".docx", ".rtf"];

            #endregion
        }

        #endregion

        #region Internal Static Inner Classes

        internal static class Indexing {
            #region Internal Constants

            public const string INDEX_NAME = "INFO_PHOENIX_IDX";

            #endregion
        }

        #endregion
    }
}
