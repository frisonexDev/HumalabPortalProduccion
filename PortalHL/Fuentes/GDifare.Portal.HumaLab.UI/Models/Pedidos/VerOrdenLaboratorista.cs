using System.Collections.Generic;

namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class VerOrdenLaboratorista
    {
        public OrdenCab Orden { get; set; }
        public List<OrdenDet> PruebasMuestras { get; set; }
    }
}