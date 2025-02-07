namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class VerDetallePedidoOperadorCab
    {
        public int IdPedido { get; set; }
        public string NumeroRemision { get; set; } = string.Empty;
        public string EstadoPedido { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string ObservacionCliente { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string OperadorLogistico { get; set; } = string.Empty;
        public string CorreoCliente { get; set; } = string.Empty;
        public string ObservacionOperador { get; set; } = string.Empty;
        public int TotalMuestras { get; set; }
        public int MuestrasRecolectadas { get; set; }
		public string Telefono { get; set; } = string.Empty;
        public string Paciente { get; set; } = string.Empty;
        public string FechaCreacion { get; set; } = string.Empty;
		public string ClienteDireccion { get; set; } = string.Empty;
		public string ClienteCiudad { get; set; } = string.Empty;
		public string Laboratorio { get; set; } = string.Empty;
	}
}
