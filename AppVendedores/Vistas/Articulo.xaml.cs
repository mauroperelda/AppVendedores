using AppVendedores.Modelos;
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
        public Articulo()
        {
            InitializeComponent();
            TraerDatos();
        }

        private void TraerDatos()
        {
            var datos = Preferences.Get("DatosArticulo", "");
            var deserialize = JsonConvert.DeserializeObject<MArticulo>(datos);
            articulo.Text = (deserialize?.art_descri).ToString();
            adicinal.Text = deserialize?.adi_descri.ToString();

            string codTex = deserialize?.art_codtex.ToString();
            string codNum = deserialize?.art_codnum.ToString();
            codigo.Text = $"{codTex} - {codNum}";

            string path = Convert.ToString(deserialize?.imagen);
            imgProducto.Source = path;
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
    }
}