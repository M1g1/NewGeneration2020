using System;
using System.IO;
using System.IO.Abstractions;
using FileStorageProvider.Interfaces;

namespace FileStorageProvider.Providers
{
    public class MediaStorageProvider : IFileStorage
    {
        private readonly IFileSystem _fileSystem;


        public MediaStorageProvider(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public bool Save(byte[] content, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            _fileSystem.File.WriteAllBytes(path, content);
            return _fileSystem.File.Exists(path);
        }

        public byte[] ReadBytes(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));

            return _fileSystem.File.ReadAllBytes(path);
        }

        public bool Delete(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));
            if (!_fileSystem.File.Exists(path))
                throw new FileNotFoundException(nameof(path));

            _fileSystem.File.Delete(path);
            return !_fileSystem.File.Exists(path);
        }
    }
}