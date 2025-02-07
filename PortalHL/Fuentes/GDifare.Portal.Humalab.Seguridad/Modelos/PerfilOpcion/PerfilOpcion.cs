using GDifare.Portal.Humalab.Segurida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class PerfilOpcion
    {
        public int PerfilOpcionID { get; set; }

        public int PerfilID { get; set; }

        public int OpcionID { get; set; }

        public int EstadoID { get; set; }

        public Perfiles Perfil { get; set; }

       // public Opcion Opcion { get; set; }

        public IList<OpcionPerfilAccion> OpcionPerfilAccion { get; set; }

      //  public string Accions { get { return string.Join(",", OpcionPerfilAccion.Where(s => s.EstadoID == 1 && s.Accion != null).Select(x => x.Accion.Codigo)) ; } }
    }
}
