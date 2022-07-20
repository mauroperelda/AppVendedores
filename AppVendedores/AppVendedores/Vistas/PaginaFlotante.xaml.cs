using System;
using System.Collections.Generic;
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
    }
}