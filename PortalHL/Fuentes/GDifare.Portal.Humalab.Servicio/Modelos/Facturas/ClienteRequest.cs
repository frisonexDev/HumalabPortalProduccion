using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Facturas
{
    public class ClienteRequest
    {   
#nullable enable
        [Column("Ruc")]
        [JsonProperty("Ruc")]
        public string? Ruc { get; set; }

        [Column("Estado")]
        [JsonProperty("Estado")]
        public string? Estado { get; set; }

        [Column("Desde")]
        [JsonProperty("Desde")]
        public string? Desde { get; set; }

        [Column("Hasta")]
        [JsonProperty("Hasta")]
        public string? Hasta { get; set; }

        [Column("Tipo")]
        [JsonProperty("Tipo")]
        public string? Tipo { get; set; }

        [Column("Valor")]
        [JsonProperty("Valor")]
        public string? Valor {  get; set; }

    }

}