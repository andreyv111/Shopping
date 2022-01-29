using Infraestructure;
using Infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceSeg_usuario : IServiceSeg_usuario
    {
        public Seg_usuario BuscarUsuario(string pcod_usuario, string ppassword)
        {
            DBOSeg_usuario miusuario = new DBOSeg_usuario();
            return miusuario.ObtenerListaUsuarios(pcod_usuario, ppassword);
        }
    }
}
