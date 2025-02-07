using Newtonsoft.Json;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;

public class RecogerPedidoResponse
{
    public int IdPedido { get; set; }
    public bool procesoExitoso { get; set; }
    public string codigoRespuesta  { get; set; }
    public string mensajeRespuesta { get; set; }

}