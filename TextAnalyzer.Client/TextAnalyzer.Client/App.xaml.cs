﻿using System;
using TextAnalyzer.Client.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextAnalyzer.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AuthorizationPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
