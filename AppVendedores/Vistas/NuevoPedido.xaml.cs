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
    public partial class NuevoPedido : ContentPage
    {
        VMNuevoPedido vmPedido = new VMNuevoPedido();
        public NuevoPedido()
        {
            InitializeComponent();
            ListaClientes.ItemsSource = vmPedido.GetClientes(BuscarCliente.Text);
        }

        private void ListaClientes_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MNuevoPedido;
            var serialize = JsonConvert.SerializeObject(item);
            if (item != null)
            {
                Preferences.Set("DatosCliente", serialize);
                Navigation.PushAsync(new DatosClienteSelec());
            }
        }

        private void btnBuscarCliente_Clicked(object sender, EventArgs e)
        {
            ListaClientes.ItemsSource = vmPedido.GetClientes(BuscarCliente.Text).Where(c => c.cli_nombre.ToUpper().Contains(BuscarCliente.Text.ToUpper()));
        }
    }
}