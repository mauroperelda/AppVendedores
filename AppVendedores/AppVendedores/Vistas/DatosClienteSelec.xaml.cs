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
    public partial class DatosClienteSelec : ContentPage
    {
        VMNuevoPedido vmPedido = new VMNuevoPedido();
        public DatosClienteSelec()
        {
            InitializeComponent();
            pickerFormaPago.ItemsSource = vmPedido.GetFormaPago();
            pickerCondVta.ItemsSource = vmPedido.GetCondVta();
            inicializarVista();
        }

        private void inicializarVista()
        {
            var keys = Preferences.ContainsKey("DatosCliente");
            if (keys)
            {
                var datos = Preferences.Get("DatosCliente", "");
                var deserializer = JsonConvert.DeserializeObject<MNuevoPedido>(datos);
                iva_condicion.Text = deserializer?.iva_condicion;
                cuit.Text = deserializer?.cuit;
                nombreCliente.Text = deserializer?.cli_nombre;
                domicilioCliente.Text = deserializer?.cli_domicilio;
                localidadCliente.Text = deserializer?.loc_nombre;
                codigoFormPago.Text = Convert.ToString(deserializer?.cli_formpag);
                condVta.Text = deserializer?.tip_descri;
                codigoCondVta.Text = Convert.ToString(deserializer?.tip_codigo);
            }
            pickerFormaPago.SelectedIndex = (Convert.ToInt32(codigoFormPago.Text)-1);
            pickerCondVta.SelectedIndex = (Convert.ToInt32(codigoCondVta.Text)-1);
        }
    }
}