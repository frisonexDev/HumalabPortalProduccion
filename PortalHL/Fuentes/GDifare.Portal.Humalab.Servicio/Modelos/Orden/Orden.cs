using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden
{
    public class Orden
    {
        public int? IdOrden { get; set; }
        public int? IdPedido { get; set; }
        public int? IdUsuarioGalileo { get; set; }
        public string? Identificacion { get; set; }
        public string? CodigoBarra { get; set; }
        public string? Medicamento { get; set; }
        public string? Diagnostico { get; set; }
        public string? Observacion { get; set; }        
        public int Estado { get; set; }
        public List<Pruebas> Pruebas { get; set; } = new List<Pruebas>();

        public Pacientes Paciente { get; set; }= new Pacientes();
        public int? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int? EmpresaId { get; set; }

    }

    public class Pruebas
    {
        public int? IdPruebaGalileo { get; set; }
        public int? IdMuestraGalileo { get; set; }
        public int? IdOrden { get; set; }
        public string? CodigoExamen { get; set; }
        public bool EsPerfil { get; set; }  
        public string? CodigoBarra { get; set; } 
        public string? Nombre { get; set; }
        public string? Abreviatura { get; set; }
        public string? Metodologia { get; set; }
        public string? NombreMuestra { get; set; }
        public string? MuestraAlterna { get; set; }
        public string? Recipiente { get; set; }
        public float? Precio { get; set; }
        public int? EstadoMuestra { get; set; }
        public int? EstadoPrueba { get; set; }   
        public int? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }

    public class Muestras
    {
        public int? IdMuestra { get; set; }
        public string? CodigoBarra { get; set; }
        public int? UsuarioCreacion { get; set; }

    }


    public class Pacientes
    {
        public string? Identificacion { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public bool? Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Edad { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public int TipoPaciente { get; set; } //verificar
        public string CodLaboratorio {  get; set; } = string.Empty;
    }




}
