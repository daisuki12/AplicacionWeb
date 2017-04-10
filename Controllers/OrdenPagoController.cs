using AplicacionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AplicacionWeb.Controllers
{
    public class OrdenPagoController : Controller
    {
        // GET: OrdenPago
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerOrdenes(string filtro)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/OrdenPago/ObtenerOrdenes?filtro={0}", filtro);
            var response = client.GetAsync(sUri).Result;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<mOrdenPago> lst = ser.Deserialize<List<mOrdenPago>>(response.Content.ReadAsStringAsync().Result);
            return Json(new { lista = lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MantOrden(string sAccion, mOrdenPago modelo)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/OrdenPago/MantOrden?sAccion={0}", sAccion);
            var response = client.PostAsJsonAsync(sUri, modelo).Result;
            mResultado obj = null;

            if (response.IsSuccessStatusCode)
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                obj = ser.Deserialize<mResultado>(response.Content.ReadAsStringAsync().Result);
            }

            return Json(new { resultado = obj.resultado, mensaje = obj.mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}