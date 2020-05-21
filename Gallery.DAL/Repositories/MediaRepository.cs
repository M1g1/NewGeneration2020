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

        public async Task AddMediaToDatabaseAsync(Media media)
        {
            _ctx.Media.Add(media);
            await _ctx.SaveChangesAsync();
        } 
        public async Task UpdateDeletionStatus(string path, bool newStatus)
        {
            var media = await _ctx.Media.FirstOrDefaultAsync(m => m.Path == path.Trim().ToLower());
            media.IsDeleted = newStatus;
            await _ctx.SaveChangesAsync();
        }
    }
}