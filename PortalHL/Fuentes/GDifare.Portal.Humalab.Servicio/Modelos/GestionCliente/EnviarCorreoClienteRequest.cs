using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class EnviarCorreoClienteRequest
{
	public string Usuario { get; set; }	= string.Empty;
	public string Contrasena { get; set; } = string.Empty;
	public string Url { get; set; } = string.Empty;
	public int UsuarioCreacion { get; set; } = 0;
	public DateTime FechaCreacion { get; set; } 
	public string NomUsuario { get; set; } = string.Empty;
}