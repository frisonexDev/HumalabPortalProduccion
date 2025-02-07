using System;

namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class OrdenCab
    {
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set;}
        public int Edad { get; set; }
        public string Diagnostico { get; set;}
        public string ObservacionOrden { get; set;}
        public string ObservacionCliente { get; set;}
        public string ObservacionOpLogistico { get; set; }
    }
}