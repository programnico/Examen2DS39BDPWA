using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examen2DS39B.Models;

namespace Examen2DS39B.Controllers
{
    public class ClienteController : Controller
    {
        examen2ds39Entities context = new examen2ds39Entities();
        // GET: Cliente
        public ActionResult Index()
        {
            if (Session["rol"]!=null)
            {
                if (Session["rol"].Equals("administrador"))
                {
                    //cargar transacciones
                    List<cliente> data = context.cliente.ToList();

                    ViewBag.data = data;

                    IList<SelectListItem> roles = new List<SelectListItem>{
                new SelectListItem{Text = "administrador", Value = "administrador"},
                new SelectListItem{Text = "cliente", Value = "cliente"}
                };

                    ViewBag.roles = roles;

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
        public ActionResult Index(cliente c)
        {
            if (ModelState.IsValid)
            {
                string accion = Request.Form["boton"].ToString();

                switch (accion)
                {
                    case "Guardar":

                        int existe = (from x in context.cliente where x.cod_cliente == c.cod_cliente && x.nit == c.nit select x.cod_cliente).Count();
                        int codCliente = (from x in context.cliente where x.cod_cliente == c.cod_cliente select x.cod_cliente).Count();
                        int nitCliente = (from x in context.cliente where x.nit == c.nit select x.nit).Count();

                        if (existe > 0)
                        {
                            TempData["msj"] = "existe";
                            return RedirectToAction("Index");
                        }
                        else if (codCliente > 0)
                        {
                            TempData["msj"] = "existe_codigo";
                            return RedirectToAction("Index");
                        }
                        if (nitCliente > 0)
                        {
                            TempData["msj"] = "existe_nit";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["msj"] = "Guardado";
                            context.cliente.Add(c);
                            context.SaveChanges();
                        }

                        break;

                    case "Eliminar":

                        context.cliente.Remove(context.cliente.FirstOrDefault(x => x.cod_cliente == c.cod_cliente));
                        context.SaveChanges();
                        TempData["msj"] = "Eliminado";

                        break;

                    case "Modificar":

                        //sacar el registro del context, el que se va a modificar
                        cliente temp = context.cliente.FirstOrDefault(x => x.cod_cliente == c.cod_cliente);
                        //aplicando la modificacion campo por campo
                        temp.nombre_cliente = c.nombre_cliente;
                        temp.nombre_cliente = c.nombre_cliente;
                        temp.nit = c.nit;
                        temp.rol = c.rol;
                        //guardar cambios
                        context.SaveChanges();
                        TempData["msj"] = "Modificado";

                        break;
                }

                return RedirectToAction("Index");
            }
            else
            {
                TempData["msj"] = "Datos";
                return RedirectToAction("Index");
            }
        }

    }
}