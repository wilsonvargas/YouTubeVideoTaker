﻿using System;
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

namespace YoutubeVideoTaker.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            PasteCommand = new Command(Paste);
            SearchCommand = new Command(Search);
        }

        public INavigation Navigation { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand SearchCommand { get; set; }

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
            var client = new YoutubeClient();
            Video videoInfo;
            try
            {
                var id = Helper.NormalizeId(Url);
                if (id != "")
                {
                    IsBusy = true;
                    videoInfo = await client.Videos.GetAsync(id);
                    //await Navigation.PushAsync(new DetailPage(videoInfo), true);
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
