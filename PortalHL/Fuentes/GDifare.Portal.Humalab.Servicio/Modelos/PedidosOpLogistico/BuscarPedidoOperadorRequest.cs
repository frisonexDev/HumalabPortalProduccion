using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class BuscarPedidoOperadorRequest
    {

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

        public string? Cliente { get; set; }

        public bool? BuscarPorIdentificacion { get; set; }

        public int? IdOperadorLogistico { get; set; }

        public string? Estado { get; set; }
    }
}
