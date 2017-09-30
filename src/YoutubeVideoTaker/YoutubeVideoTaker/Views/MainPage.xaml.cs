using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeVideoTaker.Interfaces;
using YoutubeVideoTaker.ViewModels;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using YoutubeExplode.Services;

namespace YoutubeVideoTaker.Views
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewModel;
        public MainPage()
        {
            BindingContext = viewModel = new MainPageViewModel();
            viewModel.Navigation = Navigation;
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
        }
        
        //public async void SaveVideoToDisk(string link)
        //{
        //    var client = new YoutubeClient();
        //    VideoInfo videoInfo;
        //    try
        //    {
        //        var id = NormalizeId(link);
        //        videoInfo  = await client.GetVideoInfoAsync(id);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
           
        //    resultado.Text = $"Id: {videoInfo.Id} | Title: {videoInfo.Title} | Author: {videoInfo.Author.Title} | Description: {videoInfo.Description}";
        //    var streamInfo = videoInfo.MixedStreams
        //        .OrderBy(s => s.VideoQuality)
        //        .Last();
        //    string normalizedFileSize = NormalizeFileSize(streamInfo.ContentLength);
        //    resultado.Text += $"Quality: {streamInfo.VideoQualityLabel} | Container: {streamInfo.Container} | Size: {normalizedFileSize}";
        //    string fileExtension = streamInfo.Container.GetFileExtension();
        //    string fileName = $"{videoInfo.Title}.{fileExtension}";
            
        //    var progress = new Progress<double>(p => SetValueToProgressBar(p));

        //    var cancellationToken = new CancellationTokenSource();

        //    await DownloadVideoAsync(streamInfo.Url , progress, cancellationToken.Token, fileName);
            
        //}

        //private void SetValueToProgressBar(double value) {
        //    progressBar.IsVisible = true;
        //    progressBar.Progress = value;
        //    Debug.WriteLine($"Download Progress [{value:P0}]");
        //}

        //const int _downloadImageTimeoutInSeconds = 15;
        //readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };

        //async Task DownloadVideoAsync(string url, IProgress<double> progress, CancellationToken token, string fileName)
        //{
        //    var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);
        //    string path = DependencyService.Get<IFiles>().RootDirectory();
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
        //    }

        //    var total = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
        //    var canReportProgress = total != -1 && progress != null;

        //    using (var stream = await response.Content.ReadAsStreamAsync())
        //    {
        //        var totalRead = 0L;
        //        var buffer = new byte[4096];
        //        var isMoreToRead = true;
        //        do
        //        {
        //            token.ThrowIfCancellationRequested();

        //            var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

        //            if (read == 0)
        //            {
        //                isMoreToRead = false;
        //            }
        //            else
        //            {
        //                var data = new byte[read];
        //                buffer.ToList().CopyTo(0, data, 0, read);

        //                await DependencyService.Get<IFiles>().WriteVideoFile(data, fileName, path);

        //                totalRead += read;

        //                if (canReportProgress)
        //                {
        //                    progress.Report((totalRead * 1d) / (total * 1d));
        //                }
        //            }
        //        } while (isMoreToRead);
             
        //    }
        //}

        //private static string NormalizeFileSize(long fileSize)
        //{
        //    string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        //    double size = fileSize;
        //    int unit = 0;

        //    while (size >= 1024)
        //    {
        //        size /= 1024;
        //        ++unit;
        //    }

        //    return $"{size:0.#} {units[unit]}";
        //}

        //private  void testProgress_Clicked(object sender, EventArgs e)
        //{
        //    progressBar.IsVisible = true;
        //    if (progressBar.AnimationIsRunning("SetProgress"))
        //    {
        //        progressBar.AbortAnimation("SetProgress");
        //    }
        //    else
        //    {
        //        progressBar.Animate("SetProgress", (arg) => { progressBar.Progress = arg; }, 8 * 60, 8 * 1000, Easing.Linear);
        //    }
        //}
    }
}
