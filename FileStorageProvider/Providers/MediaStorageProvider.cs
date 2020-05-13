using System;
using System.IO;
using FileStorageProvider.Interfaces;

namespace FileStorageProvider.Providers
{
    public class MediaStorageProvider : IFileStorage
    {
        public bool Save(byte[] content, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            File.WriteAllBytes(path, content);
            return File.Exists(path);
        }

        public byte[] ReadBytes(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));

            return File.ReadAllBytes(path);
        }

        public bool Delete(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Argument_EmptyPath", nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));

            File.Delete(path);
            return !File.Exists(path);
        }
    }
}