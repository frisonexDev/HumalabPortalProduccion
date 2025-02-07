using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class VerDetallePedidoOperadorRequest 
    {
        [Required]
        [JsonProperty("IdPedido")]
        public int IdPedido { get; set; }

    }
}
