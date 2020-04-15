using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Channels;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;

namespace YoutubeVideoTaker.Services.Interfaces
{
    public interface IYouTubeClientService
    {
        YoutubeClient Client { get; }

        Task<IReadOnlyList<ClosedCaptionTrackInfo>> GetClosedCaption(VideoId videoId);
        Task<StreamManifest> GetStreams(VideoId videoId);
        Task<Channel> GetVideoChannel(VideoId videoId);
        Task<Video> GetVideoDescription(VideoId videoId);
    }
}