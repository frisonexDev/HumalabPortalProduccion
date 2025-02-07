using GDifare.Portal.Humalab.Servicio.Modelos.CatalogoPruebas;
using GDifare.Portal.Humalab.Servicio.Modelos.Cliente;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.OperacionesCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.ClienteOperacion;

public class ClienteOperationNew
{
    private string serverFlexLine;
    private int portFlexLine;
    private string tokenFlexLine;

    private string ServerAdmin;
    private int PortAdmin;
    private string RouteAdmin;
    private string RouteFlexCartera;

    public ClienteOperationNew(string Server, int Port, string token, 
                               string ServerFlexLine, int PortFlexLine, string TokenFlexLine,
                               string ServerAd, int PortAd, string RouteAd,
                               string RouteFlexCart)
    {        
        serverFlexLine = ServerFlexLine;
        portFlexLine = PortFlexLine;
        tokenFlexLine = TokenFlexLine;

        ServerAdmin = ServerAd;
        PortAdmin = PortAd;
        RouteAdmin = RouteAd;

        RouteFlexCartera = RouteFlexCart;
    }

    public string ValidarCartera(int idRol, string ruc)
    {
        string result = string.Empty;
        if (ConsultarRolCliente(ruc))
        {
            string item = ConsultaEstadoHumalab(ruc);
            string item2 = ConsultaEstadoFlexline(ruc);
            (string, string) tuple = (item, item2);
            (string, string) tuple2 = tuple;
            switch (tuple2.Item1)
            {
                case "A":
                    {
                        string item3 = tuple2.Item2;
                        if (!(item3 == "A"))
                        {
                            if (item3 == "S")
                            {
                                ActualizaEstadoHumalab(ruc, "S");
                                result = "S";
                            }
                        }
                        else
                        {
                            result = "A";
                        }

                        break;
                    }
                case "T":
                    {
                        string item3 = tuple2.Item2;
                        if (!(item3 == "A"))
                        {
                            if (item3 == "S")
                            {
                                result = "T";
                            }
                        }
                        else
                        {
                            ActualizaEstadoHumalab(ruc, "A");
                            result = "A";
                        }

                        break;
                    }
                case "S":
                    {
                        string item3 = tuple2.Item2;

                        if (!(item3 == "A"))
                        {
                            if (item3 == "S")
                            {
                                result = "S";
                            }
                        }
                        else
                        {
                            ActualizaEstadoHumalab(ruc, "A");
                            result = "A";
                        }

                        break;
                    }
            }
        }
        else
        {
            result = "A";
        }

        return result;
    }

    #region consulta servicios

    public bool ConsultarRolCliente(string ruc)
    {
        try
        {
            string text = "/existeCliente?";
            if (!string.IsNullOrWhiteSpace(ruc))
            {
                text += $"ruc={HttpUtility.UrlEncode(ruc)}";
            }

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + text;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<bool>(responseText)!;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string ConsultaEstadoHumalab(string ruc)
    {
        ClienteHumalab clienteHumalab = new();

        try
        {
            string text = "/consultarEstadoHumalab?";
            if (!string.IsNullOrWhiteSpace(ruc))
            {
                text += $"ruc={HttpUtility.UrlEncode(ruc)}";
            }

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + text;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    clienteHumalab = JsonConvert.DeserializeObject<ClienteHumalab>(responseText)!;

                    if (clienteHumalab == null || clienteHumalab.Estadocliente == null)
                    {
                        return "S";
                    }

                    return clienteHumalab.Estadocliente;
                }
            }
            //ClienteHumalab clienteHumalab = CommunicatorHumaLab.InvokeOperation<ClienteHumalab>(text, (TipoOperacion)1);
            //if (clienteHumalab == null || clienteHumalab.Estadocliente == null)
            //{
            //    return "S";
            //}

            //return clienteHumalab.Estadocliente;
        }
        catch (Exception)
        {
            return "S";
        }
    }

    public int ActualizaEstadoHumalab(string ruc, string estado)
    {
        try
        {
            string text = "/actualizaEstado?";
            if (!string.IsNullOrWhiteSpace(ruc))
            {
                text += $"ruc={HttpUtility.UrlEncode(ruc)}";
            }

            if (!string.IsNullOrWhiteSpace(estado))
            {
                text += $"&estado={HttpUtility.UrlEncode(estado)}";
            }

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + text;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<int>(responseText)!;
                }
            }
            //return CommunicatorHumaLab.InvokeOperation<int>(text, (TipoOperacion)1);
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
            Task<ClienteFlexline> task = comunicatorFlexLine(ruc);
            if (task.Result == null)
            {
                return "S";
            }
            //return task.Result.EstadoCartera ? "A" : "S";
            return task.Result.EstadoCartera ? "S" : "A";
        }
        catch (Exception)
        {
            return "A";
        }
    }

    private async Task<ClienteFlexline> comunicatorFlexLine(string ruc)
    {
        string metodo = "cartera?";
        if (!string.IsNullOrWhiteSpace(ruc))
        {
            metodo += $"ruc={HttpUtility.UrlEncode(ruc)}";
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenFlexLine);
        HttpResponseMessage result = await client.GetAsync("http://" + serverFlexLine + ":" + portFlexLine + RouteFlexCartera + "/" + metodo);
        if (!result.IsSuccessStatusCode)
        {
            throw new ArgumentException("something bad happended");
        }

        return JsonConvert.DeserializeObject<ClienteFlexline>(await result.Content.ReadAsStringAsync())!;
    }

    //Estados de la orden
    public List<CatalogoDetalle> ListarEstadosAdmin(string NombreEstado)
    {
        List<CatalogoDetalle> lista = new List<CatalogoDetalle>();
        object objCatalogo;

        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/listarestadosAdmin?");
            stringBuilder.Append("NombreEstado=" + NombreEstado);
            var queryString = stringBuilder.ToString();

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + queryString;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objCatalogo = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }
            
            lista = JsonConvert.DeserializeObject<List<CatalogoDetalle>>(objCatalogo.ToString()!)!;
            return lista;
        }
        catch(Exception ex)
        {
            return lista;
        }
    }

    //Ordenes de todos los clientes
    public List<ListarOrden> ListarOrden(ConsultarOrden valor)
    {
        string FechaIniciall = valor.FechaInicio.GetDateTimeFormats('d')[0].Replace("/", "d");
        string FechaFinall = valor.FechaFin.GetDateTimeFormats('d')[0].Replace("/", "d");

        string FechaInicial = valor.FechaInicio.ToString("dd'\\d'MM'\\d'yyyy");
        string FechaFinal = valor.FechaFin.ToString("dd'\\d'MM'\\d'yyyy");

        List<ListarOrden> lista = new List<ListarOrden>();
        object objPedidos;

        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/listarordenesAdmin?");
            stringBuilder.Append("OpcionBusqueda=" + valor.OpcionBusqueda + "&");

            if (valor.opcionEstado != null && valor.opcionEstado != "")
                stringBuilder.Append("opcionEstado=" + (valor.opcionEstado ?? string.Empty) + "&");

            if (valor.DatoBusqueda != null && valor.DatoBusqueda != "")
                stringBuilder.Append("DatoBusqueda=" + (valor.DatoBusqueda ?? string.Empty) + "&");

            stringBuilder.Append("IdOrden=" + valor.IdOrden + "&");

            if (valor.CodigoBarra != null && valor.CodigoBarra != "")
                stringBuilder.Append("CodigoBarra=" + (valor.CodigoBarra ?? string.Empty) + "&");

            stringBuilder.Append("IdUsuarioGalileo=" + valor.IdUsuarioGalileo + "&");
            stringBuilder.Append("FechaInicio=" + FechaInicial + "&");
            stringBuilder.Append("FechaFin=" + FechaFinal);
            var queryString = stringBuilder.ToString();

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + queryString;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPedidos = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
            lista = JsonConvert.DeserializeObject<List<ListarOrden>>(objPedidos.ToString()!)!;
            return lista;
        }
        catch(Exception ex)
        {
            return lista;
        }
    }

    //eliminar orden administrador
    public int EliminarOrdenAdmin(Orden orden)
    {
        int result = Transaccion.Error;
        try
        {
            string ruta = "/eliminarordenAdmin?";

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + ruta;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "POST";
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(orden);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<int>(responseText)!;
                }
            }

            return result;

        }
        catch (Exception ex)
        {
            return result;
        }
    }

    //consulta una orden en especifico
    public Orden ConsultarOrdenAdmin(int IdOrden)
    {
        object objPedidos;

        var stringBuilder = new StringBuilder();
        stringBuilder.Append("/consultarordenAdmin?");
        stringBuilder.Append("IdOrden=" + IdOrden);
        var queryString = stringBuilder.ToString();

        var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + queryString;
        var request = (HttpWebRequest)WebRequest.Create(url)!;
        request.Method = "GET";

        using (var response = (HttpWebResponse)request.GetResponse())
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseText = reader.ReadToEnd();
                objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
            }
        }
        
        Orden norden = JsonConvert.DeserializeObject<Orden>(objPedidos.ToString()!)!;
        return norden;
    }


    //lista las pruebas de la orden
    public string ListarPruebasAdmin(int IdOrden, int IdUsuario)
    {
        string lista = string.Empty;
        object objPedidos;

        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/consultarpruebaAdmin?");
            stringBuilder.Append("IdOrden=" + IdOrden + "&");
            stringBuilder.Append("IdUsuarioGalileo=" + IdUsuario);
            var queryString = stringBuilder.ToString();

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + queryString;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            lista = JsonConvert.SerializeObject(objPedidos);
            return lista;
        }
        catch(Exception ex)
        {
            return lista;
        }
    }

    public string NombreEstadoOrdenAdmin(int idOrdenEstado)
    {
        string nombreEstado = "";

        try
        {
            string ruta = "/nombreEstadoOrdenAdmin";

            if (idOrdenEstado > 0)
            {
                ruta += string.Format("?idOrdenEstado={0}", HttpUtility.UrlEncode(idOrdenEstado.ToString()));
            }

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + ruta;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();                    
                    nombreEstado = responseText;
                }
            }
        }
        catch(Exception ex)
        {
            return ex.ToString();
        }

        return nombreEstado;
    }

    public List<CataTiposClienteResponse> TiposClientesHumAdmin()
    {
        string ruta = "/listarTiposClientesAdmin";
        var tipos = new List<CataTiposClienteResponse>();

        try
        {
            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + ruta;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    tipos = JsonConvert.DeserializeObject<List<CataTiposClienteResponse>>(responseText)!;
                }
            }

            if (tipos.Count > 0)
            {
                return tipos;
            }
        }
        catch
        {
            return tipos;
        }

        return tipos;
    }

    public int ActualizarOrden(Orden orden)
    {
        int result = Transaccion.Error;
        
        try
        {
            string ruta = "/actualizarordenAdmin?";

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + ruta;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "POST";
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(orden);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<int>(responseText)!;
                }
            }
            
            return result;
        }
        catch(Exception ex)
        {
            return result;
        }
    }

    public List<Muestras> ListarMuestrasAdmin(int IdOrden)
    {
        List<Muestras> lista = new List<Muestras>();
        object objMuestras;

        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/listarmuestrasAdmin?");
            stringBuilder.Append("IdOrden=" + IdOrden);
            var queryString = stringBuilder.ToString();

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + queryString;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    objMuestras = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }
            
            lista = JsonConvert.DeserializeObject<List<Muestras>>(objMuestras.ToString()!)!;
            return lista;
        }
        catch(Exception ex)
        {
            return lista;
        }
    }

    public List<CodigoBarrasPdf> MuestrasEtiquetasAdmin(List<Muestras> muestra)
    {
        List<CodigoBarrasPdf> codigoBarras = new List<CodigoBarrasPdf>();

        try
        {
            string ruta = "/pdfetiquetasAdmin";

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + ruta;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "POST";
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(muestra);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    codigoBarras = JsonConvert.DeserializeObject<List<CodigoBarrasPdf>>(responseText)!;
                }
            }

        }
        catch (Exception ex)
        {
        }

        return codigoBarras;
    }

    public int EliminarPruebaAdmin(Pruebas prueba)
    {
        int result = Transaccion.Error;

        try
        {
            string ruta = "/eliminarpruebasAdmin?";
            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + ruta;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "POST";
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(prueba);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<int>(responseText)!;
                }
            }

            return result;
        }
        catch(Exception ex)
        {
            return result;
        }
    }

    public string ResultadosNuevoPdfAdmin(string CodigoBarra)
    {
        var error = string.Empty;
        string obs = "";

        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/resultadoPdfinalAdmin?");
            stringBuilder.Append("codBarra=" + CodigoBarra);
            var queryString = stringBuilder.ToString();

            var url = ServerAdmin + ":" + PortAdmin + "/" + RouteAdmin + queryString;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    obs = responseText;
                }
            }

            return obs;
        }
        catch(Exception ex)
        {
            return error;
        }
    }

    #endregion
}
