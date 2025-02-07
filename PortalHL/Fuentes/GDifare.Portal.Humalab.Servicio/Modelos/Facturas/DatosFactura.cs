using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Facturas
{
    public class DatosFactura
    {

        [Column("Ruc")]
        [JsonProperty("Ruc")]
        public string? Ruc { get; set; }


        [Column("CodClienteCta")]
        [JsonProperty("CodClienteCta")]
        public string? CodClienteCta { get; set; }

        [Column("cliente")]
        [JsonProperty("cliente")]
        public string? cliente { get; set; }

        [Column("fecha")]
        [JsonProperty("fecha")]
        public string? fecha { get; set; }

        [Column("numeroFactura")]
        [JsonProperty("numeroFactura")]
        public string? numeroFactura { get; set; }

        [Column("estadoFactura")]
        [JsonProperty("estadoFactura")]
        public string? estadoFactura { get; set; }

        [Display(Name = "facturaList")]
        public List<Factura>? facturaList { get; set; }

        [Display(Name = "OrdenList")]
        public List<string>? OrdenList { get; set; }



        [Column("totalMuestra")]
        [JsonProperty("totalMuestra")]
        public string? totalMuestra { get; set; }


        [Column("totalFactura")]
        [JsonProperty("totalFactura")]
        public string? totalFactura { get; set; }



        [Column("identificacionUsuario")]
        [JsonProperty("identificacionUsuario")]
        public string? identificacionUsuario { get; set; }



        [Column("usuarioCreacion")]
        [JsonProperty("usuarioCreacion")]
        public string? usuarioCreacion { get; set; }

        [Column("idOrden")]
        [JsonProperty("idOrden")]
        public List<string>? idOrden { get; set; }
    }
}
