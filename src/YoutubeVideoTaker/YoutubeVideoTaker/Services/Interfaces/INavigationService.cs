using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoutubeVideoTaker.ViewModels;

namespace YoutubeVideoTaker.Services.Interfaces
{
    public interface INavigationService
    {
        Task DisplayAlert(string Title, string Message, string OkButton = "Ok");
        Task InitializeAsync();
        Task NavigateBackAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateToAsync(Type viewModelType);
    }
}
