using Microsoft.AspNetCore.Mvc;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace NafaGoldTry.PdfService.Controllers
{
    [ApiController]
    [Route("pdf")]
    public class PdfController : ControllerBase
    {
        private readonly IConverter _converter;

        public PdfController(IConverter converter)
        {
            _converter = converter;
        }

        // THIS MATCHES YOUR EXISTING PdfService.cs
        [HttpPost]
        public IActionResult Generate([FromBody] PdfRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Html)) 
                return BadRequest("HTML content missing");

            var document = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = request.Html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            var pdf = _converter.Convert(document);

            return File(pdf, "application/pdf", "document.pdf");
        }
    }

    public class PdfRequest
    {
        public string Html { get; set; }
    }
}
