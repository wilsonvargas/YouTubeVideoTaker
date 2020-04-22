using System.Collections.Generic;
using YoutubeExplode.Channels;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;

namespace YoutubeVideoTaker.Models
{
    public class ResumeVideo
    {
        public Video Video { get; set; }
        public Channel Channel { get; set; }
        public IReadOnlyList<ClosedCaptionTrackInfo> ClosedCaptionTrackInfos { get; set; }

        public List<MediaStreamList> MediaStreamList { get; set; }
    }
}
