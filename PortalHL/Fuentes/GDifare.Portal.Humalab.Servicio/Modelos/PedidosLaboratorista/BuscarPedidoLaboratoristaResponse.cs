using System.Collections.Generic;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista
{
    public class BuscarPedidoLaboratoristaResponse
    {
        public PedidoLaboratoristaCab ResumenMuestra { get; set; }
        public List<PedidoLaboratorisataDet> Ordenes { get; set; }
    }
}
