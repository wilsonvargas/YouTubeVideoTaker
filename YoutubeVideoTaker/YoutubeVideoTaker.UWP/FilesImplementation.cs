using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using YoutubeVideoTaker.Interfaces;
using YoutubeVideoTaker.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(FilesImplementation))]
namespace YoutubeVideoTaker.UWP
{
    public class FilesImplementation : IFiles
    {
        public string RootDirectory()
        {
            throw new NotImplementedException();
        }

        public async Task<string> RootDirectoryUWP(string fileName)
        {
            FolderPicker picker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFileToken", file);
            return file.Path;
        }

        

        public async Task WriteVideoFile(byte[] video, string fileName, string filePath)
        {
            try
            {
                var file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync("PickedFileToken");
                using (System.IO.Stream SourceStream = await file.OpenStreamForWriteAsync())
                {
                    SourceStream.Seek(0, System.IO.SeekOrigin.End);
                    await SourceStream.WriteAsync(video, 0, video.Length);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
