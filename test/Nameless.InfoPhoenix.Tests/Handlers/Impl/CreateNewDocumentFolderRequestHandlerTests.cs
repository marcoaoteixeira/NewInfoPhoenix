using MediatR;
using Nameless.InfoPhoenix.Requests;

namespace Nameless.InfoPhoenix.Handlers.Impl {
    public class CreateNewDocumentFolderRequestHandlerTests {
        [Test]
        public async Task Create_A_New_Document_Folder() {
            // arrange
            var mediator = ServiceHelper.Instance.GetService<IMediator>();
            var request = new CreateNewDocumentFolderRequest {
                Label = nameof(Create_A_New_Document_Folder),
                FolderPath = "\\e84f1770-0daa-4361-b2b2-f7938fe7a558"
            };

            // act
            var response = await mediator.Send(request);

            // assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response.Succeeded, Is.True, response.Error);
            });
        }
    }
}
