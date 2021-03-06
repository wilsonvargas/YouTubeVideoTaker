﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeVideoTaker.Utils;
using YoutubeVideoTaker.ViewModels;
//using YoutubeExplode.Models;
//using YoutubeExplode.Models.MediaStreams;
using Plugin.LocalNotifications;
//using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Videos;

namespace YoutubeVideoTaker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    NavigationPage.SetHasNavigationBar(this, false);
                    break;
            }
        }

        private void listMedia_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var item = (MediaStreamInfo)e.SelectedItem;
            //try
            //{
            //    var result = await App.Current.MainPage.DisplayAlert("YouTube Downloader", "Do you want download: " +
            //    viewModel.Video.Title + "." + item.Container.ToString() + Environment.NewLine +
            //    "File Size: " + Helper.NormalizeFileSize(item.Size) + "?", "Yes", "No");
            //    if (result)
            //    {
            //        showMediaDownloads.Text = "md-keyboard-arrow-down";
            //        listMedia.IsVisible = false;
            //        containerDownload.IsVisible = true;
            //        string fileExtension = item.Container.GetFileExtension();
            //        string fileName = $"{viewModel.Video.Title}.{fileExtension}";

            //        var progress = new Progress<double>(p => viewModel.SetValueToProgressBar(p));

            //        var cancellationToken = new CancellationTokenSource();

            //        await viewModel.DownloadVideoAsync(item.Url, progress, cancellationToken.Token, fileName);
            //        //CrossLocalNotifications.Current.Show(viewModel.Video.Title, "Download Completed!");
            //    }
            //    else
            //    {
            //        ((ListView)sender).SelectedItem = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
    }
}
