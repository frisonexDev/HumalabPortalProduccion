namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class ConsultaPedidosLaboratorista
    {
        public int? IdPedido { get; set; }

        public int? IdOrden { get; set; }

        public string? CodigoBarraOrden { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? UsuarioOperador { get; set; }

        public string? EstadoPedido { get; set; }

        public string? ObservacionMuestras { get; set; }
        public string? Resultados {  get; set; }
        public string IdentificacionPac { get; set; } = string.Empty;
        public string NombresPac { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;        

    }
}
