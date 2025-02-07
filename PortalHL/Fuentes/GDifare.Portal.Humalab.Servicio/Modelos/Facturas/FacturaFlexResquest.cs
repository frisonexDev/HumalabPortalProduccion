using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Facturas
{
    public class FacturaFlexResquest
    {
        [JsonProperty("Empresa", NullValueHandling = NullValueHandling.Ignore)]
        public string? Empresa { get; set; }

        [JsonProperty("Ruc", NullValueHandling = NullValueHandling.Ignore)]
        public string? Ruc { get; set; }


        [JsonProperty(nameof(Asesor), NullValueHandling = NullValueHandling.Ignore)]
        public string? Asesor { get; set; } = string.Empty;

      
        [JsonProperty(nameof(detalle), NullValueHandling = NullValueHandling.Ignore)]
        public List<FacturaDetalleFlex>? detalle { get; set; } 


    }
}
