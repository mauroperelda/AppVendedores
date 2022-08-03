using AppVendedores.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace AppVendedores.VistaModelo
{
    public class VMPrecio : BaseViewModel
    {
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MPrecio> _listaPrecio;
        public ObservableCollection<MPrecio> ListaPrecio 
        {
            get { return _listaPrecio; }
            set { _listaPrecio = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MPrecio> GetPrecio(string art_codtex, int art_codnum, int formaPago, int cli_codigo, double cantidad, int cod_usuario, int condvta)
        {
            ListaPrecio = new ObservableCollection<MPrecio>();
            try
            {
                string url = "http://24.232.208.83:8085/Precio/"+art_codtex+"/"+art_codnum+"/"+formaPago+"/"+cli_codigo+"/"+cantidad+"/"+cod_usuario+"/"+condvta+"";
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
                        DisplayAlert("Advertencia", "Error al calcular precio", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return ListaPrecio;
        }
    }
}
