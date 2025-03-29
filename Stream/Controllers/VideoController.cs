using Microsoft.AspNetCore.Mvc;
using Stream.Brokers.Google;
using Stream.Brokers.Storages;
using Stream.Models.Foundations.Videos;
using System.Threading.Tasks;

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
            IQueryable videos = this.storageBroker.SelectAllVideoMetadatas();
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
            return View("Index");
        }
    }
}
