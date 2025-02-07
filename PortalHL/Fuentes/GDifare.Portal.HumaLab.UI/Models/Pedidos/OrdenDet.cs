using System;

namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class OrdenDet
    {
        public int IdOrden { get; set; }
        public int IdPrueba { get; set; }
        public int Codigo { get; set; }
        public string PruebaPerfil { get; set; }
        public string Muestra { get; set; }
        public string CodigoBarra { get; set; }
        public int IdMuestra { get; set; }
    }
}