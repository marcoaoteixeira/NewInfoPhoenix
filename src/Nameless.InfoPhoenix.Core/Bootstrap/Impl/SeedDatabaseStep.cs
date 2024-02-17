using Nameless.InfoPhoenix.Entities;
using Nameless.InfoPhoenix.Repositories;
using Nameless.Infrastructure;

namespace Nameless.InfoPhoenix.Bootstrap.Impl {
    public sealed class SeedDatabaseStep : IStep {
        public int Order => 999;

        public bool ThrowOnFailure => false;

        private readonly IApplicationContext _applicationContext;
        private readonly IDocumentFolderRepository _documentFolderRepository;

        public SeedDatabaseStep(IApplicationContext applicationContext, IDocumentFolderRepository documentFolderRepository)
        {
            _applicationContext = applicationContext;
            _documentFolderRepository = documentFolderRepository;
        }

        public void Execute() {
            var loremFolder = Path.Combine(_applicationContext.ApplicationDataFolderPath, "sample_docs", "Lorem");
            var poemsFolder = Path.Combine(_applicationContext.ApplicationDataFolderPath, "sample_docs", "Poems");
            var votesFolder = Path.Combine(_applicationContext.ApplicationDataFolderPath, "sample_docs", "Votes");

            var documentFolders = new DocumentFolder[] {
                new() {
                    ID = Guid.Parse("e6fbd900-bec6-45e0-ba16-557c9cd8cbe5"),
                    Label = "Lorem Ipsun",
                    FolderPath = loremFolder,
                    Order = 1,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = null
                },
                new() {
                    ID = Guid.Parse("93961c74-33c2-436f-a3b8-dbae6b644ad7"),
                    Label = "Beautiful Poems",
                    FolderPath = poemsFolder,
                    Order = 2,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = null
                },
                new() {
                    ID = Guid.Parse("4d888e0f-052e-4d48-aaf8-44febce7d65f"),
                    Label = "Votes",
                    FolderPath = votesFolder,
                    Order = 3,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = null
                }
            };

            foreach (var documentFolder in documentFolders) {
                if (!_documentFolderRepository.Exists(documentFolder.ID)) {
                    _documentFolderRepository.Insert(documentFolder);
                }
            }
        }
    }
}
