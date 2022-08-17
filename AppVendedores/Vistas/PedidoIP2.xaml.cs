using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class PedidoIP2 : ContentPage
    {
        public static string URL;
        HttpClient cliente = new HttpClient();
        public PedidoIP2(string url, int term)
        {
            InitializeComponent();
            inicializarVista(url,term);
        }

        string ip;
        string term;
        string ruta;
        private void inicializarVista(string url, int term)
        {
            txturl.Text = url;
            terminal.Text = term.ToString();
        }

        private async void btnConectar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txturl.Text) || !string.IsNullOrEmpty(terminal.Text))
            {
                terminalDispositivo();
                IpServidor();
                ComprobarTerminal();
            }
            else
            {
                await DisplayAlert("Ingrese la Conexion", "Se requieren Datos", "OK");
            }
        }

        public async void ComprobarTerminal()
        {
            var numSerie = DeviceInfo.Name;
            URL = URL.Replace('\n', '/');
            string url = "" + URL + "Terminal/" + terminal.Text + "/" + numSerie + "";
            HttpResponseMessage response = cliente.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Mensaje", "Conexion Lista, vuelva a ingresar a la app", "Ok");
                Process.GetCurrentProcess().CloseMainWindow();
            }
            else
            {
                await DisplayAlert("Advertencia", "Debe poner otro numero de terminal", "OK");
            }
        }
        private void IpServidor()
        {
            ip = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
            URL = File.ReadAllText(ip);
            FileInfo file = new FileInfo(ip);
            StreamWriter sw;
            string url = "http://" + txturl.Text + "";
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
                else if (File.Exists(term) == true)
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