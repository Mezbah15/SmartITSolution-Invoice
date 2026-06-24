using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartITSolution_Invoice.Models;

namespace SmartITSolution_Invoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Invoice()
        {
            //var model = new InvoiceViewModel
            //{
            //    Participant = participant
            //};

            //var imageUrl = $"https://storage.hki-2025.com/ski-images/{participant.Image}";
            //using (var client = new HttpClient())
            //{
            //    var imageBytes = await client.GetByteArrayAsync(imageUrl);
            //    var base64Image = Convert.ToBase64String(imageBytes);
            //    model.Base64Image = base64Image;
            //}
            //var pdf = new ViewAsPdf("_AdmitPartial", model, null, true)
            //{
            //    FileName = $"{model.Participant.InvoiceNumber}.pdf",
            //    PageSize = Rotativa.AspNetCore.Options.Size.A4,
            //    PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 10, 10),
            //    CustomSwitches = "--encoding utf-8 --disable-smart-shrinking --load-media-error-handling ignore --load-error-handling ignore"
            //};

            //return pdf;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
