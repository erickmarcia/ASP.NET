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
    public class DeatalleController : Controller
    {
        string BaseUrl = "https://localhost:44326/";

        // GET: Articulo
        public async Task<ActionResult> Index()
        {
            List<Detalle> facturaInfo = new List<Detalle>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/detalle/");

                if(res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    facturaInfo = JsonConvert.DeserializeObject<List<Detalle>>(empResponse);
                }
            }
                return View(facturaInfo);
        }

        public async Task<ActionResult> Details(int id)
        {
            List<Detalle> facturaInfo = new List<Detalle>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/detalle/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var empResponse = res.Content.ReadAsStringAsync().Result;
                    facturaInfo = JsonConvert.DeserializeObject<List<Detalle>>(empResponse);
                }
            }
            return View(facturaInfo);

        }



        [HttpPost]
        public ActionResult Create(Detalle detalle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl + "api/detalle");
                var postTask = client.PostAsJsonAsync<Detalle>("Detalle", detalle);

                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Error, contacta al administrador.");
            return View(detalle);
        }

        public ActionResult Edit(int id)
        {
            Detalle detalle = null;

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
        public ActionResult Edit(Detalle detalle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var putTask = client.PutAsJsonAsync($"api/detalle/{detalle.idDetalle}", detalle);
                putTask.Wait();
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(detalle);
        }


        public ActionResult Delete(int id)
        {
            Detalle detalle = null;

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

        [HttpDelete]
        public ActionResult Delete(Detalle detalle, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                var deleteTask = client.DeleteAsync($"api/detalle/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(detalle);
        }



    }
}