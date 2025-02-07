using System;

namespace GDifare.Portales.HumaLab.Models
{
    public partial class AppSettings
    {
        public string ServerSeguridad { get; set; }
        public int PortSeguridad { get; set; }


        public string ServerLIMS { get; set; }
        public int PortLIMS { get; set; }

        public string Token { get; set; }

        /// <summary>
        ///  Microservicios propios de HUMALAB 
        /// </summary>
        public string ServerHUMALAB { get; set; } = string.Empty;   
        public int PortHUMALAB { get; set; }
		public string TokenHUMALAB { get; set; } = string.Empty;

        //Servicios Avalab
        public string UserAvalab { get; set; } = string.Empty;
        public string PassAvalab { set; get; } = string.Empty;
    }
}