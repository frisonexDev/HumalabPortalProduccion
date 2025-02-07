using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Seguridad.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace GDifare.Portal.Humalab.Seguridad.Operation
{
    public class RolOperation
    {
        private readonly Communicator CommunicatorRol;

        //Avalab
        private string ServerAvalab;
        private int PortAvalab;
        private string UserAvalab;
        private string PassAvalab;


        public RolOperation(string Server, int PortSeguridad, string Token, string user, string pass)
        {
            CommunicatorRol = new Communicator(Server, PortSeguridad, Routes.PathServicesSeguridadRol, Token);

            //Avalab
            ServerAvalab = Server;
            PortAvalab = PortSeguridad;
            UserAvalab = user;
            PassAvalab = pass;
        }

        public List<Rol> ConsultarRol(int rolID = 0, string rol = null)
        {
            //var metodo = "consultar";
            //if (rolID > 0 || !string.IsNullOrWhiteSpace(rol))
            //    metodo += "?";
            //if (rolID > 0)
            //    metodo += string.Format("idRol={0}", HttpUtility.UrlEncode(rolID.ToString()));
            //if (!string.IsNullOrWhiteSpace(rol))
            //    metodo += string.Format("Rol{0}", HttpUtility.UrlEncode(rol));
            //var lista = CommunicatorRol.InvokeOperation<List<Rol>>(metodo, TipoOperacion.GET);
            //return lista;

            //Avalab
            var metodo = "";

            if (rolID > 0 || !string.IsNullOrWhiteSpace(rol))
                metodo += "?";
            if (rolID > 0)
                metodo += string.Format("idRol={0}", HttpUtility.UrlEncode(rolID.ToString()));
            if (!string.IsNullOrWhiteSpace(rol))
                metodo += string.Format("Rol{0}", HttpUtility.UrlEncode(rol));

            var url = ServerAvalab + ":" + PortAvalab + "/" + Routes.PathServicesSeguridadRol + metodo;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string username = UserAvalab;
            string password = PassAvalab;

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<Rol>>(responseText)!;
                }
            }
        }

        public ResponseMenssage ModificarRol(ModificarRolRequest rol)
        {

            var metodo = "modificar";
            return CommunicatorRol.InvokeOperation<ResponseMenssage, ModificarRolRequest>(metodo, TipoOperacion.PUT, rol);

        }
        public ResponseMenssage AgregarRol(CrearRolRequest rol)
        {
            var metodo = "crear";
            return CommunicatorRol.InvokeOperation<ResponseMenssage, CrearRolRequest>(metodo, TipoOperacion.POST, rol);

        }
        public ResponseMenssage EliminarRol(InactivarRolRequest rol)
        {
            var metodo = "anular";
            return CommunicatorRol.InvokeOperation<ResponseMenssage, InactivarRolRequest>(metodo, TipoOperacion.PUT, rol);

        }

        public ResponseMenssage GrabarRolPerfil(RolPerfil rolPerfil)
        {
            var metodo = "asignarPerfil";
            return CommunicatorRol.InvokeOperation<ResponseMenssage, RolPerfil>(metodo, TipoOperacion.POST, rolPerfil);

        }

        public ResponseMenssage EliminarRolPerfil(RolPerfil rolPerfil)
        {
            var metodo = "inactivarPerfil";
            return CommunicatorRol.InvokeOperation<ResponseMenssage, RolPerfil>(metodo, TipoOperacion.PUT, rolPerfil);

        }
    }
}
