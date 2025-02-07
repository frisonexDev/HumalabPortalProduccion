using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class BuscarPedidoLaboratoristaRequest
    {

        [JsonProperty("FechaDesde")]
        public DateTime FechaDesde { get; set; }

        [JsonProperty("FechaHasta")]
        public DateTime FechaHasta { get; set; }

        [JsonProperty("Operador")]
        public string? Operador { get; set; }

        [DefaultValue(true)]
        [JsonProperty("BuscarPorRuc")]
        public bool? BuscarPorRuc { get; set; }

        [JsonProperty("Cliente")]
        public string? Cliente { get; set; }

        [JsonProperty("Estado")]
        public string? Estado { get; set; }

        [JsonProperty("IdPedido")]
        public int? IdPedido { get; set; }

        [JsonProperty("IdOrden")]
        public int? IdOrden { get; set; }

        [JsonProperty("IdAsesor")]
        public int? IdAsesor { get; set; }

    }
}
