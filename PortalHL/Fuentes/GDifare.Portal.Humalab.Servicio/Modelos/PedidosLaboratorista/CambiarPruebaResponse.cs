
using Newtonsoft.Json;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class CambiarPruebaResponse
    {
        [JsonProperty("IdMuestra")]
        public int IdMuestra { get; set; }
        [JsonProperty("IdPrueba")]
        public int IdPrueba { get; set; }
        [JsonProperty("RechazaMuestra")]
        public bool RechazaMuestra { get; set; }
        [JsonProperty("ActivaMuestra")]
        public bool ActivaMuestra { get; set; }
        [JsonProperty("EstadoPrueba")]
        public string EstadoPrueba { get; set; }
   
    }
}
