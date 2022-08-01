using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVendedores.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaInicio : FlyoutPage
    {
        public PaginaInicio()
        {
            InitializeComponent();
            Flyout = new PaginaFlotante();
            Detail = new NavigationPage(new DetallePagFlotante());
        }
    }
}