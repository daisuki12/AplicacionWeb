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
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerSucursales(string filtro)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/Sucursal/ObtenerSucursales?filtro={0}&idBanco={1}&tipoFiltro={2}", filtro, 0, true);
            var response = client.GetAsync(sUri).Result;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<mSucursal> lst = ser.Deserialize<List<mSucursal>>(response.Content.ReadAsStringAsync().Result);
            return Json(new { lista = lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerSucursalesBanco(int idBanco)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/Sucursal/ObtenerSucursales?filtro={0}&idBanco={1}&tipoFiltro={2}", "", idBanco, false);
            var response = client.GetAsync(sUri).Result;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<mSucursal> lst = ser.Deserialize<List<mSucursal>>(response.Content.ReadAsStringAsync().Result);
            return Json(new { lista = lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MantSucursal(string sAccion, mSucursal modelo)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/Sucursal/MantSucursal?sAccion={0}", sAccion);
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