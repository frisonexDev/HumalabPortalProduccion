using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class ModificarRolRequest
    {
        [Display(Name = "ID")]
        public int RolID { get; set; }

        [Display(Name = "ROL")]
        public string Nombre { get; set; }

        public int EmpresaID { get; set; }
    }
}
