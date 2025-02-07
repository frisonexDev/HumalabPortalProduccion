using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Seguridad.Modelos.Perfil;
using GDifare.Portal.Humalab.Seguridad.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace GDifare.Portal.Humalab.Seguridad.Operation
{
    public class PerfilOperation
    {
        private readonly Communicator CommunicatorPerfil;
        private readonly Communicator ComunicatorLab;

        //Avalab
        private string ServerAvalab;
        private int PortAvalab;
        private string UserAvalab;
        private string PassAvalab;

        public PerfilOperation(string Server, int PortSeguridad, string Token,
                               string user, string pass)
        {
            CommunicatorPerfil = new Communicator(Server, PortSeguridad, Routes.PathServicesSeguridadPerfil, Token);
            ComunicatorLab = new Communicator(Server, PortSeguridad, Routes.PathServicesLaboratorio, Token);
            //ComunicatorLab = new Communicator("atlsconflabor.sup.apps.ocp4mqa.grupodifare.com", PortSeguridad, Routes.PathServicesLaboratorio, Token); //de prueba

            //Avalab
            ServerAvalab = Server;
            PortAvalab = PortSeguridad;
            UserAvalab = user;
            PassAvalab = pass;
        }

        public List<Perfiles> ConsultarPerfil(int perfilid = 0, string perfil = null, int cargarTodo = 1)
        {
            var metodo = "consultar";
            if (perfilid > 0 || !string.IsNullOrWhiteSpace(perfil))
                metodo += "?";

            if (perfilid > 0)
                metodo += string.Format("perfilID={0}", HttpUtility.UrlEncode(perfilid.ToString()));

            if (!string.IsNullOrWhiteSpace(perfil))
            {
                string pregijoSigno = metodo.Contains("?") ? "&" : "?";
                metodo += pregijoSigno + string.Format("&perfil={0}", HttpUtility.UrlEncode(perfil));
            }

            // Cambio para evitar que se cargue todo
            if (cargarTodo == 0)
            {
                string pregijoSigno = metodo.Contains("?") ? "&" : "?";
                metodo += pregijoSigno + string.Format("cargarTodo={0}", HttpUtility.UrlEncode(cargarTodo.ToString()));
            }

            var lista = CommunicatorPerfil.InvokeOperation<List<Perfiles>>(metodo, TipoOperacion.GET);
            return lista;

        }


        public ResponseMenssage ModificarPefil(ModificarPerfilRequest request)
        {

            var metodo = "modificar";
            return CommunicatorPerfil.InvokeOperation<ResponseMenssage, ModificarPerfilRequest>(metodo, TipoOperacion.PUT, request);

        }

        public ResponseMenssage AgregarPerfil(GrabarPerfilRequest request)
        {
            var metodo = "crear";
            return CommunicatorPerfil.InvokeOperation<ResponseMenssage, GrabarPerfilRequest>(metodo, TipoOperacion.POST, request);
        }

        public ResponseMenssage EliminarPerfil(InactivarPerfilRequest request)
        {
            var metodo = "anular";
            return CommunicatorPerfil.InvokeOperation<ResponseMenssage, InactivarPerfilRequest>(metodo, TipoOperacion.PUT, request);

        }

        public ResponseMenssage AgregarPerfilOpcion(GrabarPerfilOpcionRequest request)
        {
            var metodo = "asignarOpcion";
            return CommunicatorPerfil.InvokeOperation<ResponseMenssage, GrabarPerfilOpcionRequest>(metodo, TipoOperacion.POST, request);
        }

        public List<PerfilOpcion> ConsultarPerfilOpcion(int PerfilID, int OpcionID)
        {
            var metodo = "consultarOpcion?";
            if (PerfilID > 0)
                metodo += string.Format("PerfilID={0}", HttpUtility.UrlEncode(PerfilID.ToString()));

            if (OpcionID > 0)
                metodo += string.Format("&OpcionID={0}", HttpUtility.UrlEncode(OpcionID.ToString()));

            var lista = CommunicatorPerfil.InvokeOperation<List<PerfilOpcion>>(metodo, TipoOperacion.GET);
            return lista;
        }

        public ResponseMenssage InactivarPerfilOpcion(InactivarPerfilOpcionRequest request)
        {
            var metodo = "inactivarOpcion";
            return CommunicatorPerfil.InvokeOperation<ResponseMenssage, InactivarPerfilOpcionRequest>(metodo, TipoOperacion.PUT, request);
        }

        //public ResponseMenssage AgregarPerfilOpcionAccion(GrabarOpcionPerfilAccionRequest request)
        //{
        //    var metodo = "asignarOpcionAccion";
        //    return CommunicatorPerfil.InvokeOperation<ResponseMenssage, GrabarOpcionPerfilAccionRequest>(metodo, TipoOperacion.POST, request);
        //}

        //public List<OpcionPerfilAccion> ConsultarOpcionPerfilAccion(int PerfilOpcionID = 0)
        //{
        //    var metodo = "consultarOpcionAccion";
        //    if (PerfilOpcionID > 0)
        //        metodo += "?";

        //    if (PerfilOpcionID > 0)
        //        metodo += string.Format("PerfilOpcionID={0}", HttpUtility.UrlEncode(PerfilOpcionID.ToString()));

        //    var lista = CommunicatorPerfil.InvokeOperation<List<OpcionPerfilAccion>>(metodo, TipoOperacion.GET);
        //    return lista;

        //}

        //public ResponseMenssage InactivarPerfilOpcionAccion(InactivarOpcionPerfilAccionRequest request)
        //{
        //    var metodo = "inactivarOpcionAccion";
        //    return CommunicatorPerfil.InvokeOperation<ResponseMenssage, InactivarOpcionPerfilAccionRequest>(metodo, TipoOperacion.PUT, request);
        //}

        public List<OpcionAccion> ConsultarOpcionAccion(int opcionID = 0)
        {
            var metodo = "consultarCategoriaOpcionAccion";

            if (opcionID > 0)
                metodo += "?" + string.Format("OpcionID={0}", HttpUtility.UrlEncode(opcionID.ToString()));

            var lista = CommunicatorPerfil.InvokeOperation<List<OpcionAccion>>(metodo, TipoOperacion.GET);

            return lista;
        }

        public object ConsultarLaboratorio(int rolId)
        {            
            object objLab;
            //try
            //{
            //    var metodo = "consultar";

            //    if (rolId > 0)
            //        metodo += "?" + string.Format("HomologacionEmpresa={0}", HttpUtility.UrlEncode(rolId.ToString()));

            //    objLab = ComunicatorLab.InvokeOperation<object>(metodo, TipoOperacion.GET);
            //}
            //catch (Exception ex)
            //{
            //    return ex;
            //}

            //Avalab

            try {
                var metodo = "";

                if (rolId >= 0)
                    metodo += "?" + string.Format("HomologacionEmpresa={0}", HttpUtility.UrlEncode(rolId.ToString()));

                var url = ServerAvalab + ":" + PortAvalab + "/" + Routes.PathServicesLaboratorio + metodo;
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
                        objLab = JsonConvert.DeserializeObject<object>(responseText)!;
                    }
                }

            }
            catch (Exception ex)
            {
                return ex;
            }

            
            return objLab;
        }
    }
}
