using Microsoft.Extensions.FileProviders;
using Nameless.Data;
using Nameless.InfoPhoenix.Entities;

namespace Nameless.InfoPhoenix.Repositories.Impl {
    public class DocumentFolderRepositoryTests {
        private static DocumentFolder Seed(Guid? id = null, string? label = null, string? folderPath = null, int? order = null, DateTime? createdAt = null, DateTime? modifiedAt = null) {
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();

            var innerID = id ?? Guid.NewGuid();
            var idPart = innerID.ToString()[..8];
            var file = fileProvider.GetFileInfo("sql_scripts\\DocumentFolder_Insert.sql");
            using var stream = file.CreateReadStream();
            var sql = stream.ToText();
            var document = new DocumentFolder {
                ID = innerID,
                Label = label ?? $"Test Label {idPart}",
                FolderPath = folderPath ?? $"C:\\Temp\\Test_Label_{idPart}",
                Order = order ?? 1,
                CreatedAt = createdAt ?? DateTime.Now,
                ModifiedAt = modifiedAt
            };
            var parameters = document.ToParameters();

            using var transaction = database.BeginTransaction();
            database.ExecuteNonQuery(sql, parameters: parameters);
            transaction.Commit();

            return document;
        }

        [Test]
        public void Insert_Should_Create_A_New_Document_Folder_In_The_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);
            var documentFolder = new DocumentFolder {
                Label = "Label",
                FolderPath = "FolderPath",
                Order = 1,
            };

            // act
            var actual = sut.Insert(documentFolder);

            // assert
            Assert.Multiple(() => {
                Assert.That(actual, Is.True);
                Assert.That(documentFolder.ID, Is.Not.EqualTo(Guid.Empty));
                Assert.That(documentFolder.CreatedAt, Is.EqualTo(DateTime.Now).Within(1).Seconds);
                Assert.That(documentFolder.ModifiedAt, Is.Null);
            });
        }

        [Test]
        public void Select_Should_Return_Document_Folder_From_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);
            var documentFolder = Seed(
                label: "Label_Select",
                folderPath: "FolderPath_Select",
                order: 5
            );

            // act
            var actual = sut.Select(documentFolder.ID);

            // assert
            Assert.Multiple(() => {
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.ID, Is.EqualTo(documentFolder.ID));
                Assert.That(actual.Label, Is.EqualTo(documentFolder.Label));
                Assert.That(actual.FolderPath, Is.EqualTo(documentFolder.FolderPath));
                Assert.That(actual.Order, Is.EqualTo(documentFolder.Order));
                Assert.That(actual.CreatedAt, Is.EqualTo(documentFolder.CreatedAt).Within(1).Seconds);
                Assert.That(actual.ModifiedAt, Is.EqualTo(documentFolder.ModifiedAt));
            });
        }

        [Test]
        public void SelectAll_Should_Return_All_Document_Folder_From_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);
            Seed(
                label: "Label_SelectAll_1",
                folderPath: "FolderPath_SelectAll_1",
                order: 5
            );
            Seed(
                label: "Label_SelectAll_2",
                folderPath: "FolderPath_SelectAll_2",
                order: 5
            );

            // act
            var actual = sut.SelectAll();

            // assert
            Assert.Multiple(() => {
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Has.Length.AtLeast(2));
                Assert.That(actual.Where(docFolder => docFolder.Label.Contains("SelectAll")).ToArray(), Has.Length.AtLeast(2));
            });
        }

        [Test]
        public void Exists_Should_Return_True_If_Document_Folder_Exists_In_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);
            var documentFolder = Seed(
                label: "Label_Exists",
                folderPath: "FolderPath_Exists",
                order: 5
            );

            // act
            var actual = sut.Exists(documentFolder.ID);

            // assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Exists_Should_Return_False_If_Document_Folder_Not_Exists_In_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);

            // act
            var actual = sut.Exists(Guid.NewGuid());

            // assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Delete_Should_Return_True_If_Remove_Document_Folder_From_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);
            var documentFolder = Seed(
                label: "Label_Delete",
                folderPath: "FolderPath_Delete",
                order: 5
            );

            // act
            var existsBefore = sut.Exists(documentFolder.ID);
            var deleted = sut.Delete(documentFolder.ID);
            var existsAfter = sut.Exists(documentFolder.ID);

            // assert
            Assert.Multiple(() => {
                Assert.That(existsBefore, Is.True);
                Assert.That(deleted, Is.True);
                Assert.That(existsAfter, Is.False);
            });
        }

        [Test]
        public void Update_Should_Return_True_If_Update_Document_Folder_In_Database() {
            // arrange
            var database = ServiceHelper.Instance.GetService<IDatabase>();
            var fileProvider = ServiceHelper.Instance.GetService<IFileProvider>();
            var sut = new DocumentFolderRepository(database, fileProvider);
            var documentFolder = Seed(
                label: "Label_Update",
                folderPath: "FolderPath_Update",
                order: 5
            );

            // act
            var current = sut.Select(documentFolder.ID);
            var toUpdate = documentFolder.Clone();
            toUpdate.Label = "Label_UPDATED";
            toUpdate.FolderPath = "FolderPath_UPDATED";
            var update = sut.Update(toUpdate);
            var actual = sut.Select(toUpdate.ID);

            // assert
            Assert.Multiple(() => {
                Assert.That(current, Is.Not.Null);
                Assert.That(actual, Is.Not.Null);
                Assert.That(update, Is.True);
                Assert.That(current.ID, Is.EqualTo(actual.ID));
                Assert.That(current.Label, Is.Not.EqualTo(actual.Label));
                Assert.That(current.FolderPath, Is.Not.EqualTo(actual.FolderPath));
                Assert.That(actual.Label, Is.EqualTo(toUpdate.Label));
                Assert.That(actual.FolderPath, Is.EqualTo(toUpdate.FolderPath));
            });
        }
    }
}
