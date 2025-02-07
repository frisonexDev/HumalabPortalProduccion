namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class ConsultaPedidosOpLogistico
    {
        public bool SeleccionPedido { get; set; }
        public string? Cliente { get; set; }
        public string? NumeroRemision { get; set; }
        public string? NomLab { get; set; }
        public DateTime? FechaPedido { get; set; }
        public int TotalOrdenes { get; set; }
        public int TotalMuestras { get; set; }
        public int TotalRetiradas { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string? EstadoPedido { get; set; }
        public int IdPedido { get; set; }
        public string Paciente { get; set; } = string.Empty;

    }
}
