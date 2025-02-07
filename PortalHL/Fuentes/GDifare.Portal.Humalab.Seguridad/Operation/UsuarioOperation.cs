using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Seguridad.Resources;
using GDifare.Portal.Humalab.Seguridad.Utils;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;

namespace GDifare.Portal.Humalab.Seguridad.Operation
{
    public class UsuarioOperation
    {
        private readonly Communicator CommunicatorUsuario;

        //Avalab
        public string ServerAvalab;
        public int portAvabalab;
        public string UserAvalab;
        public string PassAvalab;

        public UsuarioOperation(string server, int port, string Token, string user, string pass)
        {
            CommunicatorUsuario = new Communicator(server, port, Routes.PathServicesSeguridadUsuario, Token);

            //AVALAB
            ServerAvalab = server;
            portAvabalab = port;
            UserAvalab = user;
            PassAvalab = pass;
        }                

        public bool Autenticar(CredencialesRequest request)
        {
            //var metodo = "autenticar";
            var metodo = "";
            var login = true;

            //var desen = EncriptadorProvider.DesencriptarValor(request.Clave);
            //request.Clave = EncriptadorProvider.EncriptarValor(request.Clave);

            //Servicios Avalab
            metodo += string.Format("?nombreUsuario={0}", HttpUtility.UrlEncode(request.Usuario));

            var url = ServerAvalab + ":" + portAvabalab + "/" + Routes.PathServicesSeguridadUsuario + metodo;
            var requestNew = (HttpWebRequest)WebRequest.Create(url);
            requestNew.Method = "PUT";
            requestNew.ContentType = "application/json";

            string username = UserAvalab;
            string password = PassAvalab;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            requestNew.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    
                    if (string.IsNullOrEmpty(responseText) || responseText.Trim() == "{}")
                    {
                        login = false;
                    }
                    else
                    {
                        login = true;
                    }
                }
            }

           //return CommunicatorUsuario.InvokeOperation<bool, CredencialesRequest>(metodo, TipoOperacion.POST, request);
            return login;
        }

        public int RolUsuario(CredencialesRequest request)
        {            
            var metodo = "";
            int login = 0;

            //Servicios Avalab
            metodo += string.Format("?nombreUsuario={0}", HttpUtility.UrlEncode(request.Usuario));

            var url = ServerAvalab + ":" + portAvabalab + "/" + Routes.PathServicesSeguridadUsuario + metodo;
            var requestNew = (HttpWebRequest)WebRequest.Create(url);
            requestNew.Method = "PUT";
            requestNew.ContentType = "application/json";

            string username = UserAvalab;
            string password = PassAvalab;
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            requestNew.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(responseText) || responseText.Trim() == "{}")
                    {
                        login = 0;
                    }
                    else
                    {                        
                        var jsonElement = JsonDocument.Parse(responseText).RootElement;

                        if (jsonElement.ValueKind == JsonValueKind.Object)
                        {
                            // Caso: JSON raíz es un objeto
                            if (jsonElement.TryGetProperty("rolID", out var rolIdElement))
                            {
                                if (rolIdElement.ValueKind == JsonValueKind.Number)
                                {
                                    login = rolIdElement.GetInt32();
                                }
                                else if (rolIdElement.ValueKind == JsonValueKind.Array && rolIdElement.GetArrayLength() > 0)
                                {
                                    login = rolIdElement[0].GetInt32(); // Tomar el primer elemento del arreglo
                                }
                            }
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.Array)
                        {
                            // Caso: JSON raíz es un arreglo
                            foreach (var item in jsonElement.EnumerateArray())
                            {
                                if (item.TryGetProperty("rolID", out var rolIdElement))
                                {
                                    if (rolIdElement.ValueKind == JsonValueKind.Number)
                                    {
                                        login = rolIdElement.GetInt32();
                                    }
                                    else if (rolIdElement.ValueKind == JsonValueKind.Array && rolIdElement.GetArrayLength() > 0)
                                    {
                                        login = rolIdElement[0].GetInt32();
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            login = 0; // Caso inesperado: no es ni objeto ni arreglo
                        }                                          
                    }
                }
            }

            //return CommunicatorUsuario.InvokeOperation<bool, CredencialesRequest>(metodo, TipoOperacion.POST, request);
            return login;
        }

        public List<ObjUsuario> ObtenerUsuario(string nombreUsuario)
        {
            //var metodo = "consultar/";
            //metodo += string.Format("{0}", HttpUtility.UrlEncode(nombreUsuario));
            //return CommunicatorUsuario.InvokeOperation<ObjUsuario>(metodo, TipoOperacion.GET);

            //Avalab
            var metodo = "";
            metodo += string.Format("?nombreUsuario={0}", HttpUtility.UrlEncode(nombreUsuario));

            var url = ServerAvalab + ":" + portAvabalab + "/" + Routes.PathServicesSeguridadUsuario + metodo;
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
                    //return JsonConvert.DeserializeObject<List<ObjUsuario>>(responseText)!;
                    try
                    {
                        // Intentar deserializar como lista
                        var usuarios = JsonConvert.DeserializeObject<List<ObjUsuario>>(responseText);

                        if (usuarios != null)
                        {
                            return usuarios;
                        }
                    }
                    catch (JsonSerializationException)
                    {                        
                    }

                    try
                    {
                        // Intentar deserializar como objeto
                        var usuario = JsonConvert.DeserializeObject<ObjUsuario>(responseText);

                        if (usuario != null)
                        {                            
                            return new List<ObjUsuario> { usuario };
                        }
                    }
                    catch (JsonSerializationException ex)
                    {                        
                        throw new Exception("La respuesta no contiene un usuario válido.", ex);
                    }

                    throw new Exception("No se pudo deserializar la respuesta.");
                }
            }
        }

        public List<ObjUsuario> ConsultarUsuario(int UsuarioID = 0, int EmpresaID = 0, string usuario = null, string Nombre = null, string PrimerApellido = null, string SegundoApellido = null, int EstadoID = 0)
        {

            // MLOPEZ: A este método se le modificó al momento de construir el string de parámetros.
            // antes: listar?UsuarioID=3EmpresaID=2... etc
            // ahora (se le agregó el & para dividir los parámetros): listar?UsuarioID=3&EmpresaID=2&... etc

            var metodo = "listar";

            if (UsuarioID > 0 || EmpresaID > 0 || !string.IsNullOrWhiteSpace(usuario) || !string.IsNullOrWhiteSpace(Nombre)
                || !string.IsNullOrWhiteSpace(PrimerApellido) || !string.IsNullOrWhiteSpace(SegundoApellido) || EstadoID > 0)
                metodo += "?";

            if (UsuarioID > 0)
                metodo += string.Format("&UsuarioID={0}", HttpUtility.UrlEncode(UsuarioID.ToString()));

            if (EmpresaID > 0)
                metodo += string.Format("&EmpresaID={0}", HttpUtility.UrlEncode(EmpresaID.ToString()));

            if (!string.IsNullOrWhiteSpace(usuario))
                metodo += string.Format("&usuario={0}", HttpUtility.UrlEncode(usuario));

            if (!string.IsNullOrWhiteSpace(Nombre))
                metodo += string.Format("&Nombre={0}", HttpUtility.UrlEncode(Nombre));

            if (!string.IsNullOrWhiteSpace(PrimerApellido))
                metodo += string.Format("&PrimerApellido={0}", HttpUtility.UrlEncode(PrimerApellido));

            if (!string.IsNullOrWhiteSpace(SegundoApellido))
                metodo += string.Format("&SegundoApellido={0}", HttpUtility.UrlEncode(SegundoApellido));

            if (EstadoID > 0)
                metodo += string.Format("&Estado={0}", HttpUtility.UrlEncode(EstadoID.ToString()));

            metodo = metodo.Replace("?&","?");
            var lista = CommunicatorUsuario.InvokeOperation<List<ObjUsuario>>(metodo, TipoOperacion.GET);
            foreach(var item in lista)
            { 
                item.Clave = EncriptadorProvider.DesencriptarValor(item.Clave);
            }
            return lista;
        }
        public ResponseMenssage ModificarUsuario(ModificarUsuarioRequest usuario)
        {
            var metodo = "modificar";
            usuario.Clave = EncriptadorProvider.EncriptarValor(usuario.Clave);
            return CommunicatorUsuario.InvokeOperation<ResponseMenssage, ModificarUsuarioRequest>(metodo, TipoOperacion.PUT, usuario);

        }

        public ResponseMenssage AgregarUsuario(CrearUsuarioRequest usuario)
        {
            var metodo = "crear";
            usuario.Clave = EncriptadorProvider.EncriptarValor(usuario.Clave);
            return CommunicatorUsuario.InvokeOperation<ResponseMenssage, CrearUsuarioRequest>(metodo, TipoOperacion.POST, usuario);
        }

        public ResponseMenssage EliminarUsuario(InactivarUsuarioRequest usuario)
        {
            var metodo = "desactivar";
            return CommunicatorUsuario.InvokeOperation<ResponseMenssage, InactivarUsuarioRequest>(metodo, TipoOperacion.PUT, usuario);
        }
        public ResponseMenssage OlvideContrasena(RecuperarContrasenaRequest usuario)
        {
            var metodo = "recuperarcontrasena";
            return CommunicatorUsuario.InvokeOperation<ResponseMenssage, RecuperarContrasenaRequest>(metodo, TipoOperacion.PUT, usuario);
        }
    }
}
