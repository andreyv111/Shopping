using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Infraestructure.entities
{
    public class DBOSeg_usuario
    {

        public Seg_usuario ObtenerListaUsuarios(string pcodusuario, string password) {


            Seg_usuario misuarui = new Seg_usuario();

            using (shopingEntities db = new shopingEntities())
            {

                var resultado= (from x in db.Seg_usuario
                                 where x.Cod_usuario.Equals(pcodusuario) & x.password.Equals(password)
                                 select x).FirstOrDefault();
                misuarui = (Seg_usuario)resultado;

                if (resultado != null)
                {
                    misuarui = (Seg_usuario)resultado;
                }
                else
                {
                    misuarui = null;
                }
            }


            return misuarui;
        }


          
    }
}
