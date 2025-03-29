using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.YouTube.v3;

namespace Stream.Brokers.Google
{
    public class GoogleAuthService
    {
        private readonly YouTubeService _youtubeService;

        public GoogleAuthService(IConfiguration configuration)
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = configuration["GoogleAuth:ClientId"],
                ClientSecret = configuration["GoogleAuth:ClientSecret"]
            };

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                new[] { YouTubeService.Scope.YoutubeUpload },
                "user",
                CancellationToken.None).Result;

            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My YouTube Uploader"
            });
        }

        public async Task<string> UploadVideoAsync(string filePath, string title, string description)
        {
            var video = new Video
            {
                Snippet = new VideoSnippet
                {
                    Title = title,
                    Description = description,
                    Tags = new[] { "test", "upload" },
                    CategoryId = "22" // "People & Blogs" kategoriyasi
                },
                Status = new VideoStatus
                {
                    PrivacyStatus = "public" // 'private', 'unlisted', yoki 'public' bo'lishi mumkin
                }
            };

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = _youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                await videosInsertRequest.UploadAsync();
                return videosInsertRequest.ResponseBody.Id;
            }
        }
    }
}

