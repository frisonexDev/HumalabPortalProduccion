using PdfSharpCore.Pdf.AcroForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;

public class ListOrdClienteExcel
{
    public string CodigoBarra { get; set; } = string.Empty;
    public string MuestraOrden {  get; set; } = string.Empty;
    public string NombresPaciente { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public string CodigoExamen { get; set; } = string.Empty;
    public string NombrePrueba { get; set; } = string.Empty;
    public string Operador { get; set; } = string.Empty;
    public string EstadoOrden { get; set; } = string.Empty;
    public string EstadoMuestra { get; set; } = string.Empty;
    public string EstadoPrueba { get; set; } = string.Empty;
    public string FechaCreacion { get; set; } = string.Empty;
    public string OrdenLis { get; set; } = string.Empty;
}
