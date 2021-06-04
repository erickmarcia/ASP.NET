using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class Detalle
    {
        public int idDetalle { get; set; }
        public string codArticulo { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> cantidad { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> idFactura { get; set; }
        public Nullable<int> idArticulo { get; set; }
    }
}