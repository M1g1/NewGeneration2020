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
            if (path.Length == 0)
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
            if (path.Length == 0)
                throw new ArgumentException("Argument_EmptyPath", nameof(path));

            return File.ReadAllBytes(path);
        }

        public bool Delete(string path)
        {
            throw new NotImplementedException();
        }
    }
}