using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeVideoTaker.Services.Interfaces;
using YoutubeVideoTaker.ViewModels;

namespace YoutubeVideoTaker.Services
{
    class Locator
    {
        public static Locator Instance
        {
            get
            {
                return instance;
            }
        }

        private static readonly Locator instance = new Locator();
        private Container container;

        public void BuildContainer()
        {
            container = new Container();

            RegisterSingleton<INavigationService, NavigationService>();
            RegisterSingleton<IYouTubeClientService, YouTubeClientService>();

            Register<MainPageViewModel>();
            Register<DetailPageViewModel>();
        }

        public void BuildEmptyContainer()
        {
            container = new Container();
        }

        public void Register<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
        }

        public void Register<T>() where T : class
        {
            container.Register<T>();
        }

        public void RegisterInstance<TService>(TService Instance) where TService : class
        {
            container.RegisterInstance(Instance);
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>(Lifestyle.Singleton);
        }

        public T Resolve<T>() where T : class
        {
            return container.GetInstance<T>();
        }

        public object Resolve(Type type)
        {
            return container.GetInstance(type);
        }
    }
}
