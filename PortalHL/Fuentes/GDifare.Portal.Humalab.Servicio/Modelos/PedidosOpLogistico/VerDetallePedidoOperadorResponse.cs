using System.Collections.Generic;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico
{
    public class VerDetallePedidoOperadorResponse
    {
        public VerDetallePedidoOperadorCab Pedido { get; set; }
        public List<VerDetallePedidoOperadorDet> Muestras { get; set; }
    }
}
