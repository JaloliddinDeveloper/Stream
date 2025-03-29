using Microsoft.EntityFrameworkCore;
using Stream.Models.Foundations.Videos;

namespace Stream.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<VideoMetadata> VideoMetadatas { get; set; }

        public async ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata)=>
           await InsertAsync(videoMetadata);

        public IQueryable<VideoMetadata> SelectAllVideoMetadatas()=>
            SelectAll<VideoMetadata>();

        public async ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata)=>
            await DeleteAsync(videoMetadata);
    }
}
