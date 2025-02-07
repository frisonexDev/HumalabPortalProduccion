using GDifare.Portal.EstadoCartera.Modelos;
using GDifare.Portal.EstadoCartera.Resources;
using GDifare.Portales.Comunicaciones;
using GDifare.Portal.Humalab.Seguridad.Modelos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GDifare.Portal.EstadoCartera.Utils;
using Newtonsoft.Json;

namespace GDifare.Portal.EstadoCartera.Operation
{
    public class ClienteOperation
    {
        private readonly Communicator CommunicatorHumaLab;
        private string serverFlexLine;
        private int portFlexLine;
        private string tokenFlexLine;
        public ClienteOperation(string Server, int Port , string token, string ServerFlexLine,int PortFlexLine,string TokenFlexLine)
        {
            //Microservicio Humalab
            CommunicatorHumaLab = new Communicator(Server, Port, Routes.PathServicesCarteraEstado, token);
            serverFlexLine = ServerFlexLine;
            portFlexLine = PortFlexLine;
            tokenFlexLine = TokenFlexLine;
        }
        
        public string ValidarCartera(int idRol, string ruc)
        {
           
           string estadoCliente = string.Empty; 
           bool cartera = ConsultarRolCliente(ruc);
            if (cartera)
            {
                var estadoHumalab = ConsultaEstadoHumalab(ruc);               
                var estadoFlexline = ConsultaEstadoFlexline(ruc);
                 
                        switch ((estadoHumalab, estadoFlexline))
                        {
                            case (StringHandler.ACTIVO, StringHandler.ACTIVO):
                                estadoCliente = StringHandler.ACTIVO;
                                break;
                            case (StringHandler.TEMPORAL, StringHandler.ACTIVO):
                                 ActualizaEstadoHumalab(ruc, StringHandler.ACTIVO);
                                estadoCliente = StringHandler.ACTIVO;
                                break;
                            case (StringHandler.SUSPENDIDO, StringHandler.ACTIVO):
                                ActualizaEstadoHumalab(ruc, StringHandler.ACTIVO);
                                estadoCliente = StringHandler.ACTIVO;
                                break;

                            case (StringHandler.ACTIVO, StringHandler.SUSPENDIDO):
                                ActualizaEstadoHumalab(ruc, StringHandler.SUSPENDIDO);
                                estadoCliente = StringHandler.SUSPENDIDO;
                                break;
                            case (StringHandler.SUSPENDIDO, StringHandler.SUSPENDIDO):
                                 estadoCliente = StringHandler.SUSPENDIDO;
                                break;
                            case (StringHandler.TEMPORAL, StringHandler.SUSPENDIDO):
                                estadoCliente = StringHandler.TEMPORAL;
                                break;
                        }                
            }
            else
            {
                estadoCliente = StringHandler.ACTIVO;
            }
            
            return estadoCliente;
        }
      
        public bool ConsultarRolCliente(string ruc)
        {
            try
            {
                var metodo = "existeCliente?";
                if (!string.IsNullOrWhiteSpace(ruc))
                    metodo += string.Format("ruc={0}", HttpUtility.UrlEncode(ruc));

                bool responseMenssage = CommunicatorHumaLab.InvokeOperation<bool>(metodo, TipoOperacion.GET);
                
                return responseMenssage;

            }
            catch (Exception e)
            {
                return false;
            }

        }
      
        public string ConsultaEstadoHumalab(string ruc)
        {
            try
            {
                var metodo = "consultarEstadoHumalab?";
                if (!string.IsNullOrWhiteSpace(ruc))
                    metodo += string.Format("ruc={0}", HttpUtility.UrlEncode(ruc));


                ClienteHumalab responseMenssage = CommunicatorHumaLab.InvokeOperation<ClienteHumalab>(metodo, TipoOperacion.GET);
                if (responseMenssage == null || responseMenssage.Estadocliente == null)
                {
                    return StringHandler.SUSPENDIDO;
                }
                else
                {
                    return responseMenssage.Estadocliente;
                }
            }
            catch (Exception)
            {

                return StringHandler.SUSPENDIDO;
            }                       
        }

        public int ActualizaEstadoHumalab(string ruc,string estado)
        {
            try
            {
                var metodo = "actualizaEstado?";
                if (!string.IsNullOrWhiteSpace(ruc))
                    metodo += string.Format("ruc={0}", HttpUtility.UrlEncode(ruc));
                if (!string.IsNullOrWhiteSpace(estado))
                    metodo += string.Format("&estado={0}", HttpUtility.UrlEncode(estado));


                int responseMenssage = CommunicatorHumaLab.InvokeOperation<int>(metodo, TipoOperacion.GET);
                return responseMenssage;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public string ConsultaEstadoFlexline(string ruc)
        {
            try
            {
                Task<ClienteFlexline> responseMenssage = comunicatorFlexLine(ruc);
                if (responseMenssage.Result == null)
                {
                    return StringHandler.SUSPENDIDO;
                }
                else
                {
                    return responseMenssage.Result.EstadoCartera == true ? StringHandler.ACTIVO : StringHandler.SUSPENDIDO;
                }
            }
            catch (Exception ex)
            {
                return StringHandler.SUSPENDIDO;
            }
        }

        private async Task<ClienteFlexline> comunicatorFlexLine(string ruc)
        {
            var metodo = "cartera?";
            if (!string.IsNullOrWhiteSpace(ruc))
                metodo += string.Format("ruc={0}", HttpUtility.UrlEncode(ruc));

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenFlexLine}");

                var result = await client.GetAsync("http://" + serverFlexLine + ":" + portFlexLine + Routes.PathServicesCarteraFlexLine + "/" + metodo);
                if (!result.IsSuccessStatusCode)
                {
                    throw new ArgumentException("something bad happended");
                }

                var jsonResult = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ClienteFlexline>(jsonResult);
                return response;
            }
        }

    }
}
