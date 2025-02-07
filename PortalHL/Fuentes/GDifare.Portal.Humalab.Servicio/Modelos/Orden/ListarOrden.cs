using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden
{
    public class ListarOrden
    {
        public int NOrden { get; set; }
        public string CodigoBarra { get; set; }    
        public DateTime FechaIngreso { get; set; }
        public string NombrePaciente { get; set; }
        public float Precio { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string CodigoGalileo { get; set; } = string.Empty;        
        public string NombreCliente { get; set; } = string.Empty;
    }
}
