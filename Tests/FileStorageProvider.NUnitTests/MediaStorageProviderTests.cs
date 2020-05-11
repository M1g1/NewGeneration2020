using System;
using System.IO;
using FileStorageProvider.Interfaces;
using FileStorageProvider.Providers;
using NUnit.Framework;

namespace FileStorageProvider.NUnitTests
{
    public class MediaStorageProviderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Save_ArgumentPathIsNull_ThrowArgumentNullException()
        {
            IFileStorage mediaStorage = new MediaStorageProvider();

            byte[] content = new byte[] { };
            string path = null;

            Assert.Throws<ArgumentNullException>(() => mediaStorage.Save(content, path));
        }

        [Test]
        public void Save_ArgumentPathIsEmptyOrWhiteSpace_ThrowArgumentException()
        {
            IFileStorage mediaStorage = new MediaStorageProvider();

            byte[] content = new byte[] { };
            string path = " ";

            Assert.Throws<ArgumentException>(() => mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentContentIsNull_ThrowArgumentException()
        {
            IFileStorage mediaStorage = new MediaStorageProvider();

            byte[] content = null;
            string path = "C:\\Users\\User\\Desktop\\MySite\\NewGeneration2020\\Gallery\\Pictures\\Images\\File.png";

            Assert.Throws<ArgumentNullException>(() => mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentPathNonExistentDirectory_ThrowDirectoryNotFoundException()
        {
            IFileStorage mediaStorage = new MediaStorageProvider();

            byte[] content = new byte[] { };
            string path = "C:\\Users\\User\\Desktop\\MySitesr\\p.png";

            Assert.Throws<DirectoryNotFoundException>(() => mediaStorage.Save(content, path));

        }

        [Test]
        public void Save_ArgumentsIsCorrect_ReturnTrue()
        {
            IFileStorage mediaStorage = new MediaStorageProvider();

            byte[] content = new byte[] { };
            string path = "C:\\Users\\User\\Desktop\\MySite\\NewGeneration2020\\Gallery\\Pictures\\Images\\File.png";

            var result = mediaStorage.Save(content, path);

            Assert.AreEqual(true, result);

        }
    }
}