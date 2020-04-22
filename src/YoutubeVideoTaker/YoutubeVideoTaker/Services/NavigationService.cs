using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YoutubeVideoTaker.Services.Interfaces;
using YoutubeVideoTaker.ViewModels;
using YoutubeVideoTaker.Views;

namespace YoutubeVideoTaker.Services
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> mappings;

        protected virtual Page CurrentPage
        {
            get { return Application.Current.MainPage; }
            set { Application.Current.MainPage = value; }
        }

        public NavigationService()
        {
            mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        public async Task NavigateBackAsync()
        {
            await CurrentPage.Navigation.PopAsync(false);
        }

        public Task DisplayAlert(string Title, string Message, string OkButton = "Ok")
        {
            if (CurrentPage is Page currentPage)
            {
                return currentPage.DisplayAlert(Title, Message, OkButton);
            }
            return Task.FromResult(false);
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<MainPageViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            try
            {
                Type pageType = GetViewTypeForViewModel(viewModelType);
                if (pageType == null)
                {
                    throw new Exception($"Mapping type for {viewModelType} is not a page");
                }

                Page page = Activator.CreateInstance(pageType) as Page;
                ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;                
            }
        }

        protected Type GetViewTypeForViewModel(Type viewModelType)
        {
            if (!mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }
            return mappings[viewModelType];
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            try
            {
                Page page = CreateAndBindPage(viewModelType, parameter);
                

                if (page is MainPage)
                {
                    CurrentPage = new NavigationPage(page);
                }
                else
                {
                    if (CurrentPage is NavigationPage navigationPage)
                    {
                        await navigationPage.PushAsync(page, false);
                    }
                    else
                    {
                        CurrentPage = new NavigationPage(page);
                    }
                }

                await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private void CreatePageViewModelMappings()
        {
            mappings.Add(typeof(MainPageViewModel), typeof(MainPage));
            mappings.Add(typeof(DetailPageViewModel), typeof(DetailPage));
        }
    }
}
