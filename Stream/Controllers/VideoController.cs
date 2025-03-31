using Microsoft.AspNetCore.Mvc;
using Stream.Brokers.Storages;
using Stream.Models.Foundations.Videos;

namespace Stream.Controllers
{
    public class VideoController:Controller
    {
      private readonly IStorageBroker storageBroker;

        public VideoController(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<VideoMetadata> videos = this.storageBroker.SelectAllVideoMetadatas();
            return View(videos);
        }

        public IActionResult AddVideo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(VideoMetadata video)
        {
            await this.storageBroker.InsertVideoMetadataAsync(video);
            return RedirectToAction("Index");
        }
    }
}
