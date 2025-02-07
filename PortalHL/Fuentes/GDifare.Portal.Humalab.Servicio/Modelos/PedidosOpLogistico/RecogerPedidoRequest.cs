using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;

public class RecogerPedidoRequest
{
    [Required]
    [JsonProperty("IdPedido")]
    public int IdPedido { get; set; }
    [Required]
    [JsonProperty("IdOperador")]
    public int IdOperador { get; set; }
    [Required]
    [JsonProperty("ObservacionOperador")]
    public string ObservacionOperador { get; set; }

}

public class RecogerPedidosRequest
{
    [Required]
    [JsonProperty("IdPedido")]
    public string IdPedido { get; set; }
    [Required]
    [JsonProperty("IdOperador")]
    public int IdOperador { get; set; }
    [Required]
    [JsonProperty("ObservacionOperador")]
    public string ObservacionOperador { get; set; }
}
