using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos;

public class AppServicioMicrosExternos
{
    public string ServerFlexLine { get; set; } = string.Empty;
    public int PortFlexLine { get; set; } = 0;
    public string TokenFlexline { get; set; } = string.Empty;
    public string PathServicesClientesFlexLine { get; set; } = string.Empty;
    public string PathServicesAsesorFlexline { get; set;} = string.Empty;
    public string ServerGalileo { get; set; } = string.Empty;
    public int PortGalileo { get; set; } = 0;
    public string TokenGalileo { get; set; } = string.Empty;
    public string PathServicesGalileo { get; set; } = string.Empty;

    public string ServerGalileoCatalogoPruebas { get; set; } = string.Empty;
    public int PortGalileoCatalogoPruebas { get; set; } = 0;
    public string TokenGalileoCatalogoPruebas { get; set; } = string.Empty;
    public string PathServicioCatologoPruebas { get; set; } = string.Empty; 

    //cabecera orden pdf resultados
    public string ServerOrdenCabecera { get; set; } = string.Empty;
    public string PathOrdenCabecera { get; set; } = string.Empty;

    //paciente pdf resultados
    public string ServerPacResult { get; set; } = string.Empty;
    public string PathPacResult { get; set; } = string.Empty;

    //medico pdf resultados
    public string ServerMedResult { get; set; } = string.Empty;
    public string PathMedResult { get; set; } = string.Empty;

    //pruebas pdf resultados
    public string ServerPrueResult { get; set; } = string.Empty;
    public string PathPruebResult { get; set; } = string.Empty;

    //cartera estado
    public string PathServicesCarteraEstado {  get; set; } = string.Empty;
}
