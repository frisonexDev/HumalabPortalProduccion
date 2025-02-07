using Newtonsoft.Json;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class EntregarPedidoResponse
    {
        [JsonProperty("Entregado")]
        public bool Entregado { get; set; }
    
    }
}
