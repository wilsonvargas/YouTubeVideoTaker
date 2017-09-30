using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using YoutubeVideoTaker.Interfaces;
using YoutubeVideoTaker.Droid;
using Java.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(FilesImplementation))]
namespace YoutubeVideoTaker.Droid
{
    public class FilesImplementation : IFiles
    {
        public string RootDirectory()
        {
            File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            return path.AbsolutePath;
        }

        public Task<string> RootDirectoryUWP(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task WriteVideoFile(byte[] video, string fileName, string filePath)
        {
            string ff = System.IO.Path.Combine(filePath, fileName);
            using (System.IO.FileStream SourceStream = System.IO.File.Open(ff, System.IO.FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, System.IO.SeekOrigin.End);
                await SourceStream.WriteAsync(video, 0, video.Length);
            }
        }
    }
}