using System;
using System.Collections.Generic;
using System.Text;

namespace AppVendedores.Modelos
{
    public class MNuevoPedido
    {
        public int cli_codigo { get; set; }
        public string cli_nombre { get; set; }
        public string cli_domicilio { get; set; }
        public string cli_telefono { get; set; }
        public string cli_celular { get; set; }
        public string cuit { get; set; }
        public string iva_condicion { get; set; }
        public int iva_codigo { get; set; }
        public string cod_postal { get; set; }
        public string loc_nombre { get; set; }
        public string loc_provin { get; set; }
        public string pro_descri { get; set; }
        public int cli_formpag { get; set; }
        public int cli_condvta { get; set; }
        public string tip_descri { get; set; }
        public int cli_categoria { get; set; }
        public string cat_descri { get; set; }

    }
}
