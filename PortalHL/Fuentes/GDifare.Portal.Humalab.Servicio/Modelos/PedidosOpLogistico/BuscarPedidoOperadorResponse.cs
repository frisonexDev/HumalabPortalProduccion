using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class BuscarPedidoOperadorResponse
    {
        [JsonProperty("idPedido")]
        public int IdPedido { get; set; }

        [JsonProperty("idCliente")]
        public int IdCliente { get; set; }

        [JsonProperty("Cliente")]
        public string Cliente { get; set; }
        
        [JsonProperty("NomLaboratorio")]
        public string NomLaboratorio { get; set; } = string.Empty;

        [JsonProperty("numeroRemision")]
        public string NumeroRemision { get; set; }

        [JsonProperty("fechaCreacion")]
        public DateTime? FechaCreacion { get; set; }

        [JsonProperty("totalOrdenes")]
        public int TotalOrdenes { get; set; }

        [JsonProperty("totalMuestras")]
        public int TotalMuestras { get; set; }

        [JsonProperty("totalRetiradas")]
        public int TotalRetiradas { get; set; }

        [JsonProperty("fechaRetiro")]
        public DateTime? FechaRetiro { get; set; }

        [JsonProperty("estadoPedido")]
        public string EstadoPedido { get; set; }

        [JsonProperty("observacionCliente")]
        public string ObservacionCliente { get; set; }

        [JsonProperty("Paciente")]
        public string Paciente { get; set; } = string.Empty;
    }
}
