using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Cliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace GDifare.Portal.Humalab.Servicio.Cliente
{
    public class GenerarResultPDF
    {
        private readonly Communicator CommunicatorGestionPDF;

        private string ServerGenPdf;
        private int PortGenPdf;
        private string RouteGenPdf;

        public GenerarResultPDF(AppServicioClienteApi configuracion)
        {
            CommunicatorGestionPDF = new Communicator(configuracion.Server, configuracion.PortPDF, Routes.PathServicioPDF, configuracion.Token);

            ServerGenPdf = configuracion.ServerGenPdf;
            PortGenPdf = configuracion.PortGenPdf;
            RouteGenPdf = Routes.PathServicioPDF;
        }


        public string ObtenerEtiquetas(List<Pruebas> prueba)
        {
            string result = string.Empty;
            try
            {
                //byte[] pdf;
                string ruta = "pdfetiquetas";
                //pdf = Convert.FromBase64String(CommunicatorGestionPDF.InvokeOperation<string, List<Pruebas>>(ruta, TipoOperacion.POST, prueba));
                result = CommunicatorGestionPDF.InvokeOperation<string, List<Pruebas>>(ruta, TipoOperacion.POST, prueba);
                return result;

            }
            catch (Exception)
            {
                return result;
            }
            
        }

        public List<CodigoBarrasPdf> MuestrasEtiquetas(List<Muestras> muestra)
        {            
            List<CodigoBarrasPdf> codigoBarras = new List<CodigoBarrasPdf>();

            try
            {                
                string ruta = "pdfetiquetas";

                var url = ServerGenPdf + ":" + PortGenPdf + RouteGenPdf + ruta;
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

                //codigoBarras = CommunicatorGestionPDF.InvokeOperation<List<CodigoBarrasPdf>, List<Muestras>>(ruta, TipoOperacion.POST, muestra);                
            }
            catch (Exception ex)
            {                
            }

            return codigoBarras;
        }

        //public string MuestrasEtiquetas(List<Muestras> muestra)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        //byte[] pdf;
        //        string ruta = "pdfetiquetas";
        //        //pdf = Convert.FromBase64String(CommunicatorGestionPDF.InvokeOperation<string, List<Pruebas>>(ruta, TipoOperacion.POST, prueba));
        //        result = CommunicatorGestionPDF.InvokeOperation<string, List<Muestras>>(ruta, TipoOperacion.POST, muestra);
        //        return result;

        //    }
        //    catch (Exception)
        //    {
        //        return result;
        //    }
        //}

        public string PdfGenerarResult(GenPdfResultados genPdf)
        {
            string result = string.Empty;
            
            try
            {
                string ruta = "pdfResultados";
                result = CommunicatorGestionPDF.InvokeOperation<string, GenPdfResultados>(ruta, TipoOperacion.POST, genPdf);
                return result;
            }
            catch (Exception)
            {
                return "01";
            }            
        }

    }
}
