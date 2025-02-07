using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Facturas;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace GDifare.Portal.Humalab.Servicio.Facturas
{
    public class FacturasOperacion
    {
        private readonly Communicator CommunicatorFacturas;
        private readonly Communicator CommunicatorPDF;


        private string serverFlexLineFactura;
        private int portFlexLineFactura;
        private string tokenFlexLineFactura;

        private string ServerFactura;
        private int PortFactura;

        private string ServerGenPdf;
        private int PortGenPdf;

        public FacturasOperacion(AppServicioClienteApi settingsApi, AppServicioMicrosExternos settingsExternos)
        {           
            CommunicatorFacturas = new Communicator(settingsApi.ServerFactura, settingsApi.PortFactura , Routes.PathServicioFacturas, settingsApi.Token);
            CommunicatorPDF = new Communicator(settingsApi.ServerPDF, settingsApi.PortPDF, Routes.PathServicioPDF, settingsApi.Token);

            serverFlexLineFactura = settingsExternos.ServerFlexLine;
            portFlexLineFactura = settingsExternos.PortFlexLine;
            tokenFlexLineFactura = settingsExternos.TokenFlexline;

            ServerFactura = settingsApi.ServerFactura;
            PortFactura = settingsApi.PortFactura;

            ServerGenPdf = settingsApi.ServerPDF;
            PortGenPdf = settingsApi.PortPDF;
        }
        public List<Factura> facturasPendientes(Modelos.Facturas.ClienteRequest cliente)
        {
            List<Factura>? listaFactura = null;
            try
            {
                var metodo = "listar";

                var url = ServerFactura + ":" + PortFactura + Routes.PathServicioFacturas + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "POST";
                request.ContentType = "application/json";

                var json = JsonConvert.SerializeObject(cliente);
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
                        listaFactura = JsonConvert.DeserializeObject<List<Factura>>(responseText)!;
                    }
                }

                //listaFactura = CommunicatorFacturas.InvokeOperation<List<Factura>,Modelos.Facturas.ClienteRequest> (metodo, TipoOperacion.POST, cliente);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaFactura!;
        }

        public List<Modelos.Facturas.ClienteResponse> informacionCliente(Modelos.Facturas.ClienteRequest cliente)
        {
           List<Modelos.Facturas.ClienteResponse> infocliente = null;
            try
            {
                var metodo = "consultarClientes";

                if (!string.IsNullOrWhiteSpace(cliente.Ruc))
                {
                    metodo += string.Format("?Ruc={0}", HttpUtility.UrlEncode(cliente.Ruc));
                }

                var url = ServerFactura + ":" + PortFactura + Routes.PathServicioFacturas + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();
                        infocliente = JsonConvert.DeserializeObject<List<Modelos.Facturas.ClienteResponse>>(responseText)!;
                    }
                }

                //infocliente = CommunicatorFacturas.InvokeOperation<List<Modelos.Facturas.ClienteResponse>>(metodo, TipoOperacion.GET);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return infocliente!;
        }



        public FacturaBase64 pdfFactura(DatosFactura datosFactura)
        {
            FacturaBase64  facturaBase64 = new FacturaBase64();
            try
            {
                var metodo = "pdfFacturacion";

                var url = ServerGenPdf + ":" + PortGenPdf + Routes.PathServicioPDF + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "POST";
                request.ContentType = "application/json";

                var json = JsonConvert.SerializeObject(datosFactura);
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
                        facturaBase64.base64PDF = JsonConvert.DeserializeObject<string>(responseText)!;
                    }
                }

                //facturaBase64.base64PDF = CommunicatorPDF.InvokeOperation<string,DatosFactura>(metodo, TipoOperacion.POST,datosFactura);
                facturaBase64.status = 200;
            }
            catch (Exception ex)
            {
                facturaBase64.base64PDF = ex.ToString();
                facturaBase64.status = 500;
            }
            return facturaBase64!;
        }



        public async Task<Modelos.Facturas.FacturaFlexResponse> facturar(Modelos.Facturas.ConsolidarFacturacion facturas, ObjUsuario infoUsuario)
        {
         
            Modelos.Facturas.FacturaFlexResponse jsonResult = null!;
            Modelos.Facturas.ConsolidarFacturacion consolidarFacturacion = new ConsolidarFacturacion();
            string existoHumalab = "";
            try
            {               
                FacturaFlexResquest datosEnviarFlexLine = new FacturaFlexResquest();
                DatosFactura datosFactura = new DatosFactura();
                ClienteRequest clienteRequest = new ClienteRequest();

                datosEnviarFlexLine = facturas.facturaFlexResquest!;
                datosFactura = facturas.datosFactura!;
                clienteRequest = facturas.clienteRequest!;

                consolidarFacturacion.datosFactura = datosFactura;
                consolidarFacturacion.clienteRequest = clienteRequest;

                datosEnviarFlexLine.Empresa = "E04";

                //de prueba
                //consolidarFacturacion.datosFactura.usuarioCreacion = infoUsuario.UsuarioID.ToString();
                //existoHumalab = await InsertarFactura(consolidarFacturacion);
                //return jsonResult;

                using (var client = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(tokenFlexLineFactura))
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenFlexLineFactura}");

                    Uri u = new Uri("http://" + serverFlexLineFactura + ":" + portFlexLineFactura + Routes.PathServicioFacturar);
                    HttpContent c = new StringContent(System.Text.Json.JsonSerializer.Serialize(datosEnviarFlexLine), Encoding.UTF8, "application/json");
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = u,
                        Content = c
                    };

                    HttpResponseMessage result = await client.SendAsync(request);
                    if (result.IsSuccessStatusCode)
                    {
                        jsonResult = await result.Content.ReadFromJsonAsync<Modelos.Facturas.FacturaFlexResponse>();
                    }

                    if (jsonResult != null)
                    {

                        if (jsonResult.Correlativo != 0)
                        {
                            consolidarFacturacion.datosFactura.numeroFactura = jsonResult.Correlativo.ToString();
                            consolidarFacturacion.datosFactura.usuarioCreacion = infoUsuario.UsuarioID.ToString();
                            existoHumalab = await InsertarFactura(consolidarFacturacion);
                            jsonResult.RespuestaHumalab = existoHumalab;
                        }
                    }
                    else
                    {
                        jsonResult = await result.Content.ReadFromJsonAsync<Modelos.Facturas.FacturaFlexResponse>();
                        Modelos.Facturas.FacturaFlexResponse jsonResult1 = new FacturaFlexResponse();
                        jsonResult1.Correlativo = 0;
                        jsonResult1.RespuestaHumalab = "Error desde FlexLine";
                        jsonResult1.Mensaje = jsonResult!.Message;

                        jsonResult = jsonResult1;
                    }
                    return jsonResult;
                }
            }
            catch (Exception ex) 
            {
                return jsonResult!;
            }

        }



        public Task<string> InsertarFactura(Modelos.Facturas.ConsolidarFacturacion datosFactura)
        {
            string exito = "";
            try
            {
                var metodo = "actualizarFacturadas";

                var url = ServerGenPdf + ":" + PortGenPdf + Routes.PathServicioPDF + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "POST";
                request.ContentType = "application/json";

                var json = JsonConvert.SerializeObject(datosFactura);
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
                        exito = JsonConvert.DeserializeObject<string>(responseText)!;
                    }
                }

                //exito = CommunicatorFacturas.InvokeOperation<string, ConsolidarFacturacion>(metodo, TipoOperacion.POST, datosFactura);
            }
            catch (Exception ex)
            {
                exito = "NO" ;
            }
            return Task.FromResult(exito);
        }

        public EstadosFactura EstadosFacturacion()
        {
            EstadosFactura estados = new();
            try
            {
                var metodo = "estadosFactu";

                var url = ServerFactura + ":" + PortFactura + Routes.PathServicioFacturas + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();
                        estados = JsonConvert.DeserializeObject<EstadosFactura>(responseText)!;
                    }
                }
                //estados = CommunicatorFacturas.InvokeOperation<EstadosFactura>(metodo, TipoOperacion.GET);
            }
            catch (Exception ex)
            {
                return estados;
            }

            return estados;
        }


    }
    
}
