using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class DarAltaClienteHumalabRequest
{
	public int idGalileo { get; set; }
	public string Usuario { get; set; } = string.Empty;
	public string Identificacion { get; set; } = string.Empty;	
	public string Email { get; set; } = string.Empty;
	public int UsuarioCreacion { get; set; }
	public string idAsesorFlex { get; set; } = string.Empty;
	public string nombreAsesor { get; set; } = string.Empty;
	public string nombreGalileo { get; set; } = string.Empty;
	public string codigoCliente { get; set; } = string.Empty;
	public string DireccionCliente { get; set; } = string.Empty;
	public string ProvinciaCliente { get; set; } = string.Empty;
	public string CiudadCliente { get; set; } = string.Empty;
	public string LatitudCliente { get; set; } = string.Empty;	
	public string LongitudCliente { get; set; } = string.Empty;
	public string IdAsesorLis { get; set; } = string.Empty;
	public string Telefono { get; set; } = string.Empty;
    public string LabComercial { get; set; } = string.Empty;

}


public class DarAltaClienteHumalabRequestService
{
	public int idGalileo { get; set; }
	public string Usuario { get; set; } = string.Empty;
	public string Identificacion { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public int UsuarioCreacion { get; set; }
	public string idAsesorFlex { get; set; } = string.Empty;
	public int IdRol { get; set; }
	public string nombreAsesor { get; set; } = string.Empty;
	public string nombreGalileo { get; set; } = string.Empty;
	public string codigoCliente { get; set; } = string.Empty;
	public string DireccionCliente { get; set; } = string.Empty;
	public string ProvinciaCliente { get; set; } = string.Empty;
	public string CiudadCliente { get; set; } = string.Empty;
	public string LatitudCliente { get; set; } = string.Empty;
	public string LongitudCliente { get; set; } = string.Empty;
	public string IdAsesorLis { get; set; } = string.Empty;
	public string Telefono { get; set; } = string.Empty;
	public string LabComercial { get; set; } = string.Empty;
}