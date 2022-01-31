using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.entities
{
   public  class DBOfactura
    {
        public string Guardarfactura(venfactura encfactura, List<vendetfactura> detallefactura) {

            try
            {
                using (var context = new shopingEntities())
                {
                    context.venfactura.Add(encfactura);
                    context.SaveChanges();
                }
                return "exitoso";
            }
            catch (Exception e)
            {

                return "Error";
            }
        }
    }
}
