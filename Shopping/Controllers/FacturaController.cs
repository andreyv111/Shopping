using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infraestructure;
using Infraestructure.entities;
using ApplicationCore.Services;

namespace Shopping.Controllers
{
    public class FacturaController : Controller
    {

        public static List<vendetfactura> detalleFactura;
        public static int secuenta = 1;
        // GET: Factura
        public ActionResult Index()
        {
            return View();
        }

        // GET: Factura/Create
        public ActionResult Create()
        {
            detalleFactura = new List<vendetfactura>();
            return View();
        }

        [HttpPost]
        public ActionResult buscarArticulo( string term)
        {

            DBOArticulo DBARTICULO = new DBOArticulo();

            List<inv_articulo> ARTICULOS = DBARTICULO.ListaArticulos();
            List<Shopping.Models.Articulo> NuevosArticulos = new List<Models.Articulo>();

            foreach (var item in ARTICULOS)
            {
                Shopping.Models.Articulo mi_articulo = new Models.Articulo();
                mi_articulo.cod_articulo = item.cod_articulo;
                mi_articulo.Nombre = item.Nombre;
                mi_articulo.PRECIO = item.PRECIO;
                mi_articulo.IMPUESTO = item.IMPUESTO;
                NuevosArticulos.Add(mi_articulo);
            }
            return PartialView("_partialviewArticulo", NuevosArticulos);
        }


        [HttpPost]
        public ActionResult SeleccionarArticulo(string Articulo)
        {

            DBOArticulo DBARTICULO = new DBOArticulo();
            List<inv_articulo> ARTICULOS = DBARTICULO.ListaArticulos();
            Shopping.Models.Articulo mi_articulo = new Models.Articulo();
            foreach (var item in ARTICULOS)
            {
                if (item.cod_articulo.Equals(Articulo))
                {

                    mi_articulo.cod_articulo = item.cod_articulo;
                    mi_articulo.Nombre = item.Nombre;
                    mi_articulo.PRECIO = item.PRECIO;
                    mi_articulo.IMPUESTO = item.IMPUESTO;
                    break;
                }

            }

            vendetfactura nuevoDetalle = new vendetfactura();
            nuevoDetalle.secuencia = secuenta;
            nuevoDetalle.Cod_articulo = mi_articulo.cod_articulo;
            nuevoDetalle.nombre = mi_articulo.Nombre;
            nuevoDetalle.Precio = (decimal)mi_articulo.PRECIO;
            nuevoDetalle.Descuento = 0;
            nuevoDetalle.cantidad = 1;
            double v = calculoImpuesto((Double)mi_articulo.PRECIO, mi_articulo.cod_articulo);
            nuevoDetalle.impuesto = (decimal)v;
            nuevoDetalle.totallinea = nuevoDetalle.impuesto + (nuevoDetalle.Precio * 1);
            detalleFactura.Add(nuevoDetalle);
            secuenta = secuenta + 1;
            return PartialView("_partialViewDetalle", detalleFactura);
        }
    

        public double calculoImpuesto(Double monto ,string pcod_articulo) {

            DBOArticulo DBARTICULO = new DBOArticulo();
            List<inv_articulo> ARTICULOS = DBARTICULO.ListaArticulos();
            Shopping.Models.Articulo mi_articulo = new Models.Articulo();
            foreach (var item in ARTICULOS)
            {
                if (item.cod_articulo.Equals(pcod_articulo))
                {
                    mi_articulo.IMPUESTO = item.IMPUESTO;
                    break;
                }
            }
            double montoimpuesto = 0;
            montoimpuesto = (double)(Convert.ToDouble(monto) * (mi_articulo.IMPUESTO / 100));
            return montoimpuesto;
        
        }



        [HttpPost]
        public ActionResult ActualizarCantidad(string pcod_articulo, int psecuencia, int pval)
        {

           List<vendetfactura> nuevo_detalle= new List<vendetfactura>();
            double porDesceunto = 0;
            Double subtotal = 0;
            double montoDescuento = 0;
            foreach (var item in detalleFactura)
            {
                if (item.Cod_articulo.Equals(pcod_articulo)&& item.secuencia==psecuencia)
                {
                    if (item.Cod_articulo.Equals("D205")) { 
                        item.cantidad = pval;
                        porDesceunto = porDescuento(item.Precio, (decimal)item.cantidad, pval * item.Precio);
                        item.Descuento = porDesceunto;
                        montoDescuento = pval * (Double)item.Precio * (porDesceunto / 100);
                        subtotal = (Convert.ToDouble(item.Precio) * pval) - montoDescuento;
                        double v = calculoImpuesto(subtotal, item.Cod_articulo);
                        item.impuesto = (decimal)v;
                        item.totallinea = item.impuesto + ((item.Precio * pval)- (Decimal)montoDescuento);
                        nuevo_detalle.Add(item);
                    }
                    else {
                        item.cantidad = pval;
                        subtotal = (Convert.ToDouble(item.Precio) * pval);
                        double v = calculoImpuesto(subtotal, item.Cod_articulo);
                        item.impuesto = (decimal)v;
                        item.totallinea = item.impuesto + (item.Precio * pval);
                        nuevo_detalle.Add(item);
                    }
                }
                else
                {
                    nuevo_detalle.Add(item);
                }
            }

            return PartialView("_partialViewDetalle", nuevo_detalle);

        }

        [HttpPost]
        public ActionResult ActualizarDescuento(string pcod_articulo, int psecuencia, int pval)
        {

            List<vendetfactura> nuevo_detalle = new List<vendetfactura>();
            double porDesceunto = 0;
            Double subtotal = 0;
            double montoDescuento = 0;
            foreach (var item in detalleFactura)
            {
                if (item.Cod_articulo.Equals(pcod_articulo) && item.secuencia == psecuencia)
                {
                        item.Descuento = pval;
                        montoDescuento = Convert.ToDouble(item.cantidad) * (Double)item.Precio * (item.Descuento / 100);
                        subtotal = (Convert.ToDouble(item.Precio) * Convert.ToDouble(item.cantidad)) - montoDescuento;
                        double v = calculoImpuesto(subtotal, item.Cod_articulo);
                        item.impuesto = (decimal)v;
                        item.totallinea =(decimal) item.impuesto + (decimal)subtotal;
                        nuevo_detalle.Add(item);
                }
                else
                {
                    nuevo_detalle.Add(item);
                }
            }

            return PartialView("_partialViewDetalle", nuevo_detalle);

        }

        public double porDescuento(decimal pprecio , decimal pcantidad, decimal psubtotal) {
            double pordescuento = 0;
            float precioAnterior = 0;
            float precionuevo = 0;
            precioAnterior = (float)psubtotal;
            precionuevo = (((float)(( pcantidad/2) * pprecio)));
            pordescuento = (precioAnterior - precionuevo)*100/precioAnterior;
            return pordescuento;
        }


        [HttpPost]
        public ActionResult GuardarFactura(string Pnombre)
        {

            venfactura encfactura = new venfactura();

            encfactura.fecha = DateTime.Now;
            encfactura.Nombre = Pnombre;
            encfactura.vendetfactura = detalleFactura;
            DBOfactura factura = new DBOfactura();
          String msn=  factura.Guardarfactura(encfactura, detalleFactura);
                

                if (msn.ToString().Equals("exitoso"))
                {
                    return Json(new { action = true, msn = "Se guardo correctamente la autorizacion" });
                }
                else
                {
              
                    return Json(new { action = false, msn = "Error al guardar la autorizacion" });
                }

         
         
        }






    }
}
