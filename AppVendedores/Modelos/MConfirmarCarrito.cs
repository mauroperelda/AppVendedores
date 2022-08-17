using System;
using System.Collections.Generic;
using System.Text;

namespace AppVendedores.Modelos
{
    public class MConfirmarCarrito
    {
        public int ctacli { get; set; }
        public int zona { get; set; }
        public int actividad { get; set; }
        public int vendedor { get; set; }
        public int categoria { get; set; }
        public string transporte { get; set; }
        public int listap { get; set; }
        public string comprobante { get; set; }
        public string nombreCliente { get; set; }
        public string domicilioCliente { get; set; }
        public int codpos1 { get; set; }
        public int codpos2 { get; set; }
        public string localidad { get; set; }
        public string telefono { get; set; }
        public int CodigoIva { get; set; }
        public string cuit1 { get; set; }
        public string cuit2 { get; set; }
        public string cuit3 { get; set; }
        public string ingBruto { get; set; }
        public double subtotal { get; set; }
        public double precioTotal { get; set; }
        public string condvta { get; set; }
        public int formaPago { get; set; }
        public double net27 { get; set; }
        public double neto21 { get; set; }
        public double neto105 { get; set; }
        public double neto2 { get; set; }
        public double neto5 { get; set; }
        public double iva27 { get; set; }
        public double iva21 { get; set; }
        public double iva105 { get; set; }
        public double iva5 { get; set; }
        public double iva2 { get; set; }
        public double exento { get; set; }
        public int terminal { get; set; }

    }
}
