using AppVendedores.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace AppVendedores.VistaModelo
{
    public class VMCarrito : BaseViewModel
    {
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MCarrito> _auxCarrito;
        public ObservableCollection<MCarrito> AuxCarrito 
        {
            get { return _auxCarrito; }
            set { _auxCarrito = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MCarrito> GetAuxCarrito(int vendedor, int terminal)
        {
            AuxCarrito = new ObservableCollection<MCarrito>();
            try
            {
                string url = "http://24.232.208.83:8085/Carrito/" + vendedor + "/" + terminal + "";
                HttpResponseMessage request = cliente.GetAsync(url).Result;
                if (request.IsSuccessStatusCode)
                {
                    var json = request.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<ObservableCollection<MCarrito>>(json);
                    if (response.Count > 0)
                    {
                        foreach (var item in response)
                        {
                            var carrito = new MCarrito
                            {
                                car_terminal = item.car_terminal,
                                car_fabrica = item.car_fabrica,
                                car_codnum = item.car_codnum,
                                car_articulo = item.car_articulo,
                                car_adicional = item.car_adicional,
                                car_aliva = item.car_aliva,
                                car_cantidad = item.car_cantidad,
                                car_categoria = item.car_categoria,
                                car_cn = item.car_cn,
                                car_condiva = item.car_condiva,
                                car_ctacont = item.car_ctacont,
                                car_descuento = item.car_descuento,
                                car_dtoa = item.car_dtoa,
                                car_dtob = item.car_dtob,
                                car_dtoc = item.car_dtoc,
                                car_dtod = item.car_dtod,
                                car_dtoe = item.car_dtoe,
                                car_dtof = item.car_dtof,
                                car_dtoCondA = item.car_dtoCondA,
                                car_dtoCondB = item.car_dtoCondB,
                                car_dtoCondC = item.car_dtoCondC,
                                car_dtoCondD = item.car_dtoCondD,
                                car_dtoctdo = item.car_dtoctdo,
                                car_medida = item.car_medida,
                                car_oferta = item.car_oferta,
                                car_orden = item.car_orden,
                                car_plista = item.car_plista,
                                car_preccosto = item.car_preccosto,
                                car_punitario = item.car_punitario,
                                car_total = item.car_total,
                                car_usuario = item.car_usuario
                            };
                            AuxCarrito.Add(carrito);
                        }
                    }
                }
                else
                {
                    DisplayAlert("Advertencia", "Error al mostrar datos en el carrito", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje", "" + ex.Message, "OK");
            }
            return AuxCarrito;
        }
    }
}
