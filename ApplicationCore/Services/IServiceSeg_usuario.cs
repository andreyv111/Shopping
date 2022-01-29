using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure;

namespace ApplicationCore.Services
{
    public interface IServiceSeg_usuario
    {
         Seg_usuario BuscarUsuario(string pcod_usuario, String ppassword);
    }
}
