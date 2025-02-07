using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;

public class PruebasResultPdf
{
    public int idResultado { get; set; }
    public int idLaboratorio { get; set; }
    public int idExamen { get; set; }
    public int idMuestra { get; set; }
    public string codigoOrden { get; set; } = string.Empty;
    public string codigoExterno { get; set; } = string.Empty;
    public string nombrePaciente { get; set; } = string.Empty;
    public string apellidoPaciente { get; set; } = string.Empty;
    public string generoPaciente { get; set; } = string.Empty;
    public string fechaNacimiento { get; set; } = string.Empty;
    public int edad { get; set; }
    public string estado { get; set; } = string.Empty;
    public bool esMicrobiologia { get; set; }
    public string nombreExamen { get; set; } = string.Empty;
    public string siglaExamen { get; set; } = string.Empty;
    public string codigoMuestra { get; set; } = string.Empty;
    public string descripcionMuestra { get; set; } = string.Empty;
    public string tipoResultado { get; set; } = string.Empty;
    public string siglasUnidad { get; set; } = string.Empty;
    public decimal refMinima { get; set; }
    public decimal refMaxima { get; set; }
    public decimal panMinima { get; set; }
    public decimal panMaxima { get; set; }
    public string resultadoActual { get; set; } = string.Empty;
    public string ResultadoAnterior { get; set; } = string.Empty;
    public string fechaCreación { get; set; } = string.Empty;
    public string fechaUltimoResultado { get; set; } = string.Empty;
    public string fechaValidacion { get; set; } = string.Empty;
    public string fechaImpresion { get; set; } = string.Empty;
    public string fechaEnvio { get; set; } = string.Empty;
    public string fechaToma { get; set; } = string.Empty;
    public string fechaRecepcion { get; set; } = string.Empty;
    public int usuarioResultado { get; set; }
    public int usuarioValidacion { get; set; }
    public int usuarioToma { get; set; }
    public int usuarioTransporte { get; set; }
    public int usuarioRecepcion { get; set; }
    public int transportado { get; set; }
    public bool tomado { get; set; }
    public bool recibido { get; set; }
    public bool resultado { get; set; }
    public bool repeticion { get; set; }
    public bool validado { get; set; }
    public bool enviado { get; set; }
    public bool impreso { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string ComentarioFleb { get; set; } = string.Empty;
    public string ComentarioRecepcion { get; set; } = string.Empty;
    public string ComentarioFijo { get; set; } = string.Empty;
    public string ComentarioImpresion { get; set; } = string.Empty;
    public bool MuestraInforme { get; set; }
    public bool AplicaFormula { get; set; }
    public int IdArea { get; set; }
    public string nombreArea { get; set; } = string.Empty;
    public string EdadCompleta { get; set; } = string.Empty;
    public string CodigoExamen { get; set; } = string.Empty;
    public int NumeroDecimales { get; set; }
    public bool activo { get; set; }
    public List<ResultadoLiteral> resultadoLiteral { get; set; }
    public string code { get; set; } = string.Empty;
    public string menasje { get; set; } = string.Empty;
    public string IdCorrelacion { get; set; } = string.Empty;
    public int OrdenArea { get; set; }
    public int OrdenExamen { get; set; }
}

public class ResultadoLiteral
{

}
