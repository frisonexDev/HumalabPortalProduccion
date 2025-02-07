using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class Perfiles
    {
        [Display(Name = "ID")]
        public int PerfilID { get; set; }

        [Display(Name = "PERFIL")]
        public string Perfil { get; set; }

        public int EstadoID { get; set; }

        public IList<PerfilOpcion> PerfilOpcion { get; set; }

    }
}