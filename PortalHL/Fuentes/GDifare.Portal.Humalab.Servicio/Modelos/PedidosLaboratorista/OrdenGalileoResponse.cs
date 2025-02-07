using Newtonsoft.Json;
using System;
using System.Drawing;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class OrdenGalileoResponse
    {
        [JsonProperty("Cliente")]
        public string Cliente { get; set; }

        [JsonProperty("FechaIngreso")]
        public DateTime FechaIngreso { get; set; }

        [JsonProperty("Comentario")]
        public string Comentario { get; set; }

        [JsonProperty("Identificador")]
        public string Identificador { get; set; }

        [JsonProperty("NombrePaciente")]
        public string NombrePaciente { get; set; }

        [JsonProperty("ApellidoPaciente")]
        public string ApellidoPaciente { get; set; }

        [JsonProperty("FechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [JsonProperty("Genero")]
        public string Genero { get; set; }

        [JsonProperty("CorreoElectronico")]
        public string CorreoElectronico { get; set; }

        [JsonProperty("nombreCampoAdicional")]
        public string nombreCampoAdicional { get; set; }

        [JsonProperty("valor")]
        public string valor { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Apellido")]
        public string Apellido { get; set; }

        [JsonProperty("Matricula")]
        public string Matricula { get; set; }

        [JsonProperty("Telefono")]
        public string Telefono { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("medicamento")]
        public string medicamento { get; set; }

        [JsonProperty("diagnostico")]
        public string diagnostico { get; set; }

        [JsonProperty("CodigoExamen")]
        public string CodigoExamen { get; set; }

        [JsonProperty("NombreExamenPerfil")]
        public string NombreExamenPerfil { get; set; }

    }
}