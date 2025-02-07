

using System.Collections.Generic;

namespace GDifare.Portal.HumaLab.UI.Models
{ 
    public class MenuItem
    {
        public int OpcionID { get; set; }

        public int AplicacionID { get; set; }

        public int OpcionPadreID { get; set; }

        public string Descripcion { get; set; }

        public string Observacion { get; set; }

        public string Url { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public string IconEnable { get; set; }

        public string IconDisable { get; set; }

        public string Modulo { get; set; }

        public string Acciones { get; set; }

        public int IdRol { get; set; }

        public string IdRolGrupo { get; set; }

        public List<MenuItem> Items { get; set; }
    }
}
