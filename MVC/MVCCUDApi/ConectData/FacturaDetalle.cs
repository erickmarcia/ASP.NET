//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConectData
{
    using System;
    using System.Collections.Generic;
    
    public partial class FacturaDetalle
    {
        public int idFacturaDetalle { get; set; }
        public string codArticulo { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> idFactura { get; set; }
        public Nullable<int> idArticulo { get; set; }
        public Nullable<decimal> cantidad { get; set; }
    }
}
