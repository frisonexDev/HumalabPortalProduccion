
using HtmlRendererCore.PdfSharp;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.IO;


namespace GDifare.Portal.Humalab.Servicio.Facturas
{
    public class PDFService
    {
        public string pdfCreate(string templateName)
        {


            var document = new PdfDocument();
            var html = templateName;
            PdfGenerator.AddPdfPages(document, html, PageSize.A4);

            byte[]? res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                res = ms.ToArray();
            }         
            
            string base64EncodedPDF = System.Convert.ToBase64String(res);
            return base64EncodedPDF;           
        }
    }
}
