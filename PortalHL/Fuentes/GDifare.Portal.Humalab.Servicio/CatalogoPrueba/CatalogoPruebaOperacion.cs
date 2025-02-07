using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.CatalogoPrueba
{
    public class CatalogoPruebaOperacion
    {       
        private string serverGalileo;
        private int portGalileo;
        private string tokenGalileo;
        private string catalogoPrueba;

        private string serverCabeceraResultados;
        private string pathCabeceraResultados;

        private string serverPacResultados;
        private string pathPacResultados;

        private string serverMedResultados;
        private string pathMedResultados;

        private string serverPruebResultados;
        private string pathPruebResultados;

        //Avalab
        private string UserAvalab;
        private string PassAvalab;

        public CatalogoPruebaOperacion(AppServicioMicrosExternos configurationApisExt, string user, string pass)
        {          
            serverGalileo = configurationApisExt.ServerGalileoCatalogoPruebas;
            portGalileo = configurationApisExt.PortGalileoCatalogoPruebas; 
            tokenGalileo = configurationApisExt.TokenGalileoCatalogoPruebas;
            catalogoPrueba = configurationApisExt.PathServicioCatologoPruebas;

            //orden resultado pdf
            serverCabeceraResultados = configurationApisExt.ServerOrdenCabecera;
            pathCabeceraResultados = configurationApisExt.PathOrdenCabecera;

            //paciente resultado pdf
            serverPacResultados = configurationApisExt.ServerPacResult;
            pathPacResultados = configurationApisExt.PathPacResult;

            //medico resultado pdf
            serverMedResultados = configurationApisExt.ServerMedResult;
            pathMedResultados = configurationApisExt.PathMedResult;

            //pruebas resultado pdf
            serverPruebResultados = configurationApisExt.ServerPrueResult;
            pathPruebResultados = configurationApisExt.PathPruebResult;

            //Avalab
            UserAvalab = user;
            PassAvalab = pass;
        }

        public async Task <string> ListaPruebas()
        {
            var jsonResult = "";

            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        var metodo = "";
            //        client.Timeout = TimeSpan.FromMinutes(2); //espera la solicitud 2 minutos 

            //        if (!string.IsNullOrWhiteSpace(tokenGalileo))
            //            metodo += string.Format("&token={0}", HttpUtility.UrlEncode(tokenGalileo.ToString()));

            //        //if (!string.IsNullOrWhiteSpace(tokenGalileo))
            //        //     client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenGalileo}");

            //        var result = await client.GetAsync("http://" + serverGalileo + ":" + portGalileo + catalogoPrueba + metodo);
            //        //var result = await client.GetAsync("http://" + serverGalileo + Routes.PathServicioCatologoPruebas + portGalileo); //de prueba

            //        if (!result.IsSuccessStatusCode)
            //        {
            //            throw new ArgumentException("something bad happended");
            //        }

            //        jsonResult = await result.Content.ReadAsStringAsync();                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return "01";
            //}

            //Avalab

            try {
                var metodo = "";

                var url = serverGalileo + ":" + portGalileo + "/" + catalogoPrueba + metodo;
                
                using var httpClient = new HttpClient();
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
                        jsonResult = responseText;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return jsonResult;
        }

        //cabecera PDF resultados orden
        public async Task<CabecOrdenResul> LisCabeceraResultado(string CodigoBarra, int idEmpresa)
        {
            var metodo = "consultar";
            CabecOrdenResul respuesta = new();

            try
            {                
                if (!string.IsNullOrWhiteSpace(CodigoBarra))
                    metodo += string.Format("?CodigoOrden={0}", HttpUtility.UrlEncode(CodigoBarra));

                if (idEmpresa > 0)
                    metodo += string.Format("&IdLaboratorio={0}", HttpUtility.UrlEncode(idEmpresa.ToString()));

                if (!string.IsNullOrWhiteSpace(tokenGalileo))
                    metodo += string.Format("&token={0}", HttpUtility.UrlEncode(tokenGalileo.ToString()));

                using (var clienteF = new HttpClient())
                {
                    var resultado = await clienteF.GetAsync("http://" + serverCabeceraResultados + ":" + portGalileo + pathCabeceraResultados + metodo);
                    //var resultado = await clienteF.GetAsync("http://" + serverCabeceraResultados + pathCabeceraResultados + metodo);//de prueba

                    if (!resultado.IsSuccessStatusCode)
                        throw new ArgumentException("Problemas con el servicio de Lis.");

                    var resultadoJson = await resultado.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<CabecOrdenResul>(resultadoJson)!;
                }
            }
            catch (Exception ex)
            {
                respuesta.message = "Error";
                respuesta.status = "01";
            }

            return respuesta;
        }

        //paciente PDF resultados
        public async Task<List<PacienteResultPdf>> LisPacienteResultado(int idPaciente, int idEmpresa)
        {
            var metodo = "consultar";
            List<PacienteResultPdf> respuesta = new();

            try
            {
                if (idPaciente > 0)
                    metodo += string.Format("?IdPaciente={0}", HttpUtility.UrlEncode(idPaciente.ToString()));

                if (idEmpresa > 0)
                    metodo += string.Format("&IdLaboratorio={0}", HttpUtility.UrlEncode(idEmpresa.ToString()));
                
                if (!string.IsNullOrWhiteSpace(tokenGalileo))
                    metodo += string.Format("&token={0}", HttpUtility.UrlEncode(tokenGalileo.ToString()));

                using (var clienteF = new HttpClient())
                {
                    var resultado = await clienteF.GetAsync("http://" + serverPacResultados + ":" + portGalileo + pathPacResultados + metodo);
                    //var resultado = await clienteF.GetAsync("http://" + serverPacResultados + pathPacResultados + metodo);//de prueba

                    if (!resultado.IsSuccessStatusCode)
                        throw new ArgumentException("Problemas con el servicio de Lis.");

                    var resultadoJson = await resultado.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<List<PacienteResultPdf>>(resultadoJson)!;
                }
            }
            catch (Exception ex)
            {
                //respuesta.mensaje = "Error";                
            }

            return respuesta;
        }

        //medicos PDF resultados
        public async Task<MedicoResultPdf> LisMedicoResultado(int idMedico, int idEmpresa)
        {
            var metodo = "consultar";
            MedicoResultPdf respuesta = new();

            try
            {
                
                if (idEmpresa > 0)
                    metodo += string.Format("?IdLaboratorio={0}", HttpUtility.UrlEncode(idEmpresa.ToString()));

                if (idMedico > 0)
                    metodo += string.Format("&IdMedico={0}", HttpUtility.UrlEncode(idMedico.ToString()));

                if (!string.IsNullOrWhiteSpace(tokenGalileo))
                    metodo += string.Format("&token={0}", HttpUtility.UrlEncode(tokenGalileo.ToString()));

                using (var clienteF = new HttpClient())
                {
                    var resultado = await clienteF.GetAsync("http://" + serverMedResultados + ":" + portGalileo + pathMedResultados + metodo);
                    //var resultado = await clienteF.GetAsync("http://" + serverMedResultados + pathMedResultados + metodo);//de prueba

                    if (!resultado.IsSuccessStatusCode)
                        throw new ArgumentException("Problemas con el servicio de Lis.");

                    var resultadoJson = await resultado.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<MedicoResultPdf>(resultadoJson)!;
                }

            }
            catch (Exception ex)
            {

            }

            return respuesta;
        }

        //pruebas pdf resultados
        public async Task<List<PruebasResultPdf>> LisPruebasResultado(string codBarra, int idEmpresa)
        {
            var metodo = "listarResultados";
            List<PruebasResultPdf> respuesta = new();

            try
            {
                if (idEmpresa > 0)
                    metodo += string.Format("?IdLaboratorio={0}", HttpUtility.UrlEncode(idEmpresa.ToString()));

                if (!string.IsNullOrWhiteSpace(codBarra))
                    metodo += string.Format("&CodigoOrden={0}", HttpUtility.UrlEncode(codBarra));

                if (!string.IsNullOrWhiteSpace(tokenGalileo))
                    metodo += string.Format("&token={0}", HttpUtility.UrlEncode(tokenGalileo.ToString()));

                using (var clienteF = new HttpClient())
                {
                    var resultado = await clienteF.GetAsync("http://" + serverPruebResultados + ":" + portGalileo + pathPruebResultados + metodo);
                    //var resultado = await clienteF.GetAsync("http://" + serverPruebResultados + pathPruebResultados + metodo);//de prueba

                    if (!resultado.IsSuccessStatusCode)
                        throw new ArgumentException("Problemas con el servicio de Lis.");

                    var resultadoJson = await resultado.Content.ReadAsStringAsync();
                    respuesta = JsonConvert.DeserializeObject<List<PruebasResultPdf>>(resultadoJson)!;
                }

            }
            catch (Exception ex)
            {

            }

            return respuesta;
        }
    }
}
