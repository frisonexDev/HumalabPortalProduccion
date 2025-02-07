using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Modelos.Paciente;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GDifare.Portal.Humalab.Servicio.Cliente
{
    public class GestionPaciente
    {

        private readonly Communicator CommunicatorGestionPaciente;

        private string ServerPacienteCliente;
        private int PortPacienteCliente;
        private string RoutePacienteCliente;       

        public GestionPaciente(AppServicioClienteApi configuracion)
        {
            CommunicatorGestionPaciente = new Communicator(configuracion.Server, configuracion.Port, configuracion.RoutePaciente, configuracion.Token);

            ServerPacienteCliente = configuracion.Server;
            PortPacienteCliente = configuracion.Port;
            RoutePacienteCliente = configuracion.RoutePaciente;
        }


        public Pacientes ConsultarPaciente(ConsultarPaciente valor)
        {
            try
            {
                Pacientes datos =  new Pacientes();
                object objPaciente;

				var stringBuilder = new StringBuilder();
                stringBuilder.Append("/consultarpaciente?");
                stringBuilder.Append("Identificacion="+valor.Identificacion+"&");
                stringBuilder.Append("UsuarioCreacion="+valor.UsuarioCreacion);
                var queryString = stringBuilder.ToString();


				var url = ServerPacienteCliente + ":" + PortPacienteCliente + "/" + RoutePacienteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						objPaciente = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object objPaciente = CommunicatorGestionPaciente.InvokeOperation<object>(queryString, TipoOperacion.GET);
                datos = JsonConvert.DeserializeObject<Pacientes>(objPaciente.ToString()!)!;
                return datos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /*
        public DatosResponse RegistrarPaciente(Paciente paciente)
        {
            var result = new DatosResponse();
            try
            {
                string ruta = "/grabar?";
                
                result=CommunicatorGestionPaciente.InvokeOperation<DatosResponse,Paciente>(ruta, TipoOperacion.POST,paciente);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }


        */

    }
}
