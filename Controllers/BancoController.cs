using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
//
using AplicacionWeb.Models;

namespace AplicacionWeb.Controllers
{
    public class BancoController : Controller
    {
        // GET: Banco
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerBancos(string filtro)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string sUri = string.Format("/api/Banco/ObtenerBancos?filtro={0}", filtro);
            var response = client.GetAsync(sUri).Result;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<mBanco> lst = ser.Deserialize<List<mBanco>>(response.Content.ReadAsStringAsync().Result);
            return Json(new { lista = lst }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MantBanco(string sAccion, mBanco modelo) 
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55947");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            string sUri = string.Format("/api/Banco/MantBanco?sAccion={0}", sAccion);
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