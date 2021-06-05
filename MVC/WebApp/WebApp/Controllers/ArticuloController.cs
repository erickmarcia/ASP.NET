//using MVCCUDApi.Models;
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
    public class ArticuloController : Controller
    {
        //string BaseUrl = "https://localhost:44326/";
        string BaseUrl = "http://localhost/apidemo/";

        // GET: Articulo
        public async Task<ActionResult> Index()
        {

            List<Articulo> articuloInfo = new List<Articulo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/articulo/");

                if(res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    articuloInfo = JsonConvert.DeserializeObject<List<Articulo>>(empResponse);
                }
            }
                return View(articuloInfo);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Articulo articulo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl + "api/articulo");
                var postTask = client.PostAsJsonAsync<Articulo>("Articulo", articulo);

                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Error, contacta al administrador.");
            return View(articulo);
        }


        /// <summary>
        /// Edita la información de los Artículo, recibe como paramentro el id del Artículo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            Articulo articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var responseTask = client.GetAsync("api/articulo/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Articulo>();
                    readTask.Wait();
                    articulo = readTask.Result;
                }

            }

            return View(articulo);
        }

        [HttpPost]
        public ActionResult Edit(Articulo articulo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var putTask = client.PutAsJsonAsync($"api/articulo/{articulo.idArticulo}", articulo);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(articulo);
        }


        public ActionResult Delete(int id)
        {
            Articulo articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var responseTask = client.GetAsync("api/articulo/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Articulo>();
                    readTask.Wait();
                    articulo = readTask.Result;
                }

            }

            return View(articulo);
        }

        [HttpDelete]
        public ActionResult Delete(Articulo articulo, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var deleteTask = client.DeleteAsync($"api/articulo/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(articulo);
        }


    }
}