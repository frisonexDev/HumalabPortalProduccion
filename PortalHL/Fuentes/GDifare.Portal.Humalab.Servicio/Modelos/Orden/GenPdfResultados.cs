using Newtonsoft.Json;
using PdfSharpCore.Pdf.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;

public class GenPdfResultados
{
    public CabeceraOrden? cabecera { get; set; }
    public List<PacientesPdf>? paciente { get; set; }
    public List<MedicoPdf>? medico { get; set; }    
    public List<PruebasPdf>? pruebas { get; set; }
}

public class CabeceraOrden
{
    public int IdOrden { get; set; }
    public int IdLaboratorio { get; set; }
    public string NombreLaboratorio { get; set; } = string.Empty;
    public int IdSede { get; set; }
    public string NombreSede { get; set; } = string.Empty;
    public int IdDiagnostico { get; set; }
    public string DescripcionDiagnostico { get; set; } = string.Empty;
    public int IdPaciente { get; set; }
    public int Edad { get; set; }
    public int IdMedico { get; set; }
    public string NombreMedico { get; set; } = string.Empty;
    public string PrefijoOrden { get; set;} = string.Empty;
    public string CodigoOrden { get; set; } = string.Empty;
    public int NumeroOrden { get; set; }
    public string PrioridadOrden { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string CodigoExterno { get; set;} = string.Empty;
    public int IdUsuario { get; set; }
    public int IdEstadoOrden { get; set; }
    public bool Activo { get; set; }
    public string DetalleOrden { get; set; } = string.Empty;
    public string InformacionOrden { get; set; } = string.Empty;
    public string InformacionPaciente { get; set; } = string.Empty;
    public string TomaMuestra { get; set; } = string.Empty;
    public string EdadCompleta { get; set; } = string.Empty;    
}

public class PacientesPdf
{
    public string Identificador { get; set; } = string.Empty;
    public string NombrePac { get; set; } = string.Empty;
    public string ApelliPac { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string GenPac { get; set; } = string.Empty;
    public string MailPac { get; set; } = string.Empty;    
}

public class MedicoPdf
{
    public string ApeMedico { get; set; } = string.Empty;
    public string EmailMedico { get; set; } = string.Empty;
    public string NomMedico { get; set; } = string.Empty;
    public string TeleMedico { get; set; } = string.Empty;
}

public class PruebasPdf
{
    public int idResultado { get; set; }
    public int idLaboratorio { get; set; }
    public int idExamen { get; set; }
    public int idMuestra { get; set; }  
    public string codigoOrden { get; set; } = string.Empty;    
    public object? codigoExterno { get; set; }
    public string nombrePaciente { get; set; } = string.Empty;
    public string apellidoPaciente { get; set; } = string.Empty;
    public string generoPaciente { get; set; } = string.Empty;    
    public DateTime? fechaNacimiento { get; set; }
    public int edad { get; set; }
    public string estado { get; set; } = string.Empty;  
    public bool? esMicrobiologia { get; set; }
    public string nombreExamen { get; set; } = string.Empty;
    public string siglaExamen { get; set; } = string.Empty;
    public string codigoMuestra { get; set; } = string.Empty;
    public string descripcionMuestra { get; set; } = string.Empty;
    public string tipoResultado { get; set; } = string.Empty;
    public string siglasUnidad { get; set; } = string.Empty;
    public double? refMinima { get; set; }
    public double? refMaxima { get; set; }
    public double? panMinima { get; set; }
    public double? panMaxima { get; set; }
    public string resultadoActual { get; set; } = string.Empty;    
    public object? ResultadoAnterior { get; set; }      
    public DateTime fechaCreacion { get; set; }    
    public DateTime fechaUltimoResultado { get; set; }
    public DateTime fechaValidacion { get; set; }
    public DateTime fechaImpresion { get; set; }
    public DateTime fechaEnvio { get; set; }
    public DateTime fechaToma { get; set; }
    public DateTime fechaRecepcion { get; set; }
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
    ////public int EdadCompleta { get; set; }    
    public string EdadCompleta { get; set; } = string.Empty;
    public string CodigoExamen { get; set; } = string.Empty;
    public int NumeroDecimales { get; set; }
    //public string activo { get; set; } = string.Empty;
    public bool activo { get; set; }
    public int? IdCorrelacion { get; set; }
    public int OrdenArea { get; set; }
    public int OrdenExamen { get; set; }
    public List<ResultadoLiteralPdf>? resultadoLiteral { get; set; }
    public object? code { get; set; }
    public object? menasje { get; set; }
}

public class ResultadoLiteralPdf
{
    public string Texto { get; set; } = string.Empty;   
}