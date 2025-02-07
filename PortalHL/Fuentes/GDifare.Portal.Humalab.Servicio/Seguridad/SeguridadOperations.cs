using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Cliente;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.Seguridad
{
    public class SeguridadOperations
    {
        private readonly Communicator CommunicatorSeguridad;
        
        public string ServerHL;
        public int portHL;

        public SeguridadOperations(string server, int port, string token)
        {
            CommunicatorSeguridad = new Communicator(server, port, Routes.PathServicioCliente, token);
            
            ServerHL = server;
            portHL = port;
        }

        public bool ConsultaCliente(string cliente)
        {
            ClienteResponse? infocliente = null;
            bool existeCliente = false;

            try
            {
                var metodo = "consultarClienteHumalab";

                if (!string.IsNullOrWhiteSpace(cliente))
                {
                    metodo += string.Format("?Ruc={0}", HttpUtility.UrlEncode(cliente));
                }

                var url = ServerHL + ":" + portHL + "/" + Routes.PathServicioCliente + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();

                        try
                        {
                            var usuario = JsonConvert.DeserializeObject<ClienteResponse>(responseText);                            
                            if (usuario != null)
                            {
                                existeCliente = true;
                            }
                            else
                            {
                                existeCliente = false;
                            }
                        }
                        catch(Exception ex)
                        {
                            existeCliente = false;
                        }
                    }
                }

                //infocliente = CommunicatorSeguridad.InvokeOperation<ClienteResponse>(metodo, TipoOperacion.GET);

                //if(infocliente!=null) 
                //    existeCliente = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return existeCliente!;
        }
    }
}
