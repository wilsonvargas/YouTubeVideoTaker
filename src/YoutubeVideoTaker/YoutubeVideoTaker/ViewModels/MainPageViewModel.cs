using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YoutubeVideoTaker.Interfaces;
using YoutubeVideoTaker.Utils;
using YoutubeVideoTaker.Views;
using YoutubeExplode;
using Plugin.Clipboard;
using YoutubeExplode.Videos;
using YoutubeVideoTaker.Services.Interfaces;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Channels;
using YoutubeVideoTaker.Models;

namespace YoutubeVideoTaker.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IYouTubeClientService youTubeClientService;
        public MainPageViewModel(INavigationService navigationService, IYouTubeClientService youTubeClientService) : base(navigationService)
        {
            this.youTubeClientService = youTubeClientService;
        }
        public ICommand PasteCommand => new Command(() => Paste());
        public ICommand SearchCommand => new Command(() => Search());

        #region Properties

        public string MessageError
        {
            get { return _messageError; }
            set { SetProperty(ref _messageError, value); }
        }

        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }

        private string _messageError;
        private string _url;

        #endregion Properties

        private async void Paste()
        {
            IsBusy = false;
            MessageError = string.Empty;
            string clipboardText = await CrossClipboard.Current.GetTextAsync();
            Url = clipboardText;
        }

        private async void Search()
        {
            MessageError = string.Empty;
            try
            {
                VideoId videoId = Helper.NormalizeId(Url);
                if (videoId.Value != string.Empty)
                {
                    IsBusy = true;
                    StreamManifest streamManifest = await youTubeClientService.GetStreams(videoId);
                    IReadOnlyList<ClosedCaptionTrackInfo> closedCaptionTrackInfos = await youTubeClientService.GetClosedCaption(videoId);
                    Video video = await youTubeClientService.GetVideoDescription(videoId);
                    Channel channel = await youTubeClientService.GetVideoChannel(videoId);

                    ResumeVideo resumeVideo = new ResumeVideo();
                    resumeVideo.AudioOnlyStreamInfos = streamManifest.GetAudioOnly().ToList();
                    resumeVideo.Channel = channel;
                    resumeVideo.ClosedCaptionTrackInfos = closedCaptionTrackInfos;
                    resumeVideo.MuxedStreamInfos = streamManifest.GetMuxed().ToList();
                    resumeVideo.Video = video;
                    resumeVideo.VideoOnlyStreamInfos = streamManifest.GetVideoOnly().ToList();   

                    await navigationService.NavigateToAsync<DetailPageViewModel>(resumeVideo);
                    IsBusy = false;
                }
                else
                {
                    MessageError = "Url is not valid";
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                MessageError = "Error: " + ex.Message;
            }
        }
    }
}
