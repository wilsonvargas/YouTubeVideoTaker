using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeVideoTaker.Models;

namespace YoutubeVideoTaker.Utils
{
    public static class Helper
    {
        public static string NormalizeFileSize(long fileSize)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            double size = fileSize;
            int unit = 0;

            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }

            return $"{size:0.#} {units[unit]}";
        }

        public static VideoId NormalizeId(string input)
        {
            VideoId videoId = new VideoId(input);
            return videoId;
        }

        public static List<MediaStreamList> PopulateListGrouped(StreamManifest mediaStreamInfos)
        {
            var mixedStreams = new MediaStreamList();
            var mixed = mediaStreamInfos.GetMuxed().ToList();
            mixedStreams.Heading = "Mixed Downloads";
            foreach (var item in mixed)
            {
                mixedStreams.Add(item);
            }

            var videoStreams = new MediaStreamList();
            var videoS = mediaStreamInfos.GetVideoOnly().ToList();
            videoStreams.Heading = "Video Only Downloads";
            foreach (var item in videoS)
            {
                videoStreams.Add(item);
            }

            var audioStreams = new MediaStreamList();
            var audio = mediaStreamInfos.GetAudioOnly().ToList();
            audioStreams.Heading = "Audio Only Downloads";
            foreach (var item in audio)
            {
                audioStreams.Add(item);
            }

            var list = new List<MediaStreamList> {
               mixedStreams,
               videoStreams,
               audioStreams
            };
            return list;
        }
    }
}
