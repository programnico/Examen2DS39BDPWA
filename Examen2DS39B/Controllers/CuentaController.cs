using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examen2DS39B.Models;

namespace Examen2DS39B.Controllers
{
    public class CuentaController : Controller
    {
        examen2ds39Entities context = new examen2ds39Entities();
        // GET: Cuenta
        public ActionResult Index()
        {
            if (Session["codigo"] != null)
            {
                if (Session["rol"].Equals("administrador"))
                {
                    ViewBag.clientes = context.cliente.Select(x => new SelectListItem { Text = x.nombre_cliente, Value = x.cod_cliente.ToString() });
                    List<cuenta> cuentas = (from x in context.cuenta select x).ToList();

                    ViewBag.dataCuentas = cuentas;

                    if (TempData["msj"] != null)
                    {
                        ViewBag.msj = TempData["msj"];
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                 
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
                
            
        }

        [HttpPost]
        public ActionResult Index(cuenta c)
        {
            c.saldo = 0;
            /*
            if (Session["codigo"] != null)
            {*/
                if (ModelState.IsValid)
                {

                string accion = Request.Form["boton"].ToString();

                int existe = (from x in context.cuenta where x.cod_cliente == c.cod_cliente select x.ncta).Count();
                switch (accion)
                {
                    case "Guardar":
                        if (existe > 0)
                        {
                            TempData["msj"] = "Existe";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["msj"] = "Guardado";
                            context.cuenta.Add(c);
                            context.SaveChanges();
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                TempData["msj"] = "Datos";
                return RedirectToAction("Index");
            }
           // }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult cargarDetalleCliente(int idCliente)
        {
            List < cliente > dataCliente = context.cliente.Where(x=>x.cod_cliente == idCliente).ToList();

            List<strCliente> structs = new List<strCliente>();
            foreach (cliente item in dataCliente)
            {
                strCliente str = new strCliente();
                str.cod_cliente = item.cod_cliente;
                str.nombre_cliente = item.nombre_cliente;
                str.nit   = item.nit;
                str.rol = item.rol;
                structs.Add(str);
            }

            return Json( structs, JsonRequestBehavior.AllowGet);

        }

        struct strCliente
        {
            public int cod_cliente { get; set; }
            public string nombre_cliente { get; set; }
            public string nit { get; set; }
            public string rol { get; set; }

        }

    }
}