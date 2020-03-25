namespace Gallery.Service
{
    public interface IHashService
    {
        string ComputeSha256Hash(string rawData);
    }
}