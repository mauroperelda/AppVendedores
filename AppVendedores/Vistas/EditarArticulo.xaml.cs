using AppVendedores.Modelos;
using AppVendedores.VistaModelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVendedores.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarArticulo : ContentPage
    {
        public static string ipUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
        public string URL = File.ReadAllText(ipUrl);
        public static string term = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "terminal.txt");
        public int terminal = Convert.ToInt32(File.ReadAllText(term));
        VMPrecio p = new VMPrecio();
        HttpClient client = new HttpClient();
        string url;
        double total;
        double descuento;
        double PrecioFinal;
        double PU;
        public EditarArticulo()
        {
            InitializeComponent();
            TraerDatos();
        }

        private void TraerDatos()
        {
            var datosArt = Preferences.Get("EditarArticulo", "");
            var deserializeArt = JsonConvert.DeserializeObject<MCarrito>(datosArt);
            articulo.Text = (deserializeArt?.car_articulo).ToString();
            adicional.Text = deserializeArt?.car_adicional.ToString();
            art_cn.Text = deserializeArt?.car_cn.ToString();
            codtex.Text = deserializeArt?.car_fabrica.ToString();
            codnum.Text = deserializeArt?.car_codnum.ToString();
            codigo.Text = $"{codtex.Text} - {codnum.Text}";
            art_aliva.Text = deserializeArt?.car_aliva.ToString();
            ctacont.Text = deserializeArt?.car_ctacont.ToString();
            art_medida.Text = deserializeArt?.car_medida.ToString();
            art_plista.Text = deserializeArt?.car_plista.ToString();
            art_preccosto.Text = deserializeArt?.car_preccosto.ToString();
            Cantidad.Text = deserializeArt?.car_cantidad.ToString();
            car_orden.Text = deserializeArt?.car_orden.ToString();
            imagenArt.Text = deserializeArt?.car_imagen;
            imgProducto.Source = imagenArt.Text;

            var datoCliente = Preferences.Get("DatosCliente", "");
            var deseriaCliente = JsonConvert.DeserializeObject<MNuevoPedido>(datoCliente);
            CodCliente.Text = Convert.ToString(deseriaCliente?.cli_codigo);
            condiva.Text = Convert.ToString(deseriaCliente?.iva_codigo);
            cli_categoria.Text = Convert.ToString(deseriaCliente?.cli_categoria);

            var formapago = Preferences.Get("FormaPago", "");
            var dFormaPago = JsonConvert.DeserializeObject<MFormaPago>(formapago);
            formaPago.Text = Convert.ToString(dFormaPago?.for_codigo);

            var CondVta = Preferences.Get("CondVenta", "");
            var dConVta = JsonConvert.DeserializeObject<MCondVenta>(CondVta);
            condVta.Text = Convert.ToString(dConVta?.tip_codigo);

            var vend = Preferences.Get("login", "");
            var dVendedor = JsonConvert.DeserializeObject<MLogin>(vend);
            vendedor.Text = Convert.ToString(dVendedor?.usu_codigo);

            p.GetPrecioAsync(codtex.Text, Convert.ToInt32(codnum.Text), Convert.ToInt32(formaPago.Text), Convert.ToInt32(CodCliente.Text), Convert.ToDouble(Cantidad.Text), Convert.ToInt32(vendedor.Text), Convert.ToInt32(condVta.Text));

            var PrecioYDesc = Preferences.Get("PrecioYDescuentos", "");
            var deseriaPrecio = JsonConvert.DeserializeObject<MPrecio>(PrecioYDesc);
            PrecioUnitario.Text = Convert.ToString(deseriaPrecio?.PrecioUnitario);
            PU = Convert.ToDouble(deseriaPrecio?.PrecioUnitario);
            desc1.Text = Convert.ToString(deseriaPrecio?.desc1);
            desc2.Text = Convert.ToString(deseriaPrecio?.desc2);
            desc3.Text = Convert.ToString(deseriaPrecio?.desc3);
            desc4.Text = Convert.ToString(deseriaPrecio?.desc4);
            desc5.Text = Convert.ToString(deseriaPrecio?.desc5);
            desc6.Text = Convert.ToString(deseriaPrecio?.desc6);
            desConda.Text = Convert.ToString(deseriaPrecio?.desConda);
            desCondb.Text = Convert.ToString(deseriaPrecio?.desCondb);
            desCondc.Text = Convert.ToString(deseriaPrecio?.desCondc);
            desCondd.Text = Convert.ToString(deseriaPrecio?.desCondd);
            descContado.Text = Convert.ToString(deseriaPrecio?.descContado);
        }

        public void CalcularPrecioTotal()
        {
            total = Convert.ToDouble(PrecioUnitario.Text);
            //DESCUENTOS POR SECUENCIA
            total = total + (total * (Convert.ToDouble(desc1.Text) / 100));
            total = total + (total * (Convert.ToDouble(desc2.Text) / 100));
            total = total + (total * (Convert.ToDouble(desc3.Text) / 100));
            total = total + (total * (Convert.ToDouble(desc4.Text) / 100));
            total = total + (total * (Convert.ToDouble(desc5.Text) / 100));
            total = total + (total * (Convert.ToDouble(desc6.Text) / 100));
            //DESCUENTOS POR CONDICION DE VENTA
            total = total + (total * (Convert.ToDouble(desConda.Text) / 100));
            total = total + (total * (Convert.ToDouble(desCondb.Text) / 100));
            total = total + (total * (Convert.ToDouble(desCondc.Text) / 100));
            total = total + (total * (Convert.ToDouble(desCondd.Text) / 100));
            //DESCUENTO SI ES DE CONTADO
            total = total + (total * (Convert.ToDouble(descContado.Text) / 100));

            total = Convert.ToDouble(total.ToString("0.##"));
            descuento = total - Convert.ToDouble(PrecioUnitario.Text);
            descuento = Convert.ToDouble(descuento.ToString("0.##"));
            PrecioFinal = total;
            PrecioFinal = Convert.ToDouble(PrecioFinal.ToString("0.##")); //FORMATEO EL NUMERO FINAL CON 2 DECIMALES

        }
        protected override void OnAppearing()
        {
            PrecioUnitario.Text = Convert.ToString(PU * Convert.ToDouble(Cantidad.Text));
        }
        private void btnMenos_Clicked(object sender, EventArgs e)
        {
            double cantidadTotal = Convert.ToDouble(Cantidad.Text);
            cantidadTotal = cantidadTotal - 1;
            if (cantidadTotal >= 0)
            {
                Cantidad.Text = cantidadTotal.ToString();
                OnAppearing();
            }
            else
            {
                cantidadTotal = 0;
                DisplayAlert("Mensaje", "La cantidad no puede ser menor a 0", "Ok");
            }
        }

        private void btnMas_Clicked(object sender, EventArgs e)
        {
            double cantidadTotal = Convert.ToDouble(Cantidad.Text);
            cantidadTotal = cantidadTotal + 1;
            Cantidad.Text = cantidadTotal.ToString();
            OnAppearing();
        }
        public async void EditarCarrito()
        {
            URL = URL.Replace('\n', '/');
            url = "" + URL + "Carrito/Put";
            MCarrito car = new MCarrito
            {
                car_terminal = terminal,
                car_fabrica = codtex.Text,
                car_codnum = Convert.ToInt32(codnum.Text),
                car_articulo = articulo.Text,
                car_aliva = Convert.ToDouble(art_aliva.Text),
                car_cantidad = Convert.ToDouble(Cantidad.Text),
                car_punitario = PU,
                car_descuento = descuento,
                car_total = PrecioFinal,
                car_dtoa = Convert.ToDouble(desc1.Text),
                car_dtob = Convert.ToDouble(desc2.Text),
                car_dtoc = Convert.ToDouble(desc3.Text),
                car_dtod = Convert.ToDouble(desc4.Text),
                car_dtoe = Convert.ToDouble(desc5.Text),
                car_dtof = Convert.ToDouble(desc6.Text),
                car_dtoCondA = Convert.ToDouble(desConda.Text),
                car_dtoCondB = Convert.ToDouble(desCondb.Text),
                car_dtoCondC = Convert.ToDouble(desCondc.Text),
                car_dtoCondD = Convert.ToDouble(desCondd.Text),
                car_dtoctdo = Convert.ToDouble(descContado.Text),
                car_plista = Convert.ToDouble(art_plista.Text),
                car_preccosto = Convert.ToDouble(art_preccosto.Text),
                car_cn = Convert.ToDouble(art_cn.Text),
                car_usuario = Convert.ToInt32(vendedor.Text),
                car_adicional = adicional.Text,
                car_oferta = 1,
                car_ctacont = Convert.ToInt32(ctacont.Text),
                car_categoria = Convert.ToInt32(cli_categoria.Text),
                car_condiva = Convert.ToInt32(condiva.Text),
                car_medida = art_medida.Text,
                car_ctacli = Convert.ToInt32(CodCliente.Text),
                car_orden = Convert.ToInt32(car_orden.Text)
            };
            var json = JsonConvert.SerializeObject(car);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(url, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Mensaje", "Se ha modificado con exito", "OK");
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al modificar", "OK");
            }
        }
        private void btnConfArticulo_Clicked(object sender, EventArgs e)
        {
            CalcularPrecioTotal();
            EditarCarrito();
        }
    }
}