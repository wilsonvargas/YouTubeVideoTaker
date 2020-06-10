using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeVideoTaker.Interfaces;
using YoutubeVideoTaker.Utils;
using static YoutubeVideoTaker.Views.DetailPage;
using YoutubeExplode.Videos;
using YoutubeVideoTaker.Services.Interfaces;
using YoutubeVideoTaker.Models;
using System.Windows.Input;
using YoutubeVideoTaker.Constants;

namespace YoutubeVideoTaker.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public DetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }       

        public override Task InitializeAsync(object navigationData)
        {
            ResumeVideo = (ResumeVideo)navigationData;
            DescriptionVisibilityIndicator = IconConstants.ArrowDown;
            VideosVisibilityIndicator = IconConstants.ArrowDown;
            return base.InitializeAsync(navigationData);
        }

        #region Properties

        public bool IsComplete
        {
            get { return _isComplete; }
            set { SetProperty(ref _isComplete, value); }
        }

        public long LabelProgress
        {
            get { return _labelProgress; }
            set { SetProperty(ref _labelProgress, value); }
        }

        public double Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        public long TotalDownload
        {
            get { return _totalDownload; }
            set { SetProperty(ref _totalDownload, value); }
        }

        public Video Video
        {
            get { return _video; }
            set { SetProperty(ref _video, value); }
        }

        public bool IsDescriptionVisible
        {
            get { return isDescriptionVisible; }
            set { SetProperty(ref isDescriptionVisible, value); }
        }


        public bool IsListVideoVisible
        {
            get { return isListVideoVisible; }
            set { SetProperty(ref isListVideoVisible, value); }
        }

        public bool IsDownloading
        {
            get { return isDownloading; }
            set { SetProperty(ref isDownloading, value); }
        }

        public ResumeVideo ResumeVideo
        {
            get { return resumeVideo; }
            set { SetProperty(ref resumeVideo, value); }
        }      

        public string DescriptionVisibilityIndicator
        {
            get { return descriptionVisibilityIndicator; }
            set { SetProperty(ref descriptionVisibilityIndicator, value); }
        }       

        public string VideosVisibilityIndicator
        {
            get { return videosVisibilityIndicator; }
            set { SetProperty(ref videosVisibilityIndicator, value); }
        }

        private string videosVisibilityIndicator;
        private string descriptionVisibilityIndicator;
        private bool isDescriptionVisible;
        private bool isListVideoVisible;
        private bool isDownloading;
        private bool _isComplete;
        private long _labelProgress;
        private double _progress;
        private long _totalDownload;
        private Video _video;
        private ResumeVideo resumeVideo;

        #endregion Properties

        #region Commands
        public ICommand ShowDescriptionCommand => new Command(() => ShowDescription());
        public ICommand ShowVideosCommand => new Command(() => ShowVideos());
        #endregion

        private const int _downloadImageTimeoutInSeconds = 15;
        private readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };


        public async Task DownloadVideoAsync(string url, IProgress<double> progress, CancellationToken token, string fileName)
        {
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);
            string path = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    path = DependencyService.Get<IFiles>().RootDirectory();
                    break;

                case Device.Android:
                    path = DependencyService.Get<IFiles>().RootDirectory();
                    break;

                case Device.UWP:
                    path = await DependencyService.Get<IFiles>().RootDirectoryUWP(fileName);
                    break;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("The request returned with HTTP status code {0}", response.StatusCode));
            }

            var total = response.Content.Headers.ContentLength.HasValue ? response.Content.Headers.ContentLength.Value : -1L;
            TotalDownload = total;
            var canReportProgress = total != -1 && progress != null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var totalRead = 0L;
                var buffer = new byte[2048];
                var isMoreToRead = true;
                do
                {
                    token.ThrowIfCancellationRequested();

                    var read = await stream.ReadAsync(buffer, 0, buffer.Length, token);

                    if (read == 0)
                    {
                        isMoreToRead = false;
                    }
                    else
                    {
                        var data = new byte[read];
                        buffer.ToList().CopyTo(0, data, 0, read);

                        await DependencyService.Get<IFiles>().WriteVideoFile(data, fileName, path);

                        totalRead += read;

                        if (canReportProgress)
                        {
                            LabelProgress = totalRead;
                            progress.Report((totalRead * 1d) / (total * 1d));
                        }
                    }
                    IsComplete = !isMoreToRead;
                } while (isMoreToRead);
            }
        }

        private void ShowDescription()
        {
            IsDescriptionVisible = !IsDescriptionVisible;
            DescriptionVisibilityIndicator = (IsDescriptionVisible) ? IconConstants.ArrowUp : IconConstants.ArrowDown;
        }

        private void ShowVideos()
        {
            IsListVideoVisible = !IsListVideoVisible;
            VideosVisibilityIndicator = (IsListVideoVisible) ? IconConstants.ArrowUp : IconConstants.ArrowDown;
        }
    }
}
