using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure;

namespace ApplicationCore.Services
{
    interface IServiceFactura
    {
         string Guardarfactura(venfactura encfactura, List<vendetfactura> detallefactura);
    }
}
