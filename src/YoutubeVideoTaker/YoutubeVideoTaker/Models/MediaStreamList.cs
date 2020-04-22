using System;
using System.Collections.Generic;
using System.Text;
using YoutubeExplode.Videos.Streams;

namespace YoutubeVideoTaker.Models
{
    public class MediaStreamList : List<IStreamInfo>
    {
        public string Heading { get; set; }
        public List<IStreamInfo> All { private set; get; }
    }
}
