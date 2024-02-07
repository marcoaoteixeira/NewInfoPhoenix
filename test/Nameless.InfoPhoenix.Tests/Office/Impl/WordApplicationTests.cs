using Nameless.Infrastructure;

namespace Nameless.InfoPhoenix.Office.Impl {
    public class WordApplicationTests {
        [Test]
        public void Open_Should_Return_Document_From_FilePath() {
            // arrange
            var applicationDataFolder = ServiceHelper.Instance.GetService<IApplicationContext>().ApplicationDataFolderPath;
            var documentFilePath = Path.Combine(applicationDataFolder, "sample_docs", "Lorem", "Lorem_Ipsum.docx");
            using var sut = new WordApplication();

            // act
            var doc = sut.Open(documentFilePath);

            // assert
            Assert.Multiple(() => {
                Assert.That(doc, Is.Not.Null);
                Assert.That(doc.GetContent(formatted: false), Does.Contain("Why do we use it?"));
            });
        }

        [Test]
        public void ReleaseDocuments_Should_Close_Opened_Documents() {
            // arrange
            var applicationDataFolder = ServiceHelper.Instance.GetService<IApplicationContext>().ApplicationDataFolderPath;
            var documentFilePath = Path.Combine(applicationDataFolder, "sample_docs", "Lorem", "Lorem_Ipsum.docx");

            // act
            WordDocumentStatus statusBeforeRelease;
            WordDocumentStatus statusAfterRelease;

            IWordDocument doc;
            using (var sut = new WordApplication())
            using (doc = sut.Open(documentFilePath)) {
                statusBeforeRelease = doc.Status;
            }
            statusAfterRelease = doc.Status;

            // assert
            Assert.Multiple(() => {
                Assert.That(statusBeforeRelease, Is.EqualTo(WordDocumentStatus.Opened));
                Assert.That(statusAfterRelease, Is.EqualTo(WordDocumentStatus.Closed));
            });
        }
    }
}
