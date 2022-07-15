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
    public partial class DetallePagFlotante : ContentPage
    {
        public DetallePagFlotante()
        {
            InitializeComponent();
            inicializarVista();
        }

        private void inicializarVista()
        {
            bool usuarioLogueado = Preferences.ContainsKey("login");
            if (usuarioLogueado)
            {
                var login = Preferences.Get("login", "");
                var deserializer = JsonConvert.DeserializeObject<MLogin>(login);
                Usuario.Text = deserializer?.usu_nombre;
            }
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}