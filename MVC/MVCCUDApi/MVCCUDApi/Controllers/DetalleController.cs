using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConectData;

namespace MVCCUDApi.Controllers
{
    public class DetalleController : ApiController
    {
        private POSEntitiesAzure dbContext = new POSEntitiesAzure();

        public IEnumerable<Detalle> Get()
        {
            using(POSEntitiesAzure dbEntities  = new POSEntitiesAzure())
            {
                return dbEntities.Detalle.ToList();
            }
        }

        [HttpGet]
        public Detalle Get(int id)
        {
            using (POSEntitiesAzure dbEntities = new POSEntitiesAzure())
            {
                return dbEntities.Detalle.FirstOrDefault(e => e.idDetalle == id);
            }
        }


        [HttpGet]
        public Articulo GetidArticulo(string codArticulo)
        {
            using (POSEntitiesAzure dbEntities = new POSEntitiesAzure())
            {
                return dbEntities.Articulo.FirstOrDefault(e => e.codigo == codArticulo);
            }
        }

        //[HttpPost]
        //public IHttpActionResult AgregarDetalleFactura([FromBody] Detalle factura)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        dbContext.Detalle.Add(factura);
        //        dbContext.SaveChanges();
        //        return Ok(factura);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //}

        [HttpPost]
        public IHttpActionResult AgregarDetalle([FromBody] Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                int maxfactura = dbContext.Factura.Max(x => x.idFactura);
                int idArticulo = dbContext.Articulo.FirstOrDefault(e => e.codigo == detalle.codArticulo).idArticulo;

                // Realizas el mapeo de los datos
                var varDetalle = new Detalle
                {
                    idDetalle = detalle.idDetalle,
                    idArticulo = idArticulo,
                    codArticulo = detalle.codArticulo,
                    nombre = detalle.nombre,
                    precio = detalle.precio,
                    IVA = detalle.IVA,
                    cantidad = detalle.cantidad,
                    idFactura = maxfactura,// detalle.idFactura,
                    total = detalle.total,
                };

                dbContext.Detalle.Add(varDetalle);
                dbContext.SaveChanges();
                return Ok(varDetalle);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPut]
        public IHttpActionResult ActualizaDetalle(int id, [FromBody] Detalle factura)
        {
            if (ModelState.IsValid)
            {
                var facturaExiste = dbContext.Detalle.Count(e => e.idFactura == id) > 0;

                if (facturaExiste)
                {

                    dbContext.Detalle.Add(factura);
                    dbContext.SaveChanges();
                    return Ok(factura);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
