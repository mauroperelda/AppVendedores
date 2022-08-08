using System;
using System.Collections.Generic;
//using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
//using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Diagnostics;

namespace Ubicacion_Articulos.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoIP : ContentPage
    {
        public PedidoIP()
        {
            InitializeComponent();            
        }

        string nameBD;
        string ip;
        string term;
        string ruta;
        int IdUsuario;
        string url;
        //"Data Source = 192.168.1.210\\MASER_INF;Initial Catalog =MAURO; User ID = sa; Password=1220; MultipleActiveResultSets=True";
        private async void btnConectar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txturl.Text) || !string.IsNullOrEmpty(terminal.Text))
            {
                terminalDispositivo();
                IpServidor();
                await DisplayAlert("Mensaje", "Conexion Lista, vuelva a ingresar a la app", "Ok");
                Process.GetCurrentProcess().CloseMainWindow();
            }
            else
            {
                await DisplayAlert("Ingrese la Conexion", "Se requieren Datos", "OK");
            }
        }



        private void IpServidor()
        {
            ip = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
            FileInfo file = new FileInfo(ip);
            StreamWriter sw;
            string url = "http://"+txturl.Text+"";
            try
            {
                if (File.Exists(ip) == false)
                {
                    sw = File.CreateText(ip);
                    sw.WriteLine(url);
                    sw.Flush(); //Guardo el archivo
                    sw.Close(); //Cierro el archivo
                }
                else if (File.Exists(ip) == true)
                {
                    File.Delete(ip); //Elimino el archivo
                    sw = File.CreateText(ip);
                    sw.WriteLine(url);
                    sw.Flush(); //Guardo el archivo
                    sw.Close(); //Cierro el archivo
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
        }

        private void terminalDispositivo()
        {
            term = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "terminal.txt");
            StreamWriter sw;
            try
            {
                if (File.Exists(term) == false)
                {
                    sw = File.CreateText(term);
                    sw.WriteLine(terminal.Text);
                    sw.Flush(); //Guardo el archivo
                    sw.Close(); //Cierro el archivo
                }
                else if (File.Exists(ruta) == true)
                {
                    File.Delete(term); //Elimino el archivo
                    sw = File.CreateText(term);
                    sw.WriteLine(terminal.Text);
                    sw.Flush(); //Guardo el archivo
                    sw.Close(); //Cierro el archivo
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
        }
    }
}