using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.WCFServicio;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using AplicacionWeb.Models;

namespace AplicacionWeb.Controllers
{
    public class ConsultaController : Controller
    {
        // GET: Consulta
        public ActionResult Consulta1()
        {
            return View();
        }
        //
        public ActionResult Consulta2()
        {
            return View();
        }
        //
        public ActionResult Casos()
        {
            return View();
        }
        //
        public JsonResult ConsultaJson(int idBanco, int idSucursal, string moneda)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/Consulta/ObtenerOrdenes?idbanco={0}&idSucursal={1}&moneda={2}", idBanco, idSucursal, moneda);
            var response = client.GetAsync(sUri).Result;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<mOrdenPago> lst = ser.Deserialize<List<mOrdenPago>>(response.Content.ReadAsStringAsync().Result);
            return Json(new { lista = lst }, JsonRequestBehavior.AllowGet);
        }
        //
        public JsonResult ConsultaWfc(int idBanco) 
        {
            Service1Client obj = new Service1Client();
            var lista = obj.ObtenerSucursalesBanco(idBanco);
            return Json(new { lst = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}