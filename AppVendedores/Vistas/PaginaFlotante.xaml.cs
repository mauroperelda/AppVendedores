using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVendedores.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaFlotante : ContentPage
    {
        public static string ipUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
        public static string URL = File.ReadAllText(ipUrl);
        public static string term = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "terminal.txt");
        public static int terminal = Convert.ToInt32(File.ReadAllText(term));
        public PaginaFlotante()
        {
            InitializeComponent();
        }

        private void btnNuevoPedido_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new NuevoPedido()));
        }

        private void btnPedidosPendientes_Clicked(object sender, EventArgs e)
        {

        }

        private void btnDevoluciones_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnConfigConexion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PedidoIP2(URL, terminal));
        }
    }
}