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

        public async void ObtenerUsuario(string login, string pass)
        {
            login = login.ToUpper();
            pass = pass.ToUpper();
            try
            {
                string url = "http://www.mauroperelda.somee.com/api/Usuarios/" + login + "/" + pass + "";
                HttpResponseMessage req = cliente.GetAsync(url).Result;
                if (req.IsSuccessStatusCode)
                {
                    var json = req.Content.ReadAsStringAsync().Result;
                    Preferences.Set("login", json);
                    var res = JsonConvert.DeserializeObject<ObservableCollection<MLogin>>(json);
                    if (res.Count == 0)
                    {
                        await DisplayAlert("Mensaje", "Los datos ingresados son incorrectos, Corroborelos", "OK");
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
                        }
                        Application.Current.MainPage = new NavigationPage(new PaginaInicio());
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        //public ObservableCollection<MLogin> GetUsuario(string login, string pass)
        //{
        //    login = login.ToUpper();
        //    pass = pass.ToUpper();
        //    ListaUsuarios = new ObservableCollection<MLogin>();
        //    try
        //    {
        //        string url = "http://www.mauroperelda.somee.com/api/Usuarios/" + login + "/" + pass + "";
        //        HttpResponseMessage req = cliente.GetAsync(url).Result;
        //        if (req.IsSuccessStatusCode)
        //        {
        //            var json = req.Content.ReadAsStringAsync().Result;
        //            Preferences.Set("login",json);
        //            var res = JsonConvert.DeserializeObject<ObservableCollection<MLogin>>(json);
        //            if (res.Count == 0)
        //            {
        //                DisplayAlert("Mensaje", "Los datos ingresados son incorrectos, Corroborelos", "OK");
        //            }
        //            else
        //            {
        //                foreach (var item in res)
        //                {
        //                    MLogin log = new MLogin
        //                    {
        //                        usu_codigo = item.usu_codigo,
        //                        usu_nombre = item.usu_nombre,
        //                        usu_login = item.usu_login,
        //                        usu_contraseña = item.usu_contraseña
        //                    };
        //                    ListaUsuarios.Add(log);
        //                    if (ListaUsuarios != null)
        //                    {
                                
        //                    }
        //                    //DisplayAlert("Mensaje", "Datos correctos", "OK");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return ListaUsuarios;
        //}
    }
}
