using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using SmartITSolution_Invoice.IServices;

namespace SmartITSolution_Invoice.Services
{
    public class PdfEditorService : IPdfEditorService
    {
        public async Task<byte[]> AddPaidStampAsync(IFormFile pdfFile)
        {
            using var inputStream = new MemoryStream();
            await pdfFile.CopyToAsync(inputStream);

            inputStream.Position = 0;

            using var outputStream = new MemoryStream();

            using var reader = new PdfReader(inputStream);
            using var writer = new PdfWriter(outputStream);
            using var pdf = new PdfDocument(reader, writer);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            for (int pageNumber = 1; pageNumber <= pdf.GetNumberOfPages(); pageNumber++)
            {
                var page = pdf.GetPage(pageNumber);
                Rectangle pageSize = page.GetPageSize();

                var pdfCanvas = new PdfCanvas(page);

                using var canvas = new Canvas(pdfCanvas, pageSize);

                canvas.SetFont(font)
                      .SetFontSize(70)
                      .SetFontColor(ColorConstants.RED);

                canvas.ShowTextAligned(
                new Paragraph("PAID")
                    .SetFont(font)
                    .SetFontSize(60)
                    .SetFontColor(new DeviceRgb(0, 102, 204)),
                pageSize.GetWidth() / 2,
                pageSize.GetHeight() / 2,
                pageNumber,
                TextAlignment.CENTER,
                VerticalAlignment.BOTTOM,0   
            );
            }

            pdf.Close();

            return outputStream.ToArray();
        }
    }
}