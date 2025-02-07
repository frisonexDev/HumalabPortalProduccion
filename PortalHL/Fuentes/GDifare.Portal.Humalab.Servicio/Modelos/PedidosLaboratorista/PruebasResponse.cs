using Newtonsoft.Json;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class PruebasResponse
    {
        [JsonProperty("IdOrden")]
        public int IdOrden { get; set; }

        [JsonProperty("IdPrueba")]
        public int IdPrueba { get; set; }

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("PruebaPerfil")]
        public string PruebaPerfil { get; set; }

        [JsonProperty("PruebaRechazada")]
        public bool PruebaRechazada { get; set; }

        [JsonProperty("ObservacionPrueba")]
        public string ObservacionPrueba { get; set; }

    }
}
