﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppVendedores;
using AppVendedores.Vistas;

namespace AppVendedores
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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
