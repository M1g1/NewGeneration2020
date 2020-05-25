using System.IO;
using System.Threading.Tasks;
using FileStorageProvider.Interfaces;
using Gallery.DAL;
using Moq;
using NUnit.Framework;

namespace Gallery.Service.NUnitTests
{
    public class ImageServiceTests
    {
        private Mock<IMediaRepository> _mediaRepoMock;
        private Mock<IUserRepository> _userRepoMock;
        private Mock<IFileStorage> _storMock;
        private IImageService _imgService;

        [SetUp]
        public void Setup()
        {
            _mediaRepoMock = new Mock<IMediaRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _storMock = new Mock<IFileStorage>();
            _imgService = new ImageService(_storMock.Object, _mediaRepoMock.Object, _userRepoMock.Object);
        }

        [Test]
        public async Task DeleteAsync_CorrectArgumetns_ReturnTrue()
        {
            const string path = @"C:\Users\User\Desktop\MySite\NewGeneration2020\Gallery\Pictures\Images\6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b\wood-mouse-3179257_960_720.jpg";


            _storMock.Setup(sm => sm.Delete(path)).Returns(true);
            var result = await _imgService.DeleteAsync(path);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteAsync_PathWithNonexistentDir_ThrowDirectoryNotFoundException()
        {
            const string path = @"C:\Users\User\Desktop\MySite\NewGeneration2020\Gallery\Pictures\Images\634fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b\wood-mouse-3179257_960_720.jpg";

            _storMock.Setup(sm => sm.Delete(path)).Throws<DirectoryNotFoundException>();

            Assert.Throws<DirectoryNotFoundException>(() => _imgService.DeleteAsync(path).GetAwaiter().GetResult());
        }

        [Test]
        public async Task DeleteAsync_PathWithNonexistentFile_ThrowFileNotFoundException()
        {
            const string path = @"C:\Users\User\Desktop\MySite\NewGeneration2020\Gallery\Pictures\Images\6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b\woo-3179257_960_720.jpg";

            _storMock.Setup(sm => sm.Delete(path)).Throws<FileNotFoundException>();

            Assert.Throws<FileNotFoundException>(() => _imgService.DeleteAsync(path).GetAwaiter().GetResult());
        }
    }
}