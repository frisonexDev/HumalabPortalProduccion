using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Facturas
{
    public class ConsolidarFacturacion
    {
        [Column("datosFactura")]
        [JsonProperty("datosFactura")]
        public DatosFactura? datosFactura { get; set; }

        [Column("clienteRequest")]
        [JsonProperty("clienteRequest")]
        public ClienteRequest? clienteRequest { get; set; }


        [Column("facturaFlexResquest")]
        [JsonProperty("facturaFlexResquest")]
        public FacturaFlexResquest? facturaFlexResquest { get; set; }


    }
}
