using System;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class OrdenDet
    {
        public int IdOrden { get; set; }
        public int IdPrueba { get; set; }
        public int Codigo { get; set; }
        public string PruebaPerfil { get; set; }
        public string CodigoExamen { get; set; } = string.Empty;
        public bool PruebaRechazada { get; set; }
        public string ObservacionPrueba { get; set; }
        public string Muestra { get; set; }
        public string CodigoBarra { get; set; }
        public int IdMuestra { get; set; }
        public bool Recibido { get; set; }
        public bool Rechazado { get; set; }
        public string ObservacionMuestra { get; set; }
        public string EstadoMuestra { get; set; }
        public string EstadoOrden { get; set; } = string.Empty;
		public string FechaCreacion { get; set; } = string.Empty;
		public string CodLis { get; set; } = string.Empty;
        public string EstadoPrueba { get; set; } = string.Empty;
    }
}