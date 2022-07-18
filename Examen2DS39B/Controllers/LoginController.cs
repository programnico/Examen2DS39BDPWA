using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examen2DS39B.Models;

namespace Examen2DS39B.Controllers
{
    public class LoginController : Controller
    {

        examen2ds39Entities context = new examen2ds39Entities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string txtCodigo, string txtNit)
        {
            cliente cliente = new cliente();
            int usuario = int.Parse(txtCodigo);
            string contra = txtNit;
            string rol = (from x in context.cliente where x.cod_cliente == usuario && x.nit == txtNit select x.rol).FirstOrDefault().ToString();
            string nombre = (from x in context.cliente where x.cod_cliente == usuario && x.nit == txtNit select x.nombre_cliente).FirstOrDefault().ToString();

            if (!rol.Equals(""))
            {
                System.Web.HttpContext.Current.Session["rol"] = rol;
                System.Web.HttpContext.Current.Session["codigo"] = txtCodigo;
                System.Web.HttpContext.Current.Session["nit"] = txtNit;
                System.Web.HttpContext.Current.Session["nombre"] = nombre;
                return RedirectToAction("Index","Transacciones");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Salir()
        {
            Session["rol"] = null;
            Session["codigo"] = null;
            Session["nit"] = null;
            Session["nombre"] = null;
            return RedirectToAction("Index","Login");
        }

    }
}