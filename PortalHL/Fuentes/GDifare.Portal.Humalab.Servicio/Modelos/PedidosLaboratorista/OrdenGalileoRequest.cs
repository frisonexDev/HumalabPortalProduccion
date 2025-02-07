using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista;

public class Paciente
{
    public string Identificador { get; set; }
    public string NombrePaciente { get; set; }
    public string ApellidoPaciente { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Genero { get; set; }
    public string CorreoElectronico { get; set; }
    public List<CampoAdicional> CamposAdicional { get; set; }
}

public class CampoAdicional
{
    public string NombreCampo { get; set; }
    public string Valor { get; set; }
}

public class Medico
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Matricula { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
}

public class TomaMuestra
{
    public bool TomaRemota { get; set; }
    public string DireccionToma { get; set; }
    public bool Activo { get; set; }
}

public class OrdenGalileo
{
    public int IdLaboratorio { get; set; }
    public int IdSede { get; set; }
    public string Laboratorio { get; set; }
    public string Cliente { get; set; }
    // ... otras propiedades
    public Paciente Paciente { get; set; }
    public Medico Medico { get; set; }
    // ... otras propiedades
    public List<DetalleOrden> DetalleOrden { get; set; }
    public List<InformacionOrden> InformacionOrden { get; set; }
    public TomaMuestra TomaMuestra { get; set; }
    public bool Activo { get; set; }
}

public class DetalleOrden
{
    public string NombreExamenPerfil { get; set; }
    public string CodigoExamen { get; set; }
}

public class InformacionOrden
{
    public string NombreCampo { get; set; }
    public string Valor { get; set; }
}

