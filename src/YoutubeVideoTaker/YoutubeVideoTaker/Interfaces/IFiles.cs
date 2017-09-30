using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeVideoTaker.Interfaces
{
    public interface IFiles
    {
        Task WriteVideoFile(byte[] video, string fileName, string filePath);
        string RootDirectory();
        Task<string>RootDirectoryUWP(string fileName);
    }
}
