using System.Threading.Tasks;

namespace Gallery
{
    public interface IGalleryConfiguration
    {
        string GetPathToSave();
        string GetAvailableImageTypes();
    }
}
