using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class RecogerMuestraRequest 
    {
        [Required]
        [JsonProperty("IdMuestra")]
        public int IdMuestra { get; set; }
        [Required]
        [JsonProperty("Recolectar")]
        public bool Recolectar { get; set; }
        [Required]
        [JsonProperty("Rechazar")]
        public bool Rechazar { get; set; }
        [Required]
        [JsonProperty("EsOperador")]
        public bool EsOperador { get; set; }
        [Required]
        [JsonProperty("IdOperador")]
        public int IdOperador { get; set; }
        [JsonProperty("NombreUsuario")]
        public string NombreUsuario { get; set; }
        [JsonProperty("Observacion")]
        public string? Observacion { get; set; }

    }
}
