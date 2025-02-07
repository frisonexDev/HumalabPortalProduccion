using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class ClienteHumalabResponse
{    
    public IList<object> data = new List<object>();
    public int totalRegistros { get; set; }
    public int totalPaginas { get; set; }
}

public class DataClientes
{    
    public int IdCliente { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Ruc { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string FechaRegistro { get; set; } = string.Empty;
    public string OperadorLogistico { get; set; } = string.Empty;
    public int EstadoCodigo { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string FechaTemporal { get; set; } = string.Empty;
    public string Observacion { get; set; } = string.Empty;
}

public class ClientesFinal
{
    public List<DataClientes> dataClientes { get; set; } = new List<DataClientes>();
    public int totalRegistros { get; set; }
    public int totalPaginas { get; set; }
    public string codigoRespuesta { get; set;} = string.Empty; 
    public string mensajeRespuesta { get; set; } = string.Empty;
}
