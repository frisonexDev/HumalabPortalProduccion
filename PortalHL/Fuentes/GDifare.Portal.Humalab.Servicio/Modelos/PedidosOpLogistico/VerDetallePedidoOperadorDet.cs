namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class VerDetallePedidoOperadorDet
    {
        public int Numero { get; set; }
        public string CodigoBarra { get; set; } = string.Empty;
        public string TipoMuestra { get; set; } = string.Empty;
        public DateTime FechaOrden { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public bool Retirado { get; set; }
        public bool Rechazado { get; set; }
        public string ObservacionOperador { get; set; } = string.Empty;
        public string EstadoMuestra { get; set; } = string.Empty;
        public int IdPruebaMuestra { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string CodLaboratorio { get; set; } = string.Empty;
        public string NombrePrueba { get; set; } = string.Empty;
	}
}
