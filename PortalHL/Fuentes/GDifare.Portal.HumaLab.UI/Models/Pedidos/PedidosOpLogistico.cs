
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;

namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class PedidosOpLogistico
    {
        public BuscarPedidosOpLogistico? BuscarPedidos { get; set; }
        public List<ConsultaPedidosOpLogistico>? ConsultaPedidos { get; set; }

        public EntregarPedidoRequest? EntregaPedidos { get; set; }
    }
}
