using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppVendedores;
using AppVendedores.Vistas;
using Ubicacion_Articulos.Vistas;
using Ubicacion_Articulos.VistaModelo;

namespace AppVendedores
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ComprobarIP();
            //MainPage = new NavigationPage(new PedidoIP());
        }

        private void ComprobarIP()
        {
            VMusuario usu = new VMusuario();
            int resultado = usu.ComprobarConexion();

            if (resultado == 0)
            {
                MainPage = new NavigationPage(new PedidoIP());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
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
