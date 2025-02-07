using System;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class OrdenCab
    {
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set;}
        public int Edad { get; set; }
        public string Diagnostico { get; set;}
        public string Medicamento { get; set; }
        public string ObservacionOrden { get; set;}
        public string ObservacionCliente { get; set;}
        public string ObservacionOpLogistico { get; set; }
        public string FechaEnvio { get; set; }
        public string NombresPaciente { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string CiudadCliente { get; set; } = string.Empty;
		public string RucLab { get; set; } = string.Empty;
		public string Operador { get; set; } = string.Empty;
		public string ClienteNombre { get; set; } = string.Empty;
		public string CodLaboratorio { get; set; } = string.Empty;
		public string TipoPaciente { get; set; } = string.Empty;
	}
}