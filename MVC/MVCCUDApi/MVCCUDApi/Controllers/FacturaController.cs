using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
//using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using ConectData;

namespace MVCCUDApi.Controllers
{
    public class FacturaController : ApiController
    {
        private POSEntitiesAzure dbContext = new POSEntitiesAzure();

        [HttpGet]
        public /*IEnumerable<Factura>*/object Get()
        {
            using (POSEntitiesAzure dbEntities = new POSEntitiesAzure())
            {
                var select = (
                from f in dbEntities.Factura
                join d in dbEntities.Detalle on f.idFactura equals d.idFactura
                join a in dbEntities.Articulo on d.idArticulo equals a.idArticulo
                where f.activo == true
                select new
                {
                    f.idFactura,
                    f.codigoFactura,
                    f.fecha,
                    d.idArticulo,
                    a.codigo,
                    a.nombre,
                    d.cantidad,
                    d.precio,
                    d.IVA,
                    d.total,
                    f.activo
                }
                ).ToList();

                //return select;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(select, Newtonsoft.Json.Formatting.Indented);
                return select;
            }
        }

        [HttpGet]
        public /*IEnumerable<Factura>*/object Get(int id)
        {
            using (POSEntitiesAzure dbEntities = new POSEntitiesAzure())
            {
                var select = (
                from f in dbEntities.Factura
                join d in dbEntities.Detalle on f.idFactura equals d.idFactura
                join a in dbEntities.Articulo on d.idArticulo equals a.idArticulo
                where (f.activo == true) && (f.idFactura == id)
                select new
                {
                    f.idFactura,
                    f.codigoFactura,
                    f.fecha,
                    d.idArticulo,
                    a.codigo,
                    a.nombre,
                    d.cantidad,
                    d.precio,
                    d.IVA,
                    d.total,
                    f.activo
                }
                ).ToList();

                //return select;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(select, Newtonsoft.Json.Formatting.Indented);
                return select;
            }
        }

        //[HttpGet]
        //public Factura GetDetalle(int id)
        //{
        //    using (POSEntitiesAzure facturaEntities = new POSEntitiesAzure())
        //    {
        //        return facturaEntities.Factura.FirstOrDefault(e => e.idFactura == id);
        //    }
        //}

        [HttpGet]
        public Factura GetMax(string codFactura)
        {
            using (POSEntitiesAzure facturaEntities = new POSEntitiesAzure())
            {
                int maxfactura = facturaEntities.Factura.Max(x => x.idFactura);
                return facturaEntities.Factura.FirstOrDefault(e => e.idFactura == maxfactura);
            }
        }

        //[HttpPost]
        //public IHttpActionResult AgregarFactura([FromBody] Factura factura)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dbContext.Factura.Add(factura);
        //        dbContext.SaveChanges();
        //        return Ok(factura);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //}



        [HttpPost]
        public IHttpActionResult AgregarFactura([FromBody] Factura factura)
        {
            if (ModelState.IsValid)
            {
                // Realizas el mapeo de los datos
                var varFactura = new Factura
                {
                    idFactura = factura.idFactura,
                    codigoFactura = factura.codigoFactura,
                    fecha = factura.fecha,
                    activo = true,
                };

                dbContext.Factura.Add(varFactura);
                dbContext.SaveChanges();
                return Ok(varFactura);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPut]
        public IHttpActionResult ActualizaFactura(int id, [FromBody] Factura factura)
        {
            if (ModelState.IsValid)
            {
                var facturaExiste = dbContext.Factura.Count(e => e.idFactura == id) > 0;


                if (facturaExiste)
                {

                    dbContext.Factura.Add(factura);
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


        [HttpDelete]
        public IHttpActionResult EliminarFactura(int id)
        {
                var factura = dbContext.Factura.Find(id);

                if (factura != null)
                {

                    dbContext.Factura.Remove(factura);
                    dbContext.SaveChanges();

                    return Ok(factura);
                }
                else
                {
                    return NotFound();
                }        
        }

    }
}
