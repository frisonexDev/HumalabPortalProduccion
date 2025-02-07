
using Newtonsoft.Json;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class RecogerMuestraResponse 
    {
        [JsonProperty("IdMuestra")]
        public int IdMuestra { get; set; }

        [JsonProperty("EstadoMuestra")]
        public string? EstadoMuestra { get; set; }
        [JsonProperty("Observacion")]
        public string? Observacion { get; set; }
    }
}
