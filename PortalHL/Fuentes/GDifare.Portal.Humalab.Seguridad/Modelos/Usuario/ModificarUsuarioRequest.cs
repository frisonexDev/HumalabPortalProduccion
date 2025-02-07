using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class ModificarUsuarioRequest
    {
        [Display(Name = "ID")]
        public int UsuarioID { get; set; }

        [Display(Name = "USUARIO")]
        [Required(ErrorMessage = "El usuario es requerido")]
        [MaxLength(50, ErrorMessage = "El usuario no puede ser mayor a 50 caracteres")]
        public string Usuario { get; set; }

        [Display(Name = "EMPRESA")]
        [Required(ErrorMessage = "La empresa es requerida")]
        public int EmpresaID { get; set; }

        [Display(Name = "ROL")]
        [Required(ErrorMessage = "El rol es requerido")]
        public int RolID { get; set; }

        [Display(Name = "CLAVE")]
        [Required(ErrorMessage = "La clave es requerido")]
        [MaxLength(35, ErrorMessage = "La clave no puede ser mayor a 35 caracteres")]
        public string Clave { get; set; }

        [Display(Name = "IDENTIFICACIÓN")]
        [Required(ErrorMessage = "La identificación de usuario es requerido")]
        [MaxLength(20, ErrorMessage = "La identificación de usuario no puede ser mayor a 20 caracteres")]
        public string Identificacion { get; set; }

        [Display(Name = "NOMBRE")]
        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]*[0-9]*$", ErrorMessage = "No se pueden ingresar caracteres especiales")]
        [MaxLength(100, ErrorMessage = "El nombre del usuario no puede ser mayor a 100 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "PRIMER APELLIDO")]
        [Required(ErrorMessage = "El primer apellido del usuario es requerido")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]*$", ErrorMessage = "No se pueden ingresar caracteres especiales")]
        [MaxLength(100, ErrorMessage = "El primer apellido del usuario no puede ser mayor a 100 caracteres")]
        public string PrimerApellido { get; set; }

        [Display(Name = "SEGUNDO APELLIDO")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]*$", ErrorMessage = "No se pueden ingresar caracteres especiales")]
        [MaxLength(100, ErrorMessage = "El segundo apellido del usuario no puede ser mayor a 100 caracteres")]
        public string SegundoApellido { get; set; }

        [Display(Name = "CORREO")]
        [Required(ErrorMessage = "El correo del usuario es requerido")]
        [MaxLength(100, ErrorMessage = "El correo del usuario no puede ser mayor a 100 caracteres")]
        [RegularExpression(@"^[\d\w\._\-]+@([\d\w\._\-]+\.)+[\w]+$", ErrorMessage = "Correo electrónico no es válido")]
        public string Correo { get; set; }

        public string Foto { get; set; }

        public string Firma { get; set; }

        // agregado 03/04/2022
        public IList<Rol> Roles { get; set; } = new List<Rol>();
    }
}
