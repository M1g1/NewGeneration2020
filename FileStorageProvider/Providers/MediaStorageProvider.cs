using System;
using FileStorageProvider.Interfaces;

namespace FileStorageProvider.Providers
{
    public class MediaStorageProvider : IFileStorage
    {
        public bool Save(byte[] content, string path)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadBytes(string path)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string path)
        {
            throw new NotImplementedException();
        }
    }
}