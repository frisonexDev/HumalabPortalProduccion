using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class EntregarPedidoRequest
    {
        [Required]
        [JsonProperty("Pedidos")]
        public string Pedidos { get; set; }
        [Required]
        [JsonProperty("IdOperador")]
        public int IdOperador { get; set; }
        [Required]
        [JsonProperty("ObservacionOperador")]
        public string ObservacionOperador { get; set; }

    }
}
