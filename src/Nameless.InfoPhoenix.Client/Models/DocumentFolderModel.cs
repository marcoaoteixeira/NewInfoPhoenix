using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Client.Models {
    public sealed record DocumentFolderModel {
        #region Public Properties

        public Guid ID { get; init; }
        public string Label { get; init; } = string.Empty;
        public string FolderPath { get; init; } = string.Empty;
        public int Order { get; init; }

        #endregion

        #region Public Static Methods

        public static DocumentFolderModel Convert(DocumentFolder documentFolder)
            => new() {
                ID = documentFolder.ID,
                Label = documentFolder.Label,
                FolderPath = documentFolder.FolderPath,
                Order = documentFolder.Order,
            };

        #endregion
    }
}
