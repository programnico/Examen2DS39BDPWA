using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Examen2DS39B.Models;

namespace Examen2DS39B.Controllers
{
    public class TransaccionesController : Controller
    {
        examen2ds39Entities context = new examen2ds39Entities();

        // GET: Transacciones
        public ActionResult Index()
        {
            if (Session["codigo"] != null && !Session["codigo"].Equals(""))
            {
                int codigoC = -1;
                int codigoCuenta = -1;
                codigoC = int.Parse(Session["codigo"].ToString());
                //cargar detalle cuenta
                List<cuenta> dataDetalle = context.cuenta.Where(x => x.cod_cliente == codigoC).ToList();
                ViewBag.dataDetalle = dataDetalle;

                codigoCuenta = int.Parse((from x in context.cuenta where x.cod_cliente == codigoC select x.ncta).FirstOrDefault().ToString());

                //cargar transacciones
                List<transacciones> dataTransacciones = (from x in context.transacciones where x.ncta == codigoCuenta select x).ToList();

                ViewBag.dataTransacciones = dataTransacciones;

                IList<SelectListItem> opciones = new List<SelectListItem>{
                new SelectListItem{Text = "deposito", Value = "deposito"},
                new SelectListItem{Text = "retiro", Value = "retiro"}
                };

                ViewBag.opciones = opciones;

                if (TempData["msj"] != null)
                {
                    if (TempData["msj"].Equals("Numero transacciones por dia completadas"))
                    {
                        ViewBag.msj = "numero transacciones";
                        return View();
                    }
                    else if (TempData["msj"].Equals("Procesado correctamente"))
                    {
                        ViewBag.msj = "Procesado";
                        return View();
                    }else if(TempData["msj"].Equals("monto no disponible"))
                    {
                        ViewBag.msj = "monto no disponible";
                    }else if(TempData["msj"].Equals("No se puede retirar"))
                    {
                        ViewBag.msj = "No se puede retirar";
                    }
                    else if (TempData["msj"].Equals("Datos"))
                    {
                        ViewBag.msj = "Datos";
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            //return View();

        }

        [HttpPost]
        public ActionResult Transacciones(transacciones t)
        {
            if (Session["codigo"] != null)
            {
                int numeroTransa = (from x in context.transacciones where x.fecha == t.fecha select x.fecha).Count();

                int lastInsertCodigo = int.Parse((from x in context.transacciones orderby x.cod_transac descending select x.cod_transac).FirstOrDefault().ToString());
                t.cod_transac = lastInsertCodigo + 1;

                if (ModelState.IsValid)
                {
                    string accion = Request.Form["boton"].ToString();

                    switch (accion)
                    {
                        case "Procesar":
                            if (numeroTransa >= 10)
                            {
                                TempData["msj"] = "Numero transacciones por dia completadas";
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                if (t.tipo.Equals("deposito"))
                                {
                                    context.transacciones.Add(t);
                                    context.SaveChanges();

                                    cuenta cTemp = context.cuenta.FirstOrDefault(x => x.ncta == t.ncta);
                                    //cuenta c = context.cuenta.FirstOrDefault(x => x.cod_cliente == int.Parse(Session["codigo"].ToString()));
                                    double sumMonto = 0;
                                    double sumRetiro = 0;
                                    //var codigoCuenta = int.Parse((from x in context.cuenta where x.cod_cliente == int.Parse(Session["codigo"].ToString()) select x.ncta).FirstOrDefault().ToString());
                                    int numeroDepositos = (from x in context.transacciones where x.ncta == t.ncta && x.tipo == "deposito" select x.monto).Count();
                                    int numeroRetiros = (from x in context.transacciones where x.ncta == t.ncta && x.tipo == "retiro" select x.monto).Count();
                                    if (numeroDepositos > 0)
                                    {
                                        sumMonto = double.Parse((from x in context.transacciones where x.ncta == t.ncta && x.tipo == "deposito" select x.monto).Sum().ToString());
                                    }
                                    if (numeroRetiros > 0)
                                    {
                                        sumRetiro = double.Parse((from x in context.transacciones where x.ncta == t.ncta && x.tipo == "retiro" select x.monto).Sum().ToString());
                                    }
                                    double totalDisponible = 0;
                                    if (sumMonto >0 && sumRetiro>0)
                                    {
                                         totalDisponible = sumMonto - sumRetiro;
                                    }
                                    else if(sumMonto>0)
                                    {
                                        totalDisponible = sumMonto;
                                    }
                                    
                                    /*else if (sumRetiro > 0)
                                    {
                                        totalDisponible = 0-sumRetiro;
                                    }*/

                                    cTemp.saldo = totalDisponible;
                                    context.SaveChanges();
                                    
                                    TempData["msj"] = "Procesado correctamente";
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    //retiro
                                    //context.transacciones.Add(t);
                                    //context.SaveChanges();


                                    cuenta cTemp = context.cuenta.FirstOrDefault(x => x.ncta == t.ncta);
                                    //cuenta c = context.cuenta.FirstOrDefault(x => x.cod_cliente == int.Parse(Session["codigo"].ToString()));
                                    double sumMonto = 0;
                                    double sumRetiro = 0;
                                    //var codigoCuenta = int.Parse((from x in context.cuenta where x.cod_cliente == int.Parse(Session["codigo"].ToString()) select x.ncta).FirstOrDefault().ToString());
                                    int numeroDepositos = (from x in context.transacciones where x.ncta == t.ncta && x.tipo == "deposito" select x.monto).Count();
                                    int numeroRetiros = (from x in context.transacciones where x.ncta == t.ncta && x.tipo == "retiro" select x.monto).Count();
                                    if (numeroDepositos > 0)
                                    {
                                        sumMonto = double.Parse((from x in context.transacciones where x.ncta == t.ncta && x.tipo == "deposito" select x.monto).Sum().ToString());
                                    }
                                    if (numeroRetiros > 0)
                                    {
                                        sumRetiro = double.Parse((from x in context.transacciones where x.ncta == t.ncta && x.tipo == "retiro" select x.monto).Sum().ToString());
                                    }

                                    double totalDisponible = 0;
                                    if (sumMonto > 0 && sumRetiro > 0)
                                    {
                                        totalDisponible = sumMonto - sumRetiro;
                                    }
                                    else if (sumMonto > 0)
                                    {
                                        totalDisponible = sumMonto;
                                    }

                                    if (totalDisponible > 0)
                                    {
                                        if (totalDisponible >= t.monto)
                                        {
                                            context.transacciones.Add(t);
                                            context.SaveChanges();

                                            double totalRetiro = totalDisponible - double.Parse(t.monto.ToString());

                                            cTemp.saldo = totalRetiro;
                                            context.SaveChanges();

                                            TempData["msj"] = "Procesado correctamente";
                                            return RedirectToAction("Index");
                                        }
                                        else
                                        {
                                            TempData["msj"] = "monto no disponible";
                                            return RedirectToAction("Index");
                                        }
                                    }
                                    else if(totalDisponible==0)
                                    {
                                        TempData["msj"] = "No se puede retirar";
                                        return RedirectToAction("Index");
                                    }



                                    return RedirectToAction("Index");
                                }
                            }
                            break;
                    }

                }
                else
                {

                    TempData["msj"] = "Datos";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}