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
                codigoCondVta.Text = Convert.ToString(deserializer?.cli_condvta);
            }

            var objFormPago = pickerFormaPago.ItemsSource;

            for (int i = 0; i < objFormPago.Count; i++)
            {
                MFormaPago item = (MFormaPago)objFormPago[i];
                if (item?.for_codigo == Convert.ToInt32(codigoFormPago.Text))
                {
                    pickerFormaPago.SelectedIndex = i;
                    codFormPago.Text = item.for_codigo.ToString();
                    descriFormPago.Text = item.for_descri.ToString();
                }
            }

            var objCondVenta = pickerCondVta.ItemsSource;

            for (int i = 0; i < objCondVenta.Count; i++)
            {
                MCondVenta item = (MCondVenta)objCondVenta[i];
                if (item?.tip_codigo == Convert.ToInt32(codigoCondVta.Text))
                {
                    pickerCondVta.SelectedIndex = i;
                    codigoCondVta.Text = item.tip_codigo.ToString();
                    descriCondVta.Text = item.tip_descri.ToString();
                }
            }
        }

        private void btnComenzarPedido_Clicked(object sender, EventArgs e)
        {
            MFormaPago formpago = new MFormaPago
            {
                for_codigo = Convert.ToInt32(codFormPago.Text),
                for_descri = descriFormPago.Text
            };
            var serializeFormPago = JsonConvert.SerializeObject(formpago);
            Preferences.Set("FormaPago", serializeFormPago);

            MCondVenta condVta = new MCondVenta
            {
                tip_codigo = Convert.ToInt32(codigoCondVta.Text),
                tip_descri = descriCondVta.Text
            };
            var serializeCondVta = JsonConvert.SerializeObject(condVta);
            Preferences.Set("CondVenta", serializeCondVta);

            Navigation.PushAsync(new PageCargarArt());
        }

        private void pickerFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            MFormaPago item = new MFormaPago();
            item = (MFormaPago)picker.ItemsSource[selectedIndex];

            if (selectedIndex != -1)
            {
                codFormPago.Text = item.for_codigo.ToString();
                descriFormPago.Text = item.for_descri.ToString();
            }
        }

        private void pickerCondVta_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            MCondVenta item = new MCondVenta();
            item = (MCondVenta)picker.ItemsSource[selectedIndex];

            if (selectedIndex != -1)
            {
                codigoCondVta.Text = item.tip_codigo.ToString();
                descriCondVta.Text = item.tip_descri.ToString();
            }
        }
    }
}