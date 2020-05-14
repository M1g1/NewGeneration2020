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
            _mockFileSystem.AddDirectory("test");
            _mediaStorage = new MediaStorageProvider(_mockFileSystem);
        }

        [Test]
        public void Save_ArgumentPathIsNull_ThrowArgumentNullException()
        {
            byte[] content = new byte[] { };

            string path = null;

            Assert.Throws<ArgumentNullException>(() => _mediaStorage.Save(content, path));
        }

        [Test]
        public void Save_ArgumentPathIsEmptyOrWhiteSpace_ThrowArgumentException()
        {
            byte[] content = new byte[] { };

            string path = " ";

            Assert.Throws<ArgumentException>(() => _mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentContentIsNull_ThrowArgumentException()
        {
            byte[] content = null;

            string path = @"C:\test\test.png";

            Assert.Throws<ArgumentNullException>(() => _mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentPathNonExistentDirectory_ThrowDirectoryNotFoundException()
        {
            
            byte[] content = new byte[] { };

            string path = @"C:\Desktop\test.png";

            Assert.Throws<DirectoryNotFoundException>(() => _mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentsIsCorrect_ReturnTrue()
        {

            byte[] content = new byte[] { };

            string path = @"C:\test\test.png";

            var result = _mediaStorage.Save(content, path);

            Assert.AreEqual(true, result);

        }
    }
}