﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stream.Brokers.Storages;
using Stream.Models.Foundations.Videos;
using Tynamix.ObjectFiller;

namespace Stream.Controllers
{
    public class VideoController : Controller
    {
        private readonly IStorageBroker storageBroker;

        public VideoController(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 50; // Har sahifada 50 ta video
            int totalVideos = await this.storageBroker.SelectAllVideoMetadatas().CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalVideos / pageSize);

            List<VideoMetadata> videos = await this.storageBroker
                .SelectAllVideoMetadatas()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageGroupSize = 10; // 10 ta sahifa raqami ko'rinadi

            return View(videos);
        }

        public async Task<IActionResult> AddVideo()
        {
            var filler = CreateVideoFiller();

            for (int i = 0; i < 50000; i++)
            {
                var video = filler.Create();
                await this.storageBroker.InsertVideoMetadataAsync(video);
            }

            return RedirectToAction("Index");
        }

        private static Filler<VideoMetadata> CreateVideoFiller()
        {
            var filler = new Filler<VideoMetadata>();
            var random = new Random();

            string[] titles =
            {
        "Introduction to C#",
        "Blazor for Beginners",
        "ASP.NET Core MVC Guide",
        "Entity Framework Best Practices",
        "Deploying .NET Apps",
        "Microservices with .NET",
        "Cloud Computing with Azure",
        "Building RESTful APIs",
        "Machine Learning in .NET",
        "Advanced LINQ Techniques"
    };

            filler.Setup()
                .OnProperty(video => video.Id).Use(0) // ✅ IDENTITY ustuniga SQL avtomatik qiymat berishi uchun
                .OnProperty(video => video.Title).Use(() => titles[random.Next(titles.Length)]);

            return filler;
        }

    }
}
