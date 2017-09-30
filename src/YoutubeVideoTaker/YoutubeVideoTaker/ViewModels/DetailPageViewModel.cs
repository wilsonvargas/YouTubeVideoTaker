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
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using static YoutubeVideoTaker.Views.DetailPage;

namespace YoutubeVideoTaker.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        const int _downloadImageTimeoutInSeconds = 15;
        readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };
        #region Properties
        private VideoInfo _video;

        public VideoInfo Video
        {
            get { return _video; }
            set { SetProperty(ref _video, value); }
        }

        private List<MediaStreamList> _listOfStreamInfo;

        public List<MediaStreamList> ListOfStreamInfo
        {
            get { return _listOfStreamInfo; }
            set { SetProperty(ref _listOfStreamInfo, value); }
        }

        private double _progress;

        public double Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        private long _labelProgress;

        public long LabelProgress
        {
            get { return _labelProgress; }
            set { SetProperty(ref _labelProgress, value); }
        }

        private long _totalDownload;

        public long TotalDownload
        {
            get { return _totalDownload; }
            set { SetProperty(ref _totalDownload, value); }
        }

        private bool _isComplete;

        public bool IsComplete
        {
            get { return _isComplete; }
            set { SetProperty(ref _isComplete, value); }
        }




        #endregion

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        public DetailPageViewModel() {
            

        }

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
                case Device.Windows:
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
                var buffer = new byte[4096];
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

        public void SetValueToProgressBar(double value)
        {            
            Progress = value;
        }




    }
    }
