using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using FileStorageProvider.Interfaces;
using FileStorageProvider.Providers;
using NUnit.Framework;

namespace FileStorageProvider.NUnitTests
{
    public class MediaStorageProviderTests
    {
        private MockFileSystem _mockFileSystem;
        private IFileStorage _mediaStorage;

        [SetUp]
        public void Setup()
        {
            _mockFileSystem = new MockFileSystem();
            _mediaStorage = new MediaStorageProvider(_mockFileSystem);
        }

        [Test]
        public void Save_ArgumentPathIsNull_ThrowArgumentNullException()
        {
            _mockFileSystem.AddDirectory("SaveTest");

            byte[] content = new byte[] { };

            string path = null;

            Assert.Throws<ArgumentNullException>(() => _mediaStorage.Save(content, path));
        }

        [Test]
        public void Save_ArgumentPathIsEmptyOrWhiteSpace_ThrowArgumentException()
        {
            _mockFileSystem.AddDirectory("SaveTest");

            byte[] content = new byte[] { };

            string path = " ";

            Assert.Throws<ArgumentException>(() => _mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentContentIsNull_ThrowArgumentNullException()
        {
            _mockFileSystem.AddDirectory("SaveTest");

            byte[] content = null;

            string path = @"C:\SaveTest\test.png";

            Assert.Throws<ArgumentNullException>(() => _mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentPathNonExistentDirectory_ThrowDirectoryNotFoundException()
        {
            _mockFileSystem.AddDirectory("SaveTest");

            byte[] content = new byte[] { };

            string path = @"C:\Desktop\test.png";

            Assert.Throws<DirectoryNotFoundException>(() => _mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentsIsCorrect_ReturnTrue()
        {
            _mockFileSystem.AddDirectory("SaveTest");

            byte[] content = new byte[] { };

            string path = @"C:\SaveTest\test.png";

            var result = _mediaStorage.Save(content, path);

            Assert.AreEqual(true, result);

        }

        [Test]
        public void ReadBytes_ArgumentPathIsNull_ThrowArgumentNullException()
        {
            _mockFileSystem.AddFile(@"ReadBytesTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = null;

            Assert.Throws<ArgumentNullException>(() => _mediaStorage.ReadBytes(path));

        }

        [Test]
        public void ReadBytes_ArgumentPathIsEmptyOrWhiteSpace_ThrowArgumentException()
        {
            _mockFileSystem.AddFile(@"ReadBytesTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = " ";

            Assert.Throws<ArgumentException>(() => _mediaStorage.ReadBytes(path));

        }
        [Test]
        public void ReadBytes_ArgumentPathNonExistentFile_ThrowFileNotFoundException()
        {

            _mockFileSystem.AddFile(@"ReadBytesTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = @"C:\ReadBytesTest\af.png";

            Assert.Throws<FileNotFoundException>(() => _mediaStorage.ReadBytes(path));

        }

        [Test]
        public void ReadBytes_ArgumentsIsCorrect_ReturnByteArray()
        {

            _mockFileSystem.AddFile(@"ReadBytesTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = @"C:\ReadBytesTest\test.png";

            var result = _mediaStorage.ReadBytes(path);

            byte[] expected = new byte[] { 0 };

            Assert.AreEqual(expected, result);

        }

        [Test]
        public void Delete_ArgumentPathIsNull_ThrowArgumentNullException()
        {

            _mockFileSystem.AddFile(@"DeleteTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = null;

            Assert.Throws<ArgumentNullException>(() => _mediaStorage.Delete(path));

        }

        [Test]
        public void Delete_ArgumentPathIsEmptyOrWhiteSpace_ThrowArgumentException()
        {

            _mockFileSystem.AddFile(@"DeleteTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = "  ";

            Assert.Throws<ArgumentException>(() => _mediaStorage.Delete(path));

        }
        [Test]
        public void Delete_ArgumentPathNonExistentFile_ThrowFileNotFoundException()
        {

            _mockFileSystem.AddFile(@"DeleteTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = @"C:\DeleteTest\af.png";

            Assert.Throws<FileNotFoundException>(() => _mediaStorage.Delete(path));

        }

        [Test]
        public void Delete_ArgumentsIsCorrect_ReturnTrue()
        {

            _mockFileSystem.AddFile(@"DeleteTest\test.png", new MockFileData(new byte[] { 0 }));

            string path = @"C:\DeleteTest\test.png";

            var result = _mediaStorage.Delete(path);

            Assert.AreEqual(true, result);

        }
    }
}