using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FacturaController : Controller
    {
        string BaseUrl = "https://localhost:44326/";

        private static readonly ICollection<Factura> facturas = new HashSet<Factura>();

        private static readonly ICollection<Detalle> facturaDetalle = new HashSet<Detalle>();

        private static readonly ICollection<Articulo> articulosx = new HashSet<Articulo>();

        //GET: Factura
        public async Task<ActionResult> Index()
        {
            List<Factura> facturaInfo = new List<Factura>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/factura/");

                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    facturaInfo = JsonConvert.DeserializeObject<List<Factura>>(empResponse);
                }
            }
            return View(facturaInfo);
        }

        public async Task<ActionResult> Create()
        {
            //GET: Articulo
            List<Articulo> articulos = new List<Articulo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/articulo/");

                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    articulos = JsonConvert.DeserializeObject<List<Articulo>>(empResponse);
                }
            }

            List<SelectListItem> ObjList = new List<SelectListItem>();

            foreach (var x in articulos)
            {
                ObjList.Add(new SelectListItem()
                {
                    Text = x.nombre.ToString(),
                    Value = x.idArticulo.ToString()
                });

            }

            //////Assigning generic list to ViewBag
            ViewBag.Productos = ObjList;
            //PopulateArticuloDropDownList();
            return View();
        }


        public ActionResult Details(int id)
        {
            Detalle detalle = new Detalle();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var responseTask = client.GetAsync("api/detalle/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Detalle>();
                    readTask.Wait();
                    detalle = readTask.Result;
                }

            }

            return View(detalle);
        }


        [HttpPost]
        public ActionResult Create(Factura modelo)
        {
            if (modelo == null)
            {
                modelo = new Factura();
            }

            if (CrearFactura(modelo))
                {
                    CrearFacturaDetalle(modelo);
                    return RedirectToAction("Index");
                }
          
            return View(modelo);
        }

        private bool CrearFactura(Factura factura)
        {
            if (ModelState.IsValid)
            {
                if (factura.conceptos != null && factura.conceptos.Count > 0)
                {
                    // este id posiblemente lo asigne tu base de datos.
                    factura.idFactura = facturas.Count > 0 ? facturas.Max(x => x.idFactura) + 1 : 1;
                    factura.fecha = DateTime.Now;
                    factura.activo = true;

                    foreach (var x in factura.conceptos)
                    {
                        factura.codigoFactura = x.codigoFactura;// facturas.Count > 0 ? facturas.Max(x => x.codigoFactura) + 1 : "0001";
                        //detalle.nombre = articulosx.FirstOrDefault(e => e.codigo == x.codArticulo).nombre;
                    }


                    facturas.Add(factura);

                    CreateFactura(factura);
                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "No puede guardar facturas sin detalle");
                }
            }

            return false;
        }

        private static void EliminarDetallePorIndice(Factura factura, string operacion)
        {
            // se asume que en el parametro 'operacion' viene el index del detalle a eliminar.
            string indexStr = operacion.Replace("eliminar-detalle-", "");
            int index = 0;

            if (int.TryParse(indexStr, out index) && index >= 0 && index < factura.conceptos.Count)
            {
                var item = factura.conceptos.ToArray()[index];
                factura.conceptos.Remove(item);
            }
        }

        //[HttpPost]
        public ActionResult CreateFactura(Factura factura)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl + "api/factura");
                var postTask = client.PostAsJsonAsync<Factura>("Factura", factura);

                postTask.Wait();
                var result = postTask.Result;
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador.");
            return View(factura);
        }

        private bool CrearFacturaDetalle(Factura factura)
        {
            //if (ModelState.IsValid)
            //{

            if (factura.conceptos != null && factura.conceptos.Count > 0)
                {
                    Detalle detalle = new Detalle();

                    foreach (var x in factura.conceptos)
                    {
                        detalle.idFactura = x.idFactura;
                        detalle.codArticulo = x.codArticulo;
                        detalle.cantidad = x.cantidad;
                        detalle.precio = x.precio;
                        detalle.IVA = x.IVA;
                        detalle.total = Convert.ToDecimal(x.cantidad) * (Convert.ToDecimal(x.precio) + Convert.ToDecimal(x.IVA));  //x.total;
                        facturaDetalle.Add(detalle);

                        CreateFacturaDetalle(detalle);
                    }

                    return true;
                }
                else
                {
                    ModelState.AddModelError("", "No puede guardar facturas sin detalle");
                }
         

            return false;
        }
         
        public ActionResult CreateFacturaDetalle(Detalle detallle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl + "api/detalle");
                var postTask = client.PostAsJsonAsync<Detalle>("Detalle", detallle);

                postTask.Wait();
                var result = postTask.Result;
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador.");
            return View(detallle);
        }


        public ActionResult Edit(int id)
        {
            Factura factura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var responseTask = client.GetAsync("api/factura/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Factura>();
                    readTask.Wait();
                    factura = readTask.Result;
                }

            }

            return View(factura);
        }

        [HttpPost]
        public ActionResult Edit(Factura factura)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var putTask = client.PutAsJsonAsync($"api/factura/{factura.idFactura}", factura);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(factura);
        }

        public ActionResult Delete(int id)
        {
            Factura factura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var responseTask = client.GetAsync("api/factura/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Factura>();
                    readTask.Wait();
                    factura = readTask.Result;
                }

            }

            return View(factura);
        }

        [HttpDelete]
        public ActionResult Delete(Factura factura, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var deleteTask = client.DeleteAsync($"api/factura/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(factura);
        }


    }
}