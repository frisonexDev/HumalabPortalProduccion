namespace GDifare.Portal.HumaLab.UI.Models;

public class ListaPruebasHumalab
{
    public string Abreviatura { get; set; } = string.Empty;
    public string CodigoExamen { get; set; } = string.Empty;
    public string Metodologia { get; set; } = string.Empty;
    public string Muestra { get; set; } = string.Empty;
    public string MuestraAlterna { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;
    public double? Precio { get; set; }
    public string Recipiente { get; set; } = string.Empty;
}
