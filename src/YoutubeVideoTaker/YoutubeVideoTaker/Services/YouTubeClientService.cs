using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;
using YoutubeVideoTaker.Services.Interfaces;

namespace YoutubeVideoTaker.Services
{
    public class YouTubeClientService : IYouTubeClientService
    {
        #region Properties

        public YoutubeClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new YoutubeClient();
                }
                return _client;
            }
        }

        private YoutubeClient _client;

        #endregion Properties

        public async Task<StreamManifest> GetStreams(VideoId videoId)
        {
            StreamManifest streamManifest = await Client.Videos.Streams.GetManifestAsync(videoId);
            return streamManifest;
        }

        public async Task<IReadOnlyList<ClosedCaptionTrackInfo>> GetClosedCaption(VideoId videoId)
        {
            ClosedCaptionManifest closedCaptionManifest = await Client.Videos.ClosedCaptions.GetManifestAsync(videoId);
            return closedCaptionManifest.Tracks;
        }

        public async Task<Video> GetVideoDescription(VideoId videoId)
        {
            Video videoDescription = await Client.Videos.GetAsync(videoId);
            return videoDescription;
        }

        public async Task<Channel> GetVideoChannel(VideoId videoId)
        {
            Channel videoChannel = await Client.Channels.GetByVideoAsync(videoId);
            return videoChannel;
        }
    }
}
