using AppVendedores.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace AppVendedores.VistaModelo
{
    public class VMBuscarArticulo : BaseViewModel
    {
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MArticulo> _listaArticulos;
        public ObservableCollection<MArticulo> ListaArticulos
        {
            get { return _listaArticulos; }
            set { _listaArticulos = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MArticulo> GetArticulo(string descripcion)
        {
            ListaArticulos = new ObservableCollection<MArticulo>();
            try
            {
                string url = "http://24.232.208.83:8085/Articulos/" + descripcion + "";
                HttpResponseMessage request = cliente.GetAsync(url).Result;
                if (request.IsSuccessStatusCode)
                {
                    var json = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<ObservableCollection<MArticulo>>(json);
                    if (response.Count > 0)
                    {
                        foreach (var item in response)
                        {
                            MArticulo NArticulo = new MArticulo
                            {
                                art_descri = item.art_descri,
                                imagen = item.imagen,
                                Codigo = item.Codigo,
                                CBarra = item.CBarra,
                                art_codtex = item.art_codtex,
                                art_codnum = item.art_codnum,
                                art_codfab = item.art_codfab,
                                art_codinterno = item.art_codinterno,
                                adi_codigo = item.adi_codigo,
                                adi_descri = item.adi_descri,
                                art_aliva = item.art_aliva,
                                art_preclista = item.art_preclista,
                                art_cn = item.art_cn,
                                art_preccosto = item.art_preccosto,
                                art_precmayo = item.art_precmayo,
                                art_precmino = item.art_precmino,
                                art_precventa = item.art_precventa,
                                art_ctacont = item.art_ctacont
                            };
                            ListaArticulos.Add(NArticulo);
                        }
                    }
                    else
                    {
                        DisplayAlert("Advertencia", "No se encontraron datos en la BD", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return ListaArticulos;
        }
    }
}
