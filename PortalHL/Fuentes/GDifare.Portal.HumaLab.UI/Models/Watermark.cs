using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GDifare.Portal.HumaLab.UI.Models;

public class Watermark : PdfPageEventHelper
{
    private string wmImage;
    public Watermark(string image)
    {
        wmImage = image;
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {

        // Ruta de la imagen de la marca de agua
        string watermarkImagePath = wmImage;

        Byte[] iByteWM = Convert.FromBase64String(watermarkImagePath);

        // Cargar la imagen de la marca de agua        
        Image watermarkImage = Image.GetInstance(iByteWM);
        watermarkImage.ScaleToFit(300, 300); // Ajustar el tamaño de la imagen según tus necesidades

        // Obtener el tamaño de la página
        Rectangle pageSize = document.PageSize;

        // Crear un objeto PdfGState para establecer la opacidad de la imagen
        PdfGState gstate = new PdfGState();
        gstate.FillOpacity = 0.1f; // Ajustar la opacidad según tus necesidades

        // Guardar el estado del contenido gráfico
        PdfContentByte content = writer.DirectContentUnder;
        content.SaveState();

        // Aplicar la opacidad al contenido gráfico
        content.SetGState(gstate);

        // Colocar la imagen de la marca de agua en la esquina inferior derecha
        watermarkImage.SetAbsolutePosition(pageSize.Width - watermarkImage.ScaledWidth - 150, pageSize.Height - watermarkImage.ScaledHeight - 300);

        // Agregar la imagen de la marca de agua al contenido gráfico
        content.AddImage(watermarkImage);

        // Restaurar el estado del contenido gráfico
        content.RestoreState();
    }
}
