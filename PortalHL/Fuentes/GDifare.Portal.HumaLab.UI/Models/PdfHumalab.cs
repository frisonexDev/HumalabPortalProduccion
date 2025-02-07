using GDifare.Portal.Humalab.Servicio.Modelos.Cliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Facturas;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.Utils;
using HtmlAgilityPack;
using HtmlRendererCore.PdfSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using OfficeOpenXml;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Drawing;
using System.IO;
using System.Text;
using ZXing;
using ZXing.Windows.Compatibility;


namespace GDifare.Portal.HumaLab.UI.Models;

public class PdfHumalab
{    

    #region GenerarDocumFactu
    public FacturaBase64 GenerarDocumFactu(DatosFactura datosFactura)
    {
        HtmlDocument htmlDocument = new HtmlDocument();
        FacturaBase64 facturaBase64 = new FacturaBase64();

        string base64Pdf = "";
        string htmlcontent = "";

        try
        {
            htmlcontent = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Factura Cliente</title>\r\n\r\n    <style type=\"text/css\">\r\n        body {\r\n            font-size: 10px;\r\n        }\r\n\r\n        .tableheader {\r\n            border: silver 1px solid;\r\n            padding: 5px;\r\n        }\r\n\r\n        .tablePruebas tr td {\r\n            border: silver 1px solid;\r\n            padding: 10px;\r\n        }\r\n\r\n        .cuadrado {\r\n            font-size: 24px;\r\n            border: solid 1px black;\r\n            width: 200px;\r\n            height: 10px;\r\n            text-align: center;\r\n            font-weight: bold;\r\n        }\r\n\r\n        #total {\r\n            display: flex;\r\n            flex-direction: column;\r\n            align-items: end;\r\n        }\r\n\r\n        p {\r\n            color: black;\r\n        }\r\n\r\n        #datosFactura {\r\n            width: 18%;\r\n            padding-top: 11px;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <br />\r\n    <div>\r\n        <img class=\"adapt-img\" src=\"https://slynox.stripocdn.email/content/guids/CABINET_6c99b0fb926c16c619692950ec20f07b97ec51ad8db74166634208aa62d01eed/images/humalab.PNG\" alt=\"\" width=\"150\" style=\"display:block;font-size:14px;border:0;outline:none;text-decoration:none\" />\r\n    </div>\r\n\r\n\r\n    <table style=\"width:100%\">\r\n        <tr>\r\n            <td style=\"width:40%\">\r\n                <h2>HUMALAB S.A</h2>\r\n                <h3>HUMALAB S.A</h3>\r\n                <h4>Matriz: AV.6 DE DICIEMBRE N3-110 Y WHYMPER</h4>\r\n                <h5>Sucursal: URB. CIUDAD COLON, EDIFICIO METROPARK LOCAL 9</h5>\r\n                <h5>Contribuyente Especial Nro.</h5>\r\n                <h5>Obligado a llevar contabilidad: NO</h5>\r\n            </td>\r\n\r\n\r\n\r\n            <td style=\"width:50%\">\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n                <div id=\"estadoFac\" class=\"cuadrado\"></div>\r\n\r\n\r\n\r\n                <div>\r\n                    <h5>Fecha y Hora de Autorización : </h5><span id=\"fechaAut\"></span>\r\n\r\n                </div>  <div>\r\n                    <h5>Ambiente: PRODUCCIÓN</h5>\r\n                </div><div>\r\n                    <h5>Tipo de Emisión: NORMAL</h5>\r\n                </div>\r\n\r\n\r\n\r\n\r\n\r\n            </td>\r\n        </tr>\r\n    </table>\r\n\r\n\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#FDFEFE\">\r\n        <div style=\" width:100%\">\r\n            <table class=\"tableheader\" style=\"width:100%\">\r\n                <tr class=\"tableheader\">\r\n                    <td style=\"font-weight: bold;\">Razón Social/Nombres y Apellidos:</td>\r\n                    <td id=\"nombre\"></td>\r\n                    <td style=\"font-weight: bold;\">Fecha Emisión: </td>\r\n                    <td id=\"fecha\"></td>\r\n                </tr>\r\n                <tr class=\"tableheader\">\r\n                    <td style=\"font-weight: bold;\">RUC/CI:</td>\r\n                    <td id=\"ruc\"></td>\r\n                    <td style=\"font-weight: bold;\">Guía Remisión:</td>\r\n                    <td id=\"guia\"></td>\r\n\r\n                </tr>\r\n                <tr class=\"tableheader\">\r\n                    <td></td>\r\n                    <td></td>\r\n                    <td style=\"font-weight: bold;\">Moneda:</td>\r\n                    <td id=\"dolar\">DOLAR</td>\r\n                </tr>\r\n                <tr>\r\n                </tr>\r\n            </table>\r\n\r\n            <br />\r\n            <br />\r\n            <table id=\"tablaFactura\" class=\"tablePruebas\" class=\"tablePruebas\" style=\"width:100%;\">\r\n\r\n                <thead>\r\n                    <tr class=\"tablePruebas\" style=\" text-align: center; font-weight: bold;\">\r\n                        <td style=\"font-weight: bold;\"> Código </td>\r\n                        <td style=\"font-weight: bold;\"> Descripción </td>\r\n                        <td style=\"font-weight: bold;\"> Cantidad </td>\r\n                        <td style=\"font-weight: bold;\"> Precio Unitario </td>\r\n                        <td style=\"font-weight: bold;\"> Total </td>\r\n                    </tr>\r\n                </thead>\r\n\r\n                <tbody id=\"tablaFacturaDatos\"></tbody>\r\n\r\n                <tfoot>\r\n                    <tr>\r\n                        <td style=\"text-align: right; font-weight: bold;\" colspan=\"4\">Total</td>\r\n                        <td id=\"total\" style=\"text-align: right; font-weight: bold;\"></td>\r\n                    </tr>\r\n                </tfoot>\r\n            </table>\r\n            <br />\r\n\r\n            <h5>Total Ordenes : <span id=\"totalMuestra\"></span></h5>\r\n            <h5>Total Factura :<span id=\"detalleMuestras\"></span></h5>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n</body>\r\n</html>";

            htmlDocument.LoadHtml(htmlcontent);

            //cabecera tabla
            htmlDocument.GetElementbyId("nombre").InnerHtml = datosFactura.cliente;
            htmlDocument.GetElementbyId("ruc").InnerHtml = datosFactura.Ruc;
            htmlDocument.GetElementbyId("fecha").InnerHtml = datosFactura.fecha;
            htmlDocument.GetElementbyId("estadoFac").InnerHtml = datosFactura.estadoFactura;

            DateTime fechaProceso = DateTime.Now;
            string fechaFinal = fechaProceso.ToString();
            htmlDocument.GetElementbyId("fechaAut").InnerHtml = fechaFinal;

            //datos de la tabla
            var tablaFactura = htmlDocument.GetElementbyId("tablaFacturaDatos");
            if (tablaFactura == null)
            {
                tablaFactura = htmlDocument.CreateElement("table");
                tablaFactura.Id = "miTablaFactura";
                htmlDocument.DocumentNode.AppendChild(tablaFactura);
            }

            foreach (var listaTabla in datosFactura.facturaList!)
            {
                var filaTabla = htmlDocument.CreateElement("tr");
                tablaFactura.AppendChild(filaTabla);

                var celdaCodigo = htmlDocument.CreateElement("td");
                celdaCodigo.InnerHtml = listaTabla.Codigo!.ToString();
                filaTabla.AppendChild(celdaCodigo);

                var celdaDescripcion = htmlDocument.CreateElement("td");
                celdaDescripcion.InnerHtml = listaTabla.Nombre!.ToString();
                filaTabla.AppendChild(celdaDescripcion);

                var celdaCantidad = htmlDocument.CreateElement("td");
                celdaCantidad.InnerHtml = listaTabla.Cantidad!.ToString();
                filaTabla.AppendChild(celdaCantidad);

                var celdaPrecio = htmlDocument.CreateElement("td");
                celdaPrecio.InnerHtml = listaTabla.Precio?.ToString() ?? "";
                filaTabla.AppendChild(celdaPrecio);

                var celdaTotal = htmlDocument.CreateElement("td");
                celdaTotal.InnerHtml = listaTabla.Precio?.ToString() ??"";
                filaTabla.AppendChild(celdaTotal);
            }

            htmlDocument.GetElementbyId("total").InnerHtml = datosFactura.totalFactura?.ToString() ?? "";
            htmlDocument.GetElementbyId("totalMuestra").InnerHtml = datosFactura.totalMuestra?.ToString() ?? "";

            string numOrden = string.Join(", ", datosFactura.OrdenList!);
            htmlDocument.GetElementbyId("detalleMuestras").InnerHtml = numOrden;

            //arma todo el HTML
            var finalHtml = htmlDocument.DocumentNode.OuterHtml;

            var data = new PdfSharpCore.Pdf.PdfDocument();

            PdfGenerator.AddPdfPages(data, finalHtml, PdfSharpCore.PageSize.A4);
            using (MemoryStream ms = new MemoryStream())
            {
                data.Save(ms, false);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] response = ms.ToArray();
                base64Pdf = Convert.ToBase64String(response);

                facturaBase64.base64PDF = base64Pdf;
                facturaBase64.status = 200;
            }

        }
        catch (Exception ex)
        {
            facturaBase64.base64PDF = "Error";
            facturaBase64.status = 401;
        }

        return facturaBase64;
    }

    #endregion GenerarDocumFactu

    #region GenerarDocumPedido

    public string GenerarDocumPedido(VerDetallePedidoOperadorResponse pedidoOpe)
    {
        string htmlcontent = "";
        string base64Pdf = "";
        HtmlDocument htmlDocument = new HtmlDocument();

        try
        {
			//htmlcontent = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Pedidos Cliente</title>\r\n\r\n    <style type=\"text/css\">\r\n        body {\r\n            background-color: white;\r\n        }\r\n\r\n        h1 {\r\n            text-align: center;\r\n        }\r\n\r\n        .border {\r\n            border-collapse: collapse;\r\n            width: 100%;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n            .border th, .border td {\r\n                padding: 5px;\r\n                border: 2px solid black;\r\n            }\r\n\r\n        .header {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n            .header div {\r\n                flex-basis: 48%;\r\n            }\r\n\r\n            .header strong {\r\n                display: inline-block;\r\n                width: 150px;\r\n            }\r\n\r\n        .summary {\r\n            text-align: left;\r\n            width: 100%;\r\n            display: flex;\r\n            justify-content: space-between;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n            .summary strong {\r\n                flex-basis: 50%;\r\n            }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;height:100%;padding:0;Margin:0\">\r\n    <br />\r\n    <div>\r\n        <img class=\"adapt-img\" src=\"https://slynox.stripocdn.email/content/guids/CABINET_6c99b0fb926c16c619692950ec20f07b97ec51ad8db74166634208aa62d01eed/images/humalab.PNG\" alt=\"\" width=\"150\" style=\"display:block;font-size:14px;border:0;outline:none;text-decoration:none\" />\r\n    </div>\r\n\r\n\r\n    <table style=\"width:100%\">\r\n        <tr>\r\n            <td style=\"width:50%\">\r\n                <p><strong id=\"cliente\">asda</strong></p>\r\n                <p><strong id=\"usuario\">asd</strong></p>\r\n                <p><strong id=\"contacto\">asd</strong></p>\r\n                <p><strong id=\"correo\">asd</strong></p>\r\n                <p><strong id=\"obser\">asd</strong></p>\r\n            </td>\r\n\r\n            <td style=\"width:50%\">\r\n                <p><strong id=\"remision\">asd</strong></p>\r\n                <p><strong id=\"operador\">asd</strong></p>\r\n                <!--<p><strong id=\"contactoOp\">asd</strong></p>-->\r\n                <p><strong id=\"correoOp\">asd</strong></p>\r\n                <p><strong id=\"obserOp\">asd</strong></p>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#FDFEFE\">\r\n        <table class=\"border\" style=\"width:100%\">\r\n            <thead>\r\n                <tr>\r\n                    <th>Paciente</th>\r\n                    <th>Muestra</th>\r\n                    <th>Tipo muestra</th>\r\n                    <th>Fecha</th>\r\n                    <th>Usuario</th>\r\n                    <th>Retirado</th>\r\n                    <th>Rechazado</th>\r\n                    <th>Comentario</th>\r\n                    <th>Estado</th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tablaPedidosDatos\">\r\n            </tbody>\r\n\r\n        </table>\r\n\r\n        <p><strong id=\"totalMuestras\"></strong></p>\r\n        <p><strong id=\"muestrasReco\"></strong></p>\r\n\r\n    </div>\r\n\r\n</body>\r\n</html>";
			htmlcontent = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\">\r\n<head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta content=\"width=device-width, initial-scale=1\" name=\"viewport\" />\r\n    <meta name=\"x-apple-disable-message-reformatting\" />\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <title>Pedidos Cliente</title>\r\n\r\n    <style type=\"text/css\">\r\n        body {\r\n            background-color: white;\r\n        }\r\n\r\n        h1 {\r\n            text-align: center;\r\n        }\r\n\r\n        .border {\r\n            border-collapse: collapse;\r\n            width: 100%;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n            .border th, .border td {\r\n                padding: 5px;\r\n                border: 2px solid black;\r\n            }\r\n\r\n        .header {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n            .header div {\r\n                flex-basis: 48%;\r\n            }\r\n\r\n            .header strong {\r\n                display: inline-block;\r\n                width: 150px;\r\n            }\r\n\r\n        .summary {\r\n            text-align: left;\r\n            width: 100%;\r\n            display: flex;\r\n            justify-content: space-between;\r\n            margin-bottom: 20px;\r\n        }\r\n\r\n            .summary strong {\r\n                flex-basis: 50%;\r\n            }\r\n    </style>\r\n</head>\r\n<body style=\"width:100%;height:100%;padding:0;Margin:0\">\r\n    <br />\r\n    <div>\r\n        <img class=\"adapt-img\" src=\"https://slynox.stripocdn.email/content/guids/CABINET_6c99b0fb926c16c619692950ec20f07b97ec51ad8db74166634208aa62d01eed/images/humalab.PNG\" alt=\"\" width=\"150\" style=\"display:block;font-size:14px;border:0;outline:none;text-decoration:none\" />\r\n    </div>\r\n\r\n\r\n    <table style=\"width:100%\">\r\n        <tr>\r\n            <td style=\"width:50%\">\r\n                <p><strong id=\"cliente\">asda</strong></p>\r\n                <p><strong id=\"laboratorio\">asda</strong></p>\r\n                <p><strong id=\"usuario\">asd</strong></p>\r\n                <p><strong id=\"contacto\">asd</strong></p>\r\n                <p><strong id=\"correo\">asd</strong></p>\r\n                <p><strong id=\"obser\">asd</strong></p>\r\n                <p><strong id=\"direcCliente\"></strong></p>\r\n                <p><strong id=\"ciudadCliente\"></strong></p>\r\n            </td>\r\n\r\n            <td style=\"width:50%\">\r\n                <p><strong id=\"remision\">asd</strong></p>\r\n                <p><strong id=\"operador\">asd</strong></p>\r\n                <!--<p><strong id=\"contactoOp\">asd</strong></p>-->\r\n                <p><strong id=\"correoOp\">asd</strong></p>\r\n                <p><strong id=\"obserOp\">asd</strong></p>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n\r\n    <div class=\"es-wrapper-color\" style=\"background-color:#FDFEFE\">\r\n        <table class=\"border\" style=\"width:100%\">\r\n            <thead>\r\n                <tr>\r\n                    <th>Paciente</th>\r\n                    <th>Identificación</th>\r\n                    <th>Examen</th>\r\n                    <th>Muestra</th>\r\n                    <th>Tipo muestra</th>\r\n                    <th>Fecha</th>                    \r\n                    <th>Retirado</th>\r\n                    <th>Rechazado</th>\r\n                    <th>Comentario</th>\r\n                    <th>Estado</th>\r\n                </tr>\r\n            </thead>\r\n\r\n            <tbody id=\"tablaPedidosDatos\">\r\n            </tbody>\r\n\r\n        </table>\r\n\r\n        <p><strong id=\"totalMuestras\"></strong></p>\r\n        <p><strong id=\"muestrasReco\"></strong></p>\r\n\r\n    </div>\r\n\r\n</body>\r\n</html>";

			htmlDocument.LoadHtml(htmlcontent);

            //cabecera
            htmlDocument.GetElementbyId("cliente").InnerHtml = "Cliente: " + pedidoOpe.Pedido.Cliente;
			htmlDocument.GetElementbyId("laboratorio").InnerHtml = "Laboratorio: " + (pedidoOpe.Pedido.Laboratorio ?? "");
			htmlDocument.GetElementbyId("usuario").InnerHtml = "Responsable: " + pedidoOpe.Pedido.Usuario;
            htmlDocument.GetElementbyId("contacto").InnerHtml = "Contacto responsable: " + pedidoOpe.Pedido.Telefono;
            htmlDocument.GetElementbyId("correo").InnerHtml = "Correo responsable: " + pedidoOpe.Pedido.CorreoCliente;
            htmlDocument.GetElementbyId("obser").InnerHtml = "Observación responsable: " + pedidoOpe.Pedido.ObservacionCliente;
            htmlDocument.GetElementbyId("direcCliente").InnerHtml = "Direccion: " + (pedidoOpe.Pedido.ClienteDireccion ?? "");
            htmlDocument.GetElementbyId("ciudadCliente").InnerHtml = "Ciudad: " + (pedidoOpe.Pedido.ClienteCiudad ?? "");            

			htmlDocument.GetElementbyId("remision").InnerHtml = "N° de remisión: " + pedidoOpe.Pedido.NumeroRemision;
            htmlDocument.GetElementbyId("operador").InnerHtml = "Operador logístico: " + pedidoOpe.Pedido.OperadorLogistico;
            //htmlDocument.GetElementbyId("contactoOp").InnerHtml = "Contacto operador logístico: " + pedidoOpe.Pedido.CorreoCliente;
            //htmlDocument.GetElementbyId("correoOp").InnerHtml = "Correo operador logístico: " + pedidoOpe.Pedido.CorreoCliente;
            htmlDocument.GetElementbyId("correoOp").InnerHtml = "Fecha creación pedido: " + pedidoOpe.Pedido.FechaCreacion;
            htmlDocument.GetElementbyId("obserOp").InnerHtml = "Observación operador logístico: " + pedidoOpe.Pedido.ObservacionOperador;

            //datos de la tabla
            var tablaPedidos = htmlDocument.GetElementbyId("tablaPedidosDatos");

            foreach (var listaTabla in pedidoOpe.Muestras)
            {
                var filaTabla = htmlDocument.CreateElement("tr");
                tablaPedidos.AppendChild(filaTabla);

                var Paciente = htmlDocument.CreateElement("td");
                Paciente.InnerHtml = listaTabla.Paciente != null ? listaTabla.Paciente.ToString() : "";
                filaTabla.AppendChild(Paciente);

                var Identificacion = htmlDocument.CreateElement("td");
                Identificacion.InnerHtml = listaTabla.Identificacion != null ? listaTabla.Identificacion.ToString() : "";
                filaTabla.AppendChild(Identificacion);

                var NombreExamen = htmlDocument.CreateElement("td");
                NombreExamen.InnerHtml = listaTabla.NombrePrueba != null ? listaTabla.NombrePrueba.ToString() : "";
                filaTabla.AppendChild(NombreExamen);

                var TipoMuestra = htmlDocument.CreateElement("td");
                TipoMuestra.InnerHtml = /*listaTabla.TipoMuestra != null ?*/ listaTabla.CodigoBarra.ToString() /*: ""*/;
                filaTabla.AppendChild(TipoMuestra);

                var Muestra = htmlDocument.CreateElement("td");
                Muestra.InnerHtml = listaTabla.TipoMuestra!.ToString();
                filaTabla.AppendChild(Muestra);

                var Numero = htmlDocument.CreateElement("td");
                Numero.InnerHtml = listaTabla.FechaOrden.ToString("yyyy-MM-dd");
                filaTabla.AppendChild(Numero);                

                var IdPruebaReti = htmlDocument.CreateElement("td");
                IdPruebaReti.InnerHtml = listaTabla.Retirado == true ? "Sí" : "No";
                filaTabla.AppendChild(IdPruebaReti);

                var IdPruebaRecha = htmlDocument.CreateElement("td");
                IdPruebaRecha.InnerHtml = listaTabla.Rechazado == true ? "Sí" : "No";
                filaTabla.AppendChild(IdPruebaRecha);

                var ObservacionOperador = htmlDocument.CreateElement("td");
                ObservacionOperador.InnerHtml = listaTabla.ObservacionOperador != null ? listaTabla.ObservacionOperador.ToString() : "";
                filaTabla.AppendChild(ObservacionOperador);

                var EstadoMuestra = htmlDocument.CreateElement("td");
                EstadoMuestra.InnerHtml = listaTabla.EstadoMuestra != null ? listaTabla.EstadoMuestra.ToString() : "";
                filaTabla.AppendChild(EstadoMuestra);
            }

            htmlDocument.GetElementbyId("totalMuestras").InnerHtml = "Total de muestras: " + pedidoOpe.Pedido.TotalMuestras;
            htmlDocument.GetElementbyId("muestrasReco").InnerHtml = "Muestras recolectadas: " + pedidoOpe.Pedido.MuestrasRecolectadas;

            //arma todo el HTML
            var finalHtml = htmlDocument.DocumentNode.OuterHtml;

            var data = new PdfSharpCore.Pdf.PdfDocument();

            PdfGenerator.AddPdfPages(data, finalHtml, PdfSharpCore.PageSize.A3);
            using (MemoryStream ms = new MemoryStream())
            {
                data.Save(ms, false);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] response = ms.ToArray();

                base64Pdf = Convert.ToBase64String(response);
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }

        return base64Pdf;
    }

    #endregion GenerarDocumPedido

    #region GenerarDocumPedidoExcel

    public string GenerarDocumPedidoExcel(VerDetallePedidoOperadorResponse pedidoOpe)
    {
        string base64Pdf = "";

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        try
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage(memoryStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    //Datos Cliente
                    worksheet.Cells["A2"].Value = "Cliente:";
                    worksheet.Cells["B2"].Value = pedidoOpe.Pedido.Cliente;

                    worksheet.Cells["A4"].Value = "Responsable:";
                    worksheet.Cells["B4"].Value = pedidoOpe.Pedido.Usuario;

                    worksheet.Cells["A6"].Value = "Contacto responsable:";
                    worksheet.Cells["B6"].Value = pedidoOpe.Pedido.Telefono;

                    worksheet.Cells["A8"].Value = "Correo responsable:";
                    worksheet.Cells["B8"].Value = pedidoOpe.Pedido.CorreoCliente;

                    worksheet.Cells["A10"].Value = "Observación responsable:";
                    worksheet.Cells["B10"].Value = pedidoOpe.Pedido.ObservacionCliente;

                    worksheet.Cells["A12"].Value = "Dirección:";
                    worksheet.Cells["B12"].Value = pedidoOpe.Pedido.ClienteDireccion;

                    worksheet.Cells["A14"].Value = "Ciudad:";
                    worksheet.Cells["B14"].Value = pedidoOpe.Pedido.ClienteCiudad;

                    worksheet.Cells["A15"].Value = "Laboratorio:";
                    worksheet.Cells["B15"].Value = pedidoOpe.Pedido.Laboratorio;

                    //Datos Operador
                    worksheet.Cells["A16"].Value = "N° de remisión:";
                    worksheet.Cells["B16"].Value = pedidoOpe.Pedido.NumeroRemision;

                    worksheet.Cells["A18"].Value = "Operador logístico:";
                    worksheet.Cells["B18"].Value = pedidoOpe.Pedido.OperadorLogistico;

                    worksheet.Cells["A20"].Value = "Correo operador logístico:";
                    worksheet.Cells["B20"].Value = pedidoOpe.Pedido.CorreoCliente;

                    worksheet.Cells["A22"].Value = "Observación operador logístico:";
                    worksheet.Cells["B22"].Value = pedidoOpe.Pedido.ObservacionOperador;

                    int rowStart = 24;

                    worksheet.Cells[rowStart, 1].Value = "Paciente";
                    worksheet.Cells[rowStart, 2].Value = "Nombre Examen";
                    worksheet.Cells[rowStart, 3].Value = "TipoMuestra";
                    worksheet.Cells[rowStart, 4].Value = "Muestra";
                    worksheet.Cells[rowStart, 5].Value = "Fecha";
                    worksheet.Cells[rowStart, 6].Value = "Identificación";
                    worksheet.Cells[rowStart, 7].Value = "Retirada";
                    worksheet.Cells[rowStart, 8].Value = "Rechazada";
                    worksheet.Cells[rowStart, 9].Value = "ObservacionOperador";
                    worksheet.Cells[rowStart, 10].Value = "EstadoMuestra";

                    int row = rowStart + 1;
                    foreach (var pedidos in pedidoOpe.Muestras)
                    {
                        worksheet.Cells[row, 1].Value = pedidos.Paciente;
                        worksheet.Cells[row, 2].Value = pedidos.NombrePrueba;
                        worksheet.Cells[row, 3].Value = pedidos.TipoMuestra;
                        worksheet.Cells[row, 4].Value = pedidos.CodigoBarra;
                        worksheet.Cells[row, 5].Value = pedidos.FechaOrden.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 6].Value = pedidos.Identificacion;

                        if (pedidos.Retirado == true)
                        {
                            worksheet.Cells[row, 7].Value = "Si";
                        }
                        else
                        {
                            worksheet.Cells[row, 7].Value = "No";
                        }

                        if (pedidos.Rechazado == true)
                        {
                            worksheet.Cells[row, 8].Value = "Si";
                        }
                        else
                        {
                            worksheet.Cells[row, 8].Value = "No";
                        }

                        worksheet.Cells[row, 9].Value = pedidos.ObservacionOperador;
                        worksheet.Cells[row, 10].Value = pedidos.EstadoMuestra;
                        row++;
                    }

                    package.Save();
                }

                byte[] excelBytes = memoryStream.ToArray();
                base64Pdf = Convert.ToBase64String(excelBytes);
            }
            
        }
        catch (Exception ex)
        {
            return "01";
        }

        return base64Pdf;
    }

    #endregion GenerarDocumPedidoExcel


    #region GenerarDocumEti

    public string GenerarDocumEti(List<CodigoBarrasPdf> BarrasCodigo)
    {
        List<CodigoBarrasPdf> codigosNuevos = new List<CodigoBarrasPdf>();

        string[] codigos = { };
        var codigoBase64 = string.Empty;
        string base64Pdf = string.Empty;
        var modificadoHtml = string.Empty;         
        string htmlcontent = string.Empty;

        try
        {
            var data = new PdfSharpCore.Pdf.PdfDocument();
            htmlcontent = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n    <script src=\"https://cdn.jsdelivr.net/npm/jsbarcode@3.11.5/dist/JsBarcode.all.min.js\"></script>\r\n    <style>\r\n        \r\n    </style>\r\n</head>\r\n<body>    \r\n    {ImagesPlaceholder}    \r\n</body>\r\n</html>";

            //Genera código de barras
            foreach (var barcode in (dynamic)BarrasCodigo)
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.CODE_128;
                barcodeWriter.Options = new ZXing.Common.EncodingOptions
                {
                    Width = 285, // Ancho total código de barras
                    Height = 70, // Altura total código de barras
                    Margin = 0, // Margen alrededor código de barras
                    PureBarcode = false //mostrar el número del código
                };

                var codigoBitmap = barcodeWriter.Write(barcode.Codigo);
                codigoBase64 = ImagenBase64(codigoBitmap);

                codigosNuevos.Add(new CodigoBarrasPdf
                {
                    Nombre = barcode.Nombre,
                    Codigo = codigoBase64,
                    NombrePaciente = barcode.NombrePaciente,
                    IdentiPaciente = barcode.IdentiPaciente,
                    muestraGalileo = barcode.muestraGalileo
                });
            }

            StringBuilder finalCodigos = new StringBuilder();

            foreach (var valores in codigosNuevos)
            {
                finalCodigos.Append($"<p style=\"text-align:center; font-size: 20px;\">{valores.Nombre}</p>");
                finalCodigos.Append($"<p style=\"text-align:center; font-size: 14px;\">{valores.NombrePaciente}</p>");
                finalCodigos.Append($"<p style=\"text-align:center; font-size: 13px;\">{valores.IdentiPaciente}</p>");
                finalCodigos.Append($"<center><img src='data:image/png;base64,{valores.Codigo}'/></center>");
                finalCodigos.Append($"<p style=\"text-align:center; font-size: 12px;\">{valores.muestraGalileo}</p><br />");
            }

            //Reemplaza datos en el PDF
            htmlcontent = htmlcontent.Replace("{ImagesPlaceholder}", finalCodigos.ToString());

            //Dimensiones PDF
            var pageSize = new XSize();
            pageSize.Width = 400;
            pageSize.Height = 255;

            PdfGenerateConfig config = new PdfGenerateConfig();
            config.ManualPageSize = pageSize;
            config.PageOrientation = PageOrientation.Portrait;
            config.MarginLeft = 3;
            config.MarginRight = 5;

            PdfGenerator.AddPdfPages(data, htmlcontent, config);
            using (MemoryStream ms = new MemoryStream())
            {
                data.Save(ms, false);
                ms.Seek(0, SeekOrigin.Begin);

                byte[] response = ms.ToArray();

                base64Pdf = Convert.ToBase64String(response);
            }
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }

        return base64Pdf;
    }

    //ImagenBase64
    public string ImagenBase64(System.Drawing.Image image)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = ms.ToArray();
            return System.Convert.ToBase64String(imageBytes);
        }
    }

    #endregion GenerarDocumEti

    #region EtiquetaNew

    public string EtiquetaNew(List<CodigoBarrasPdf> BarrasCodigo)
    {
        var pgSize = new iTextSharp.text.Rectangle(211.65f, 94.48f);
        Document pdfDoc = new Document(pgSize, 0, 0, 0, 0);

        using MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

		BarcodeLib.Barcode b = new BarcodeLib.Barcode
		{
			IncludeLabel = false,
			LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER,
			ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg,
			Width = 160,
			Height = 80
		};

		pdfDoc.Open();

		foreach (var barcode in (dynamic)BarrasCodigo)
		{
			System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.CODE128, barcode.Codigo, Color.Black, Color.White, 200, 31);

			//name
			var tblOrden = new PdfPTable(1) { WidthPercentage = 100f, SpacingAfter = 0, SpacingBefore = 0 };
			PdfPCell celNombrePaciente = CrearCeldaBordeTextoCentro("", "Verdana", 6, true);
			celNombrePaciente.Padding = 0;
			celNombrePaciente.Border = 0;
			celNombrePaciente.PaddingTop = 5;
			celNombrePaciente.PaddingBottom = 5;
			celNombrePaciente.PaddingLeft = 0;
			celNombrePaciente.PaddingRight = 0;
			celNombrePaciente.Colspan = 0;
			celNombrePaciente.ExtraParagraphSpace = 0;
			tblOrden.SpacingAfter = 0;
			tblOrden.SpacingBefore = 0;

			Paragraph p;
			p = new Paragraph(6, barcode.NombrePaciente.ToUpper());
			p.SpacingAfter = 0;
			p.SpacingBefore = 0;
			p.ExtraParagraphSpace = 0;
			p.Trim();
			p.Alignment = Element.ALIGN_CENTER;
			p.Font.Size = 8;
			celNombrePaciente.AddElement(p);
			tblOrden.AddCell(celNombrePaciente);

			string celPAciente = String.Format("ID:{0}  S:{1} E:{2}", barcode.IdentiPaciente.ToString());
			Paragraph pid;
			pid = new Paragraph(1, celPAciente);
			pid.SpacingAfter = 0;
			pid.SpacingBefore = 0;
			pid.ExtraParagraphSpace = 0;
			pid.Alignment = Element.ALIGN_CENTER;
			pid.Font.Size = 6;

			PdfPCell celIdPaciente = CrearCeldaBordeTextoCentro("", "Verdana", 6, true, 0);
			celIdPaciente.Padding = 0;
			celIdPaciente.Border = 0;
			celIdPaciente.PaddingTop = 5;
			celIdPaciente.PaddingBottom = 5;
			celIdPaciente.PaddingLeft = 0;
			celIdPaciente.PaddingRight = 0;
			celIdPaciente.Colspan = 0;
			tblOrden.SpacingAfter = 0;
			celIdPaciente.AddElement(pid);
			tblOrden.AddCell(celIdPaciente);
			tblOrden.SpacingBefore = 0;

			var celLogo = new PdfPCell { Border = iTextSharp.text.Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
			celLogo.Padding = 0;
			celLogo.Border = 0;
			celLogo.Colspan = 0;

			var imgLogo = iTextSharp.text.Image.GetInstance(img, BaseColor.WHITE);
			imgLogo.Alignment = Element.ALIGN_CENTER;
			celLogo.AddElement(imgLogo);
			tblOrden.SpacingAfter = 0;
			tblOrden.SpacingBefore = 0;
			tblOrden.AddCell(celLogo);

			PdfPCell celCodigoBarras = CrearCeldaBordeTextoCentro("", "Verdana", 15, true, 0);
			
            Paragraph pCodeBarras;
			pCodeBarras = new Paragraph(1, barcode.Codigo);
			pCodeBarras.SpacingAfter = 0;
			pCodeBarras.SpacingBefore = 8;
			pCodeBarras.ExtraParagraphSpace = 0;
			pCodeBarras.Alignment = Element.ALIGN_CENTER;
			pCodeBarras.Font.Size = 15;

			celCodigoBarras.Padding = 0;
			celCodigoBarras.Border = 0;
			celCodigoBarras.PaddingBottom = 3;
			celCodigoBarras.PaddingTop = 5;
			celCodigoBarras.PaddingLeft = 0;
			celCodigoBarras.PaddingRight = 0;
			celCodigoBarras.Colspan = 0;
			celCodigoBarras.AddElement(pCodeBarras);
			tblOrden.SpacingAfter = 0;
			tblOrden.SpacingBefore = 0;
			tblOrden.AddCell(celCodigoBarras);

			pdfDoc.Add(tblOrden);
			pdfDoc.NewPage();
		}
		pdfDoc.Close();

		byte[] byteArray = memoryStream.ToArray();
		string pdfNew = Convert.ToBase64String(byteArray);

		return pdfNew;
	}

	public static PdfPCell CrearCeldaBordeTextoCentro(string Texto = "", string FontStyle = "Arial", int FontSize = 10, bool Bold = false, int ColSpan = 1)
	{
		var celTexto = new PdfPCell { HorizontalAlignment = Element.ALIGN_CENTER, Colspan = ColSpan };
		Paragraph parLaboratorio = new Paragraph { Alignment = Element.ALIGN_CENTER, Font = new iTextSharp.text.Font(FontFactory.GetFont(FontStyle, FontSize, Bold ? iTextSharp.text.Font.BOLD : iTextSharp.text.Font.NORMAL)) };
		parLaboratorio.Add(Texto);
		celTexto.AddElement(parLaboratorio);
		return celTexto;
	}
	#endregion
}
