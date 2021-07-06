using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace TextAnalyzer.Client.Services.SettingsService
{
    public class SettingsService : ISettingsService
    {
        private const string AccessToken = nameof(AccessToken);
        private const string RefreshToken = nameof(RefreshToken);
        public string GetAccessToken() => Preferences.Get(nameof(AccessToken), null);
        public string GetRefreshToken() => Preferences.Get(nameof(RefreshToken), null);
        public void SetAccessToken(string accessToken) => Preferences.Set(nameof(AccessToken), accessToken);
        public void SetRefreshToken(string refreshToken) => Preferences.Set(nameof(RefreshToken), refreshToken);
    }
}
