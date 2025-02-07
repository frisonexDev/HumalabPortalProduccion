using System.Collections.Generic;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class VerOrdenLaboratoristaResponse
    {
        public OrdenCab Orden { get; set; }
        public List<OrdenDet> PruebasMuestras { get; set; }
    }
}