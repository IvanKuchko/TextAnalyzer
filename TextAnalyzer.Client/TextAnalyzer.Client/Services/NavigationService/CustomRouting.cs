using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace TextAnalyzer.Client.Services.NavigationService
{
    public static class CustomRouting
    {
        private static IDictionary<Type, Type> _dictionary = new Dictionary<Type, Type>();

        public static Page GetPage(Type viewModelType)
        {
            var pageType = _dictionary[viewModelType];
            var page = (Page)Activator.CreateInstance(pageType);
            return page;
        }

        public static object GetOrCreateViewModel(Type pageType)
        {
            var viewModelType = _dictionary.FirstOrDefault(x => x.Value == pageType).Key;
            var viewModel = ViewResolver.Container.GetInstance(viewModelType);
            return viewModel;
        }

        public static void RegisterRoute(Type viewModelType, Type pageType)
        {
            if (!_dictionary.ContainsKey(viewModelType))
                _dictionary.Add(viewModelType, pageType);
            else
                _dictionary[viewModelType] = pageType;
        }

        public static void UnregisterRoutes() => _dictionary.Clear();
    }
}
