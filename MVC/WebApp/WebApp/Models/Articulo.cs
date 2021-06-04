using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class Articulo
    {
        public int idArticulo { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> iva { get; set; }
        public Nullable<bool> aplicaIva { get; set; }
        public Nullable<bool> activo { get; set; }
    }
}