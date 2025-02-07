using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden
{
    public class LogObservaciones
    {
        public string Orden { get; set; }=string.Empty;
        public string ObservacionOrden { get; set; } = string.Empty;
        public string Muestra { get; set; } = string.Empty;
        public string ObservacionMuestra { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}
