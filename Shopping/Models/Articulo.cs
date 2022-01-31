using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class Articulo
    {

        public string cod_articulo { get; set; }
        public string Nombre { get; set; }
        public Nullable<decimal> PRECIO { get; set; }
        public Nullable<double> IMPUESTO { get; set; }
        public int ID { get; set; }

    }
}