using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.CatalogoPruebas
{
    public class CatalogoDetalle
    {

        public int IdCatalogoMaestro { get; set; }
        public int IdCatalogoDetalle { get; set; }
        public int Relacion { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Orden { get; set; }
        public bool Editable { get; set; }
        public bool Eliminar { get; set; }
    }
}
