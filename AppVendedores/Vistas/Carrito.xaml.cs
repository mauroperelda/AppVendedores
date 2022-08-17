using AppVendedores.Modelos;
using AppVendedores.VistaModelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        public static string ipUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
        public string URL = File.ReadAllText(ipUrl);
        public static string term = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "terminal.txt");
        HttpClient client = new HttpClient();
        int terminal = Convert.ToInt32(File.ReadAllText(term));
        public Carrito()
        {
            InitializeComponent();
            //var term = Preferences.Get("terminal", "");
            //var terminal = JsonConvert.DeserializeObject(term);
            //var vend = Preferences.Get("login", "");
            //var vendedor = JsonConvert.DeserializeObject<MLogin>(vend);

            //var cli = Preferences.Get("DatosCliente", "");
            //var cliente = JsonConvert.DeserializeObject<MNuevoPedido>(cli);

            //ListaArticulos.ItemsSource = car.GetAuxCarrito(vendedor.usu_codigo,Convert.ToInt32(terminal), cliente.cli_codigo);
        }

        protected override void OnAppearing()
        {
            var term = Preferences.Get("terminal", "");
            var terminal = JsonConvert.DeserializeObject(term);
            var vend = Preferences.Get("login", "");
            var vendedor = JsonConvert.DeserializeObject<MLogin>(vend);

            var cli = Preferences.Get("DatosCliente", "");
            var cliente = JsonConvert.DeserializeObject<MNuevoPedido>(cli);

            ListaArticulos.ItemsSource = car.GetAuxCarrito(vendedor.usu_codigo, Convert.ToInt32(terminal), cliente.cli_codigo);
        }
        private void ListaArticulos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MCarrito;
            var serializer = JsonConvert.SerializeObject(item);
            if (item != null)
            {
                Preferences.Set("EditarArticulo", serializer);
                Navigation.PushAsync(new EditarArticulo());
            }
        }

        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
            SwipeItem swipe = sender as SwipeItem;
            var articulo = swipe.BindingContext as MCarrito;

            var term = Preferences.Get("terminal", "");
            var terminal = JsonConvert.DeserializeObject(term);
            var vend = Preferences.Get("login", "");
            var vendedor = JsonConvert.DeserializeObject<MLogin>(vend);
            var usuario = vendedor?.usu_codigo;

            var cli = Preferences.Get("DatosCliente", "");
            var cliente = JsonConvert.DeserializeObject<MNuevoPedido>(cli);
            var c = cliente?.cli_codigo;

            //var datosArt = Preferences.Get("listaCarrito", "");
            //var deserializeArt = JsonConvert.DeserializeObject<MCarrito>(datosArt);
            var codtex = articulo.car_fabrica;
            var codnum = articulo.car_codnum;
            var adicional = articulo.car_adicional;
            var ad = adicional.Length;
            if (adicional == "")
            {
                adicional = "null";
            }
            else 
            {
                adicional = adicional.Replace("/", "*");
            }

            URL = URL.Replace('\n', '/');
            string url = "" + URL + "Carrito/DeleteCarrito/" + usuario + "/" + Convert.ToInt32(terminal) + "/" + c + "/" + codtex + "/" + codnum + "/" + adicional + "";
            //client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Mensaje", "Articulo eliminado del carrito", "OK");
                OnAppearing();
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al eliminar articulo del carrito", "OK");
            }
        }
        int puntoVenta;
        string letraV;
        int numeraV;
        double iva2 = 0;
        double subtotal2 = 0;
        double iva5 = 0;
        double subtotal5 = 0;
        double iva105 = 0;
        double subtotal105 = 0;
        double iva21 = 0;
        double subtotal21 = 0;
        double iva27 = 0;
        double subtotal27 = 0;
        double exento = 0;
        double tsubtotal = 0;
        double total = 0;
        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            var x = Preferences.Get("Totales", "");
            var z = JsonConvert.DeserializeObject(x);
            string[] cad = z.ToString().Split(';');

                iva2 = Convert.ToDouble(cad[0]);
                subtotal2 = Convert.ToDouble(cad[1]);
                iva5 = Convert.ToDouble(cad[2]);
                subtotal5 = Convert.ToDouble(cad[3]);
                iva105 = Convert.ToDouble(cad[4]);
                subtotal105 = Convert.ToDouble(cad[5]);
                iva21 = Convert.ToDouble(cad[6]);
                subtotal21 = Convert.ToDouble(cad[7]);
                iva27 = Convert.ToDouble(cad[8]);
                subtotal27 = Convert.ToDouble(cad[9]);
                exento = Convert.ToDouble(cad[10]);
                tsubtotal = Convert.ToDouble(cad[11]);
                total = Convert.ToDouble(cad[12]);

            var term = Preferences.Get("terminal", "");
            var terminal = JsonConvert.DeserializeObject(term);
            URL = URL.Replace('\n', '/');
            string url2 = ""+URL+"Terminal/"+terminal+"";
            HttpResponseMessage request = client.GetAsync(url2).Result;
            if (request.IsSuccessStatusCode)
            {
                var json2 = request.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<ObservableCollection<MTerminal>>(json2);
                if (res.Count > 0)
                {
                    foreach (var item in res)
                    {
                        puntoVenta = item.ter_ptovta;
                    }
                }
            }

            string url3 = ""+URL+"NumeroComprobante/"+puntoVenta+"";
            HttpResponseMessage req = client.GetAsync(url3).Result;
            if (req.IsSuccessStatusCode)
            {
                var json3 = req.Content.ReadAsStringAsync().Result;
                var res2 = JsonConvert.DeserializeObject<ObservableCollection<MNumComprobante>>(json3);
                if (res2.Count > 0)
                {
                    foreach (var item in res2)
                    {
                        numeraV = item.ptc_numeraV;
                        letraV = item.ptc_letraV;
                    }
                }
            }

            string str = puntoVenta.ToString();
            string puntoV = str.PadLeft(4,'0');

            string str2 = numeraV.ToString();
            string numerav = str2.PadLeft(8,'0');
            string numComp = "" + letraV + "" + puntoV + "-" + numerav + "";


            var vend = Preferences.Get("login", "");
            var vendedor = JsonConvert.DeserializeObject<MLogin>(vend);
            var usuario = vendedor?.usu_codigo;

            var cli = Preferences.Get("DatosCliente", "");
            var cliente = JsonConvert.DeserializeObject<MNuevoPedido>(cli);
            var cli_codigo = cliente?.cli_codigo;
            var cli_zona = cliente?.cli_zona;
            var cli_actividad = cliente?.cli_actividad;
            var cli_categoria = cliente?.cli_categoria;
            var cli_transporte = cliente?.cli_transporte;
            var cli_lisp = cliente?.cli_lisp;
            var cli_nombre = cliente?.cli_nombre;
            var cli_domicilio = cliente?.cli_domicilio;
            var cli_codpos1 = cliente?.cli_codpos1;
            var cli_codpos2 = cliente?.cli_codpos2;
            var cli_localidad = cliente?.loc_nombre;
            var cli_telefono = cliente?.cli_celular;
            var cli_condiva = cliente?.iva_codigo;
            var cli_cuit1 = cliente?.cli_cuit1;
            var cli_cuit2 = cliente?.cli_cuit2;
            var cli_cuit3 = cliente?.cli_cuit3;
            var cli_ingbru = cliente?.cli_ingbru;
            var cli_condvta = cliente?.cli_condvta;
            var cli_formpag = cliente?.cli_formpag;


            string url = ""+URL+"ConfirmarCarrito/Post";
            MConfirmarCarrito car = new MConfirmarCarrito
            {
                ctacli = Convert.ToInt32(cli_codigo),
                zona = Convert.ToInt32(cli_zona),
                actividad = Convert.ToInt32(cli_actividad),
                vendedor = Convert.ToInt32(usuario),
                categoria = Convert.ToInt32(cli_categoria),
                transporte = cli_transporte,
                listap = Convert.ToInt32(cli_lisp),
                nombreCliente = cli_nombre,
                domicilioCliente = cli_domicilio,
                codpos1 = Convert.ToInt32(cli_codpos1),
                codpos2 = Convert.ToInt32(cli_codpos2),
                localidad = cli_localidad,
                telefono = cli_telefono,
                CodigoIva = Convert.ToInt32(cli_condiva),
                cuit1 = cli_cuit1,
                cuit2 = cli_cuit2,
                cuit3 = cli_cuit3,
                ingBruto = cli_ingbru,
                condvta = Convert.ToString(cli_condvta),
                formaPago = Convert.ToInt32(cli_formpag),
                comprobante = numComp,
                subtotal = tsubtotal,
                net27 = subtotal27,
                iva27 = iva27,
                neto21 = subtotal21,
                iva21 = iva21,
                neto105 = subtotal105,
                iva105 = iva105,
                neto5 = subtotal5,
                iva5 = iva5,
                neto2 = subtotal2,
                iva2 = iva2,
                precioTotal = total,
                exento = exento,
                terminal = Convert.ToInt32(terminal)
            };
            var json = JsonConvert.SerializeObject(car);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Mensaje", "Pedido confirmado", "OK");
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al confirmar Pedido", "OK");
            }
            string url4 = ""+URL+"Carrito/Delete/"+Convert.ToInt32(terminal)+"/"+usuario+"/"+cli_codigo+"";
            HttpResponseMessage r = await client.DeleteAsync(url4);
            if (r.IsSuccessStatusCode)
            {
                OnAppearing();
            }
        }
    }
}