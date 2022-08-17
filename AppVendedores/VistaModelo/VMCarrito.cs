using AppVendedores.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace AppVendedores.VistaModelo
{
    public class VMCarrito : BaseViewModel
    {
        public static string ipUrl = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "url.txt");
        public static string URL = File.ReadAllText(ipUrl);
        public static string term = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "terminal.txt");
        public static int terminal = Convert.ToInt32(File.ReadAllText(term));
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MCarrito> _auxCarrito;
        public ObservableCollection<MCarrito> AuxCarrito 
        {
            get { return _auxCarrito; }
            set { _auxCarrito = value; OnPropertyChanged(); }
        }

        double iva2 = 0;
        double subtotal2 = 0;
        double iva5 = 0;
        double subtotal5 = 0;
        double iva105 = 0;
        double subtotal105 = 0;
        double iva21 = 0;
        double subtotal21 = 0;
        double iva27 = 0;
        double subtotal27 = 0;
        double exento = 0;
        double cf = 0;
        double monotributo = 0;
        double tsubtotal = 0;
        double total = 0;
        double TOTAL = 0;
        private void CalcularSubtotal(double precTotal, double iva, double cantidad)
        {
            var datosCliente = Preferences.Get("DatosCliente", "");
            var Cliente = JsonConvert.DeserializeObject<MNuevoPedido>(datosCliente);

            double precioUnitario = precTotal;
            double ivaArticulo = iva;
            int condIva = Convert.ToInt32(Cliente.iva_codigo);
            //total += precioUnitario * cantidad;

            if (condIva == 1)//RI
            {
                double subtotal = 0;
                if (ivaArticulo == 10.5)
                {
                    string dato = 1 + "," + 105;
                    total = precioUnitario * (Convert.ToDouble(dato));
                    subtotal = precioUnitario;
                    iva105 += Convert.ToDouble((total - subtotal).ToString("0.##"));
                    subtotal105 += Convert.ToDouble(subtotal.ToString("0.##"));
                    tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                    TOTAL += Convert.ToDouble(total.ToString("0.##"));
                }
                else if (ivaArticulo == 21)
                {
                    string dato = 1 + "," + 21;
                    total = precioUnitario * (Convert.ToDouble(dato));
                    subtotal = precioUnitario;
                    iva21 += Convert.ToDouble((total - subtotal).ToString("0.##"));
                    subtotal21 += Convert.ToDouble(subtotal.ToString("0.##"));
                    tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                    TOTAL += Convert.ToDouble(total.ToString("0.##"));
                }
                else if (ivaArticulo == 27)
                {
                    string dato = 1 + "," + 27;
                    total = precioUnitario * (Convert.ToDouble(dato));
                    subtotal = precioUnitario;
                    iva27 += Convert.ToDouble((precioUnitario - subtotal).ToString("0.##"));
                    subtotal27 += Convert.ToDouble(subtotal.ToString("0.##"));
                    tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                    TOTAL += Convert.ToDouble(total.ToString("0.##"));
                }
                else if (ivaArticulo == 5)
                {
                    string dato = 1 + "," + 5;
                    total = precioUnitario * (Convert.ToDouble(dato));
                    subtotal = precioUnitario;
                    iva5 += Convert.ToDouble((precioUnitario - subtotal).ToString("0.##"));
                    subtotal5 += Convert.ToDouble(subtotal.ToString("0.##"));
                    tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                    TOTAL += Convert.ToDouble(total.ToString("0.##"));
                }
                else if (ivaArticulo == 2.5)
                {
                    string dato = 1 + "," + 2.5;
                    total = precioUnitario * (Convert.ToDouble(dato));
                    subtotal = precioUnitario;
                    iva2 += Convert.ToDouble((precioUnitario - subtotal).ToString("0.##"));
                    subtotal2 += Convert.ToDouble(subtotal.ToString("0.##"));
                    tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                    TOTAL += Convert.ToDouble(total.ToString("0.##"));
                }
                else
                {

                }
            }
            else if (condIva == 2 || condIva == 6) //MONOT
            {
                double subtotal = 0;
                subtotal = precioUnitario;
                monotributo += Convert.ToDouble(subtotal.ToString("0.##"));
                tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                TOTAL += Convert.ToDouble(subtotal.ToString("0.##"));
            }
            else if (condIva == 3) //CONSUMIDOR FINAL
            {
                double subtotal = 0;
                subtotal = precioUnitario;
                cf += tsubtotal;
                tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                TOTAL += Convert.ToDouble(subtotal.ToString("0.##"));
            }
            else if (condIva == 4)//EXENTO
            {
                double subtotal = 0;
                subtotal = precioUnitario;
                exento += tsubtotal;
                tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                TOTAL += Convert.ToDouble(subtotal.ToString("0.##"));
            }
            else
            {
                double subtotal = 0;
                subtotal = precioUnitario;
                tsubtotal += Convert.ToDouble(subtotal.ToString("0.##"));
                TOTAL += Convert.ToDouble(subtotal.ToString("0.##"));
            }
        }
        public ObservableCollection<MCarrito> GetAuxCarrito(int vendedor, int terminal, int client)
        {
            AuxCarrito = new ObservableCollection<MCarrito>();
            try
            {
                URL = URL.Replace('\n', '/');
                string url = ""+URL+"Carrito/"+vendedor+"/"+terminal+"/"+client+"";
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
                                car_usuario = item.car_usuario,
                                car_imagen = item.car_imagen
                            };
                            CalcularSubtotal(item.car_total, item.car_aliva,item.car_cantidad);
                            AuxCarrito.Add(carrito);
                            var serialize = JsonConvert.SerializeObject(AuxCarrito[0]);
                            if (serialize != null)
                            {
                                Preferences.Set("listaCarrito",serialize);
                            }
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
            string datos = $"{iva2};{subtotal2};{iva5};{subtotal5};{iva105};{subtotal105};{iva21};{subtotal21};{iva27};{subtotal27};{exento};{tsubtotal};{TOTAL}";
            var serializ = JsonConvert.SerializeObject(datos);
            if (serializ != null)
            {
                Preferences.Set("Totales", serializ);
            }
            return AuxCarrito;
        }
    }
}
