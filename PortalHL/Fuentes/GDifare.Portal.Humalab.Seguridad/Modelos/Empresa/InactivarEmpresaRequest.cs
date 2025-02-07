using System;
using System.Collections.Generic;
using System.Text;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
   public class InactivarEmpresaRequest
    {
        public int EmpresaID { get; set; }
        public string Empresa { get; set; }
        public string Ruc { get; set; }
        public int Estado { get; set; }
    }
}


