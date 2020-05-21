using System;
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
    }
}