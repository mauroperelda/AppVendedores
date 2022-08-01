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
    public partial class PageCargarArt : ContentPage
    {
        VMBuscarArticulo buscar = new VMBuscarArticulo();
        public PageCargarArt()
        {
            InitializeComponent();
        }

        private void btnBuscarArticulo_Clicked(object sender, EventArgs e)
        {
            if (BuscarArticulo.Text != "")
            {
                ListaArticulos.ItemsSource = buscar.GetArticulo(BuscarArticulo.Text);
            }
            else
            {
                DisplayAlert("Mensaje", "Debe ingresar alguna busqueda", "ok");
            }
        }

        private void ListaArticulos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MArticulo;
            var serialize = JsonConvert.SerializeObject(item);
            if (item != null)
            {
                Preferences.Set("DatosArticulo", serialize);
                Navigation.PushAsync(new Articulo());
            }
        }
    }
}