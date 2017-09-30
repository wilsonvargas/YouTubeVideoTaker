using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeVideoTaker.Utils
{
    public class MediaStreamList : List<MediaStreamInfo>
    {
        public string Heading { get; set; }
        public List<MediaStreamInfo> Streams { get; set; }
    }
}
