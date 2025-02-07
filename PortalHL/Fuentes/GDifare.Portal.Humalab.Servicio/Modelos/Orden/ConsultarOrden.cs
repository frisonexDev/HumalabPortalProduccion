using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden
{
    public class ConsultarOrden
    {
        public int OpcionBusqueda { get; set; }

        public string DatoBusqueda { get; set; } = string.Empty;

        public int IdOrden { get; set; }

        public string CodigoBarra { get; set; } = string.Empty;

        public int IdUsuarioGalileo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string opcionEstado { get; set; } = string.Empty;

    }
}
