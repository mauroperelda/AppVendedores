using AppVendedores.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppVendedores.VistaModelo
{
    public class VMNuevoPedido : BaseViewModel
    {
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MNuevoPedido> _listaClientes;
        public ObservableCollection<MNuevoPedido> ListaClientes
        {
            get { return _listaClientes; }
            set { _listaClientes = value; OnPropertyChanged(); }
        }
        private ObservableCollection<MFormaPago> _formaPago;
        public ObservableCollection<MFormaPago> FormaPago
        {
            get { return _formaPago; }
            set { _formaPago = value; OnPropertyChanged(); }
        }
        private ObservableCollection<MCondVenta> _condvta;
        public ObservableCollection<MCondVenta> Condvta
        {
            get { return _condvta; }
            set { _condvta = value; OnPropertyChanged(); }
        }
        public ObservableCollection<MNuevoPedido> GetCliente()
        {
            ListaClientes = new ObservableCollection<MNuevoPedido>();
            try
            {
                string url = "http://24.232.208.83:8000/apivale/clientes.php";
                HttpResponseMessage request = cliente.GetAsync(url).Result;
                if (request.IsSuccessStatusCode)
                {
                    var json = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<ObservableCollection<MNuevoPedido>>(json);
                    foreach (var item in response)
                    {
                        MNuevoPedido Npedido = new MNuevoPedido 
                        {
                            cli_codigo = item.cli_codigo,
                            cli_nombre = item.cli_nombre,
                            cli_domicilio = item.cli_domicilio,
                            iva_condicion = item.iva_condicion,
                            loc_nombre = item.loc_nombre,
                            cod_postal = item.cod_postal,
                            pro_descri = item.pro_descri,
                            cli_formpag = item.cli_formpag,
                            cli_condvta = item.cli_condvta,
                            cuit = item.cuit
                        };
                        ListaClientes.Add(Npedido);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return ListaClientes;
        }
        public ObservableCollection<MFormaPago> GetFormaPago()
        {
            FormaPago = new ObservableCollection<MFormaPago>();
            try
            {
                string url = "http://24.232.208.83:8000/apivale/forma_pago.php";
                HttpResponseMessage req = cliente.GetAsync(url).Result;
                if (req.IsSuccessStatusCode)
                {
                    var json = req.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ObservableCollection<MFormaPago>>(json);
                    foreach (var item in res)
                    {
                        MFormaPago FPago = new MFormaPago
                        {
                            for_codigo = item.for_codigo,
                            for_descri = item.for_descri
                        };
                        FormaPago.Add(FPago);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return FormaPago;
        }
        public ObservableCollection<MCondVenta> GetCondVta()
        {
            Condvta = new ObservableCollection<MCondVenta>();
            try
            {
                string url = "http://24.232.208.83:8000/apivale/condvta.php";
                HttpResponseMessage req = cliente.GetAsync(url).Result;
                if (req.IsSuccessStatusCode)
                {
                    var json = req.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ObservableCollection<MCondVenta>>(json);
                    foreach (var item in res)
                    {
                        MCondVenta CondVta = new MCondVenta
                        {
                            tip_codigo = item.tip_codigo,
                            tip_descri = item.tip_descri
                        };
                        Condvta.Add(CondVta);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return Condvta;
        }
    }
}
