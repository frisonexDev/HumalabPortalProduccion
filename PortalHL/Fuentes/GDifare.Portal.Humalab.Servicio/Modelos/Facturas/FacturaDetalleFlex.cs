using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Facturas
{
    public class FacturaDetalleFlex
    {
        [JsonProperty("Codigo", NullValueHandling = NullValueHandling.Ignore)]
        public string? Codigo { get; set; }

        [JsonProperty("Cantidad", NullValueHandling = NullValueHandling.Ignore)]
        public string? Cantidad { get; set; }


        [JsonProperty(nameof(Precio), NullValueHandling = NullValueHandling.Ignore)]
        public string? Precio { get; set; } = string.Empty;

        [JsonProperty(nameof(Serie), NullValueHandling = NullValueHandling.Ignore)]
        public string? Serie { get; set; } = string.Empty;

        [JsonProperty(nameof(Lote), NullValueHandling = NullValueHandling.Ignore)]
        public string? Lote { get; set; } = string.Empty;

        [JsonProperty(nameof(FechaVcto), NullValueHandling = NullValueHandling.Ignore)]
        public string? FechaVcto { get; set; } = string.Empty;
    }
}
