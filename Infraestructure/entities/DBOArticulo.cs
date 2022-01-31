using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.entities
{
 public   class DBOArticulo
    {
        public List<inv_articulo> ListaArticulos() {
            List<inv_articulo> listaDeArticulos = new List<inv_articulo>();
            try
            { 
                using (var context = new shopingEntities())
                {
                    listaDeArticulos = context.inv_articulo.ToList();
                }
                return listaDeArticulos;

            }
            catch (Exception e)
            {
                string msn = e.Message;
                return null;
            }  
        }
    }
}
