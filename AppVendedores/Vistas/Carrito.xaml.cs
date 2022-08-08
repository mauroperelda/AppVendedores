using AppVendedores.Modelos;
using AppVendedores.VistaModelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVendedores.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carrito : ContentPage
    {
        VMCarrito car = new VMCarrito();
        public Carrito()
        {
            InitializeComponent();
            var term = Preferences.Get("terminal", "");
            var terminal = JsonConvert.DeserializeObject(term);
            var vend = Preferences.Get("login", "");
            var vendedor = JsonConvert.DeserializeObject<MLogin>(vend);

            var cli = Preferences.Get("DatosCliente", "");
            var cliente = JsonConvert.DeserializeObject<MNuevoPedido>(cli);

            ListaArticulos.ItemsSource = car.GetAuxCarrito(vendedor.usu_codigo,Convert.ToInt32(terminal), cliente.cli_codigo);
        }
    }
}