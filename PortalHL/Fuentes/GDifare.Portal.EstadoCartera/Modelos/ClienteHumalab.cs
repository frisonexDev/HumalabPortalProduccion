


using System.ComponentModel.DataAnnotations.Schema;

namespace GDifare.Portal.EstadoCartera.Modelos
{
    public class ClienteHumalab
    {
        public string? Estadocliente { get; set; }


        public string? Fechasuspension { get; set; }

        public string Fecharegistro { get; set; } = string.Empty;


        public string Ruc { get; set; } = string.Empty;
    }
}