using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Seguridad.Modelos.Perfil;

public class LaboratorioResponse
{
    public List<Laboratorio> laboratorio {  get; set; }
}

public class DetallesLaboratorio
{
    public int IdLaboratorio { get; set; }
    public string NombreLaboratorio { get; set; } = string.Empty;
    public string Abreviatura {  get; set; } = string.Empty;
    public string CallePrincipal { get; set; } = string.Empty;
    public string CalleSecundaria {  get; set; } = string.Empty;
    public int? IdCiudad {  get; set; }
    public string Ciudad {  get; set; } = string.Empty;
    public int? IdPais { get; set; }
    public string Pais { get; set; } = string.Empty;
    public string Numeracion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string NombreContacto { get; set; } = string.Empty;
    public string Ruc { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string VencimientoLicencia {  get; set; } = string.Empty;
    public bool Activo { get; set; }
    public int? HomologacionEmpresa {  get; set; }
    public string Logotipo {  get; set; } = string.Empty;
}

public class Laboratorio
{
    public string code { get; set; } = string.Empty;
    public List<DetallesLaboratorio> details { get; set; }
    public string message { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
}