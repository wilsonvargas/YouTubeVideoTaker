using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YoutubeVideoTaker.Interfaces;
using YoutubeVideoTaker.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(FilesImplementation))]
namespace YoutubeVideoTaker.iOS
{
    public class FilesImplementation : IFiles
    {
        public string RootDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }

        public Task<string> RootDirectoryUWP()
        {
            throw new NotImplementedException();
        }

        public Task<string> RootDirectoryUWP(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task WriteVideoFile(byte[] video, string fileName, string filePath)
        {
            
            string localPath = System.IO.Path.Combine(filePath, fileName);

            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
            using (FileStream fs = new FileStream(localPath, FileMode.Create, FileAccess.Write))
            {
                await fs.WriteAsync(video, 0, video.Length);
            }
        }
    }
}

