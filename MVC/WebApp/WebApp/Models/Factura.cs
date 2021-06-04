using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public partial class Factura
    {
        public Factura ()
        {
            ComprobanteDetalle = new List<Detalle>();
        }

        public int idFactura { get; set; }
        public string codigoFactura { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<bool> activo { get; set; }

        public int idDetalle { get; set; }
        public string codArticulo { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> cantidad { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> total { get; set; }

        public ICollection<Detalle> conceptos { get; set; }

        public virtual List<Detalle> ComprobanteDetalle { get; set; }

        //public Factura()
        //{
        //    this.Detalle = new HashSet<Detalle>();
        //}

        //public ICollection<Detalle> conceptos { get; set; }

        //public List<Detalle> conceptos { get; set; }

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
            
            public string codigoFactura { get; set; }

            public Nullable<int> idArticulo { get; set; }
        }

    }
}