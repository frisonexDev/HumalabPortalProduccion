namespace GDifare.Portal.HumaLab.UI.Models.Pedidos;

public class ExportarDatosRequest
{
    public int IdPedido { get; set; }
    public int IdOrden {  get; set; }
    public string IdentificacionPac {  get; set; } = string.Empty;
    public string CodigoBarraOrden { get; set; } = string.Empty;
    public string NombresPac { get; set; } = string.Empty;
    public string EstadoPedido { get; set; } = string.Empty;
    public string FechaCreacion { get; set; } = string.Empty;
    public string ObservacionMuestras { get; set; } = string.Empty;
    public string Resultados { get; set; } = string.Empty;
    public string UsuarioOperador { get; set; } = string.Empty;
    public string RucLab { get; set; } = string.Empty;
    public string Operador { get; set; } = string.Empty;
    public string ClienteNombre { get; set; } = string.Empty;
    public string CodLaboratorio { get; set; } = string.Empty;
    public string TipoPaciente {  get; set; } = string.Empty;
}
