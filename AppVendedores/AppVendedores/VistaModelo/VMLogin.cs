using AppVendedores.Modelos;
using AppVendedores.Vistas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppVendedores.VistaModelo
{
    public class VMLogin : BaseViewModel
    {
        HttpClient cliente = new HttpClient();
        private ObservableCollection<MLogin> listaUsuarios;
        public ObservableCollection<MLogin> ListaUsuarios
        {
            get { return listaUsuarios; }
            set { listaUsuarios = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MLogin> GetUsuario(string login, string pass)
        {
            login = login.ToUpper();
            pass = pass.ToUpper();
            ListaUsuarios = new ObservableCollection<MLogin>();
            try
            {
                string url = "http://www.mauroperelda.somee.com/api/Usuarios/" + login + "/" + pass + "";
                HttpResponseMessage req = cliente.GetAsync(url).Result;
                if (req.IsSuccessStatusCode)
                {
                    var json = req.Content.ReadAsStringAsync().Result;
                    var res = JsonConvert.DeserializeObject<ObservableCollection<MLogin>>(json);
                    if (res.Count == 0)
                    {
                        DisplayAlert("Mensaje", "Los datos ingresados son incorrectos, Corroborelos", "OK");
                    }
                    else
                    {
                        foreach (var item in res)
                        {
                            MLogin log = new MLogin
                            {
                                usu_codigo = item.usu_codigo,
                                usu_nombre = item.usu_nombre,
                                usu_login = item.usu_login,
                                usu_contraseña = item.usu_contraseña
                            };
                            ListaUsuarios.Add(log);
                            var serialize = JsonConvert.SerializeObject(ListaUsuarios[0]);
                            if (ListaUsuarios != null && ListaUsuarios.Count > 0)
                            {
                                Preferences.Set("login", serialize);
                                Application.Current.MainPage = new PaginaInicio();
                            }
                        }
                    }
                }
                else
                {
                    DisplayAlert("Mensaje", "Complete Campos", "OK");
                    
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Mensaje",""+ex.Message, "OK");
            }
            return ListaUsuarios;
        }
    }
}
