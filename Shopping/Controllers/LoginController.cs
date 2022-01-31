using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infraestructure.entities;
using ApplicationCore.Services;
using Infraestructure;

namespace Shopping.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(String pcodUsuaruo,string ppassword)
        {
            return View();
        }

        // GET: Login
        public ActionResult Create(String pcodUsuaruo, string ppassword)
        {
     
            IServiceSeg_usuario buscar_usuario = new ServiceSeg_usuario();
            Seg_usuario miusuario = buscar_usuario.BuscarUsuario(pcodUsuaruo, ppassword);
            try
            {
                if (miusuario != null)
                {
                    Session["user"] = miusuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    return Json(new { action = false, msn = "La cedula o contraseña son incorrectas" });

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Default", "Error");
            }
        }

    }
}
