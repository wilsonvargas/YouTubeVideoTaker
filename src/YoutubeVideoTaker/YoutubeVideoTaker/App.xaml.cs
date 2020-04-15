using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using YoutubeVideoTaker.Services;
using YoutubeVideoTaker.Services.Interfaces;
using YoutubeVideoTaker.Views;

namespace YoutubeVideoTaker
{
    public partial class App : Application
    {
        private INavigationService navigationService;
        public App()
        {
            InitializeComponent();

            Locator.Instance.BuildContainer();
            Locator.Instance.BuildContainer();
            navigationService = Locator.Instance.Resolve<INavigationService>();
            InitNavigation();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void InitNavigation()
        {
            await navigationService.InitializeAsync();
        }
    }
}
