using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Facturas
{
    public class FacturaFlexResponse
    {
        [JsonProperty("Mensaje", NullValueHandling = NullValueHandling.Ignore)]
        public string? Mensaje { get; set; }

        [JsonProperty("Message", NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }

        [JsonProperty("Correlativo", NullValueHandling = NullValueHandling.Ignore)]
        public int? Correlativo { get; set; }

        [JsonProperty(nameof(Fecha), NullValueHandling = NullValueHandling.Ignore)]
        public string? Fecha { get; set; } = string.Empty;

        [JsonProperty(nameof(RespuestaHumalab), NullValueHandling = NullValueHandling.Ignore)]
        public string? RespuestaHumalab { get; set; } = string.Empty;
    }
}
