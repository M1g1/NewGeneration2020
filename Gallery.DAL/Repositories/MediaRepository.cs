using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Gallery.DAL.Models;

namespace Gallery.DAL
{
    public class MediaRepository : IMediaRepository
    {
        private readonly GalleryDbContext _ctx;

        public MediaRepository(GalleryDbContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public async Task<bool> IsMediaExistByPathAsync(string path)
        {
            return await _ctx.Media.AnyAsync(m => m.Path == path.Trim().ToLower());
        }

        public async Task AddMediaToDatabaseAsync(Media media)
        {
            _ctx.Media.Add(media);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Media> GetMediaByPath(string path)
        {
            return await _ctx.Media.FirstOrDefaultAsync(m => m.Path == path.Trim().ToLower());
        }

        public async Task UpdateMediaDeletionStatusAsync(Media media, bool newStatus)
        {
            media.IsDeleted = newStatus;
            await _ctx.SaveChangesAsync();
        }
    }
}