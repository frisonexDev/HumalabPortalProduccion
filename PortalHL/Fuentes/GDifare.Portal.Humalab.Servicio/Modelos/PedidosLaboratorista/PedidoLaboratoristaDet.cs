using System;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class PedidoLaboratorisataDet
    {
        public int IdOrden { get; set; }

        public int IdPedido { get; set; }

        public string CodigoBarra { get; set; }
        public DateTime FechaCreacion { get; set; }

        public string UsuarioOperador { get; set; }

        public string EstadoPedido { get; set; }

        public string ObservacionMuestras { get; set; }
        public string Resultados { get; set; } = string.Empty;
        public string IdentificacionPac { get; set; } = string.Empty;
        public string NombresPac { get; set; } = string.Empty;
        public string ClienteNombre { get; set; } = string.Empty;        
    }
}
