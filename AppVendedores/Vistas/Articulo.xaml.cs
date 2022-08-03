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
    public partial class Articulo : ContentPage
    {
        VMPrecio p = new VMPrecio();
        public Articulo()
        {
            InitializeComponent();
            TraerDatos();
        }

        private void TraerDatos()
        {
            var datosArt = Preferences.Get("DatosArticulo", "");
            var deserializeArt = JsonConvert.DeserializeObject<MArticulo>(datosArt);
            articulo.Text = (deserializeArt?.art_descri).ToString();
            adicinal.Text = deserializeArt?.adi_descri.ToString();

            codtex.Text = deserializeArt?.art_codtex.ToString();
            codnum.Text = deserializeArt?.art_codnum.ToString();
            codigo.Text = $"{codtex.Text} - {codnum.Text}";

            string path = Convert.ToString(deserializeArt?.imagen);
            imgProducto.Source = path;

            var datoCliente = Preferences.Get("DatosCliente", "");
            var deseriaCliente = JsonConvert.DeserializeObject<MNuevoPedido>(datoCliente);
            CodCliente.Text = Convert.ToString(deseriaCliente?.cli_codigo);

            var formapago = Preferences.Get("FormaPago", "");
            var dFormaPago = JsonConvert.DeserializeObject<MFormaPago>(formapago);
            formaPago.Text = Convert.ToString(dFormaPago?.for_codigo);

            var CondVta = Preferences.Get("CondVenta", "");
            var dConVta = JsonConvert.DeserializeObject<MCondVenta>(CondVta);
            condVta.Text = Convert.ToString(dConVta?.tip_codigo);

            var vend = Preferences.Get("login", "");
            var dVendedor = JsonConvert.DeserializeObject<MLogin>(vend);
            vendedor.Text = Convert.ToString(dVendedor?.usu_codigo);

            p.GetPrecio(codtex.Text,Convert.ToInt32(codnum.Text), Convert.ToInt32(formaPago.Text), Convert.ToInt32(CodCliente.Text),Convert.ToDouble(Cantidad.Text), Convert.ToInt32(vendedor.Text), Convert.ToInt32(condVta.Text));

            var PrecioYDesc = Preferences.Get("PrecioYDescuentos", "");
            var deseriaPrecio = JsonConvert.DeserializeObject<MPrecio>(PrecioYDesc);
            PrecioUnitario.Text = Convert.ToString(deseriaPrecio?.PrecioUnitario);
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

        double total;
        double descuento;
        double PrecioFinal;
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

            descuento = total - Convert.ToDouble(PrecioUnitario.Text);

            PrecioFinal = total * Convert.ToDouble(Cantidad.Text);
            PrecioFinal = Convert.ToDouble(PrecioFinal.ToString("0.##")); //FORMATEO EL NUMERO FINAL CON 2 DECIMALES
        }
        private void btnMenos_Clicked(object sender, EventArgs e)
        {
            double cantidadTotal = Convert.ToDouble(Cantidad.Text);
            cantidadTotal = cantidadTotal - 1;
            if (cantidadTotal >= 0)
            {
                Cantidad.Text = cantidadTotal.ToString();
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
        }

        private void btnConfArticulo_Clicked(object sender, EventArgs e)
        {
            CalcularPrecioTotal();
        }
    }
}