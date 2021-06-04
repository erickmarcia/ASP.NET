using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using ConectData;
 

namespace MVCCUDApi.Controllers
{
    public class ArticuloController : ApiController
    {
        private POSEntitiesAzure dbContext = new POSEntitiesAzure();

        public IEnumerable<Articulo> Get()
        {
            using (POSEntitiesAzure dbEntities = new POSEntitiesAzure())
            {
                return dbEntities.Articulo.ToList();
            }
        }

        [HttpGet]
        public Articulo Get(int id)
        {
            using (POSEntitiesAzure articuloEntities = new POSEntitiesAzure())
            {
                return articuloEntities.Articulo.FirstOrDefault(e => e.idArticulo == id);
            }
        }

        

        [System.Web.Http.HttpPost]
        public IHttpActionResult AgregarArticulo([FromBody] Articulo articulo)
        {
            if (ModelState.IsValid)
            {   
                Decimal iva = 0;
                if (articulo.aplicaIva == true)
                {
                    iva = Convert.ToDecimal(articulo.precio) * Convert.ToDecimal(0.13);
                }

                // Realizas el mapeo de los datos
                var varArticulos = new Articulo
                {
                    idArticulo = articulo.idArticulo,
                    codigo = articulo.codigo,
                    nombre = articulo.nombre,
                    precio = articulo.precio,
                    iva = iva,
                    aplicaIva = articulo.aplicaIva,
                    activo = true,
                };

                dbContext.Articulo.Add(varArticulos);
                dbContext.SaveChanges();
                return Ok(varArticulos);
            }
            else
            {
                return BadRequest();
            }

        }


        [System.Web.Http.HttpPut]
        public IHttpActionResult ActualizarArticulo(int id, [FromBody] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                var articuloExiste = dbContext.Articulo.Count(c => c.idArticulo == id) > 0;


                if (articuloExiste)
                {

                    dbContext.Entry(articulo).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    return Ok();
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


        [System.Web.Http.HttpDelete]
        public IHttpActionResult EliminarArticulo(int id)
        {
                var articulo = dbContext.Articulo.Find(id);

                if (articulo != null)
                {

                    dbContext.Articulo.Remove(articulo);
                    dbContext.SaveChanges();

                    return Ok(articulo);
                }
                else
                {
                    return NotFound();
                }        
        }

        public class ElementJsonIntKey
        {
            public decimal Value { get; set; }
            public string Text { get; set; }
            public decimal AplicaIVA { get; set; }

        }

    }
}
