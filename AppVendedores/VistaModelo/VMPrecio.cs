using AppVendedores.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppVendedores.VistaModelo
{
    public class VMPrecio : BaseViewModel
    {
        public static string ipUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
        public static string URL = File.ReadAllText(ipUrl);
        public static string term = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "terminal.txt");
        public static int terminal = Convert.ToInt32(File.ReadAllText(term));
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MPrecio> _listaPrecio;
        public ObservableCollection<MPrecio> ListaPrecio 
        {
            get { return _listaPrecio; }
            set { _listaPrecio = value; OnPropertyChanged(); }
        }

        public async Task<ObservableCollection<MPrecio>> GetPrecioAsync(string art_codtex, int art_codnum, int formaPago, int cli_codigo, double cantidad, int cod_usuario, int condvta)
        {
            ListaPrecio = new ObservableCollection<MPrecio>();
            try
            {
                URL = URL.Replace('\n', '/');
                string url = ""+URL+"Precio/"+art_codtex+"/"+art_codnum+"/"+formaPago+"/"+cli_codigo+"/"+cantidad+"/"+cod_usuario+"/"+condvta+"";
                HttpResponseMessage request = cliente.GetAsync(url).Result;
                if (request.IsSuccessStatusCode)
                {
                    var json = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<ObservableCollection<MPrecio>>(json);
                    if (response.Count > 0)
                    {
                        foreach (var item in response)
                        {
                            MPrecio precio = new MPrecio
                            {
                                desc1 = item.desc1,
                                desc2 = item.desc2,
                                desc3 = item.desc3,
                                desc4 = item.desc4,
                                desc5 = item.desc5,
                                desc6 = item.desc6,
                                desConda = item.desConda,
                                desCondb = item.desCondb,
                                desCondc = item.desCondc,
                                desCondd = item.desCondd,
                                descContado = item.descContado,
                                PrecioUnitario = item.PrecioUnitario
                            };
                            ListaPrecio.Add(precio);
                            var serializePrecio = JsonConvert.SerializeObject(ListaPrecio[0]);
                            Preferences.Set("PrecioYDescuentos",serializePrecio);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Advertencia", "Error al calcular precio", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", ""+request.Headers, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return ListaPrecio;
        }
    }
}
