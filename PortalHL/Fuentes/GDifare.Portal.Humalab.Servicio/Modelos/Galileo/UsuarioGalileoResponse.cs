using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Galileo;

public class UsuarioGalileoResponse
{
	public int? usuarioID {  get; set; }
	public int? rolID { get; set; }
	public string? usuario { get; set; } 
	public string? clave { get; set; } 
	public string? identificacion { get; set; }
	public string? nombre { get; set; } 
	public string? primerApellido { get; set; }
	public string? segundoApellido { get; set; }
	public string? correo { get; set; }
	public int? estadoID { get; set; }
	public string? descripcionEstado { get; set; }
	public int? empresaID { get; set; } = 0;
	public int? diaExpiracion { get; set; } = 0;
	public string? fechaExpiracion { get; set; }
	public List<RolesGalileo> roles { get; set; } = new List<RolesGalileo>();
    //public string usuarioHomologacion { get; set; } = string.Empty;
    public List<object>? usuarioHomologacion { get; set; }
    public string? foto { get; set; }	
	public string? firma { get; set; }
	public string? codigoRespuesta { get; set; } 
	public string? mensajeRespuesta { get; set; }
}

public class RolesGalileo
{
	public int? usuarioRolID { get; set; }
	public int? usuarioID { get; set; }
	public int? rolID { get; set; }
	public int? empresaID { get; set; }
	public string rolDescripcion { get; set; } = string.Empty;
}