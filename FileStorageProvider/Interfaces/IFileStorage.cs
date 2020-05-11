namespace FileStorageProvider.Interfaces
{
    public interface IFileStorage
    {
        bool Save(byte [] content, string path);
        byte[] ReadBytes(string path);
        bool Delete(string path);
    }
}