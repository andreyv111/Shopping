using Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.entities;

namespace ApplicationCore.Services
{
    public class ServiceFactura : IServiceFactura
    {
        public string Guardarfactura(venfactura pencfactura, List<vendetfactura> pdetallefactura)
        {
            DBOfactura mifactura = new DBOfactura();
            return mifactura.Guardarfactura(pencfactura, pdetallefactura);
        }
    }
}
