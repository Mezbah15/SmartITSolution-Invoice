using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SmartITSolution_Invoice.IServices;
using SmartITSolution_Invoice.Models;
using SmartITSolution_Invoice.Services;

namespace SmartITSolution_Invoice.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly IPdfEditorService _pdfService;

    public HomeController(IWebHostEnvironment env, IPdfEditorService pdfService)
    {
        _env = env;
        _pdfService = pdfService;
    }

    public IActionResult Index()
    {
        return View(new InvoiceViewModel());
    }

    [HttpPost]
    public IActionResult Invoice(InvoiceViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var company = new CompanyInfo
        {
            CompanyName = "SMART IT SOLUTION",
            Address = "Jigatola, Dhanmondi, Dhaka",
            Phone = "+8801309445401",
            Website = "www.smartitsolution.net",
            LogoPath = Path.Combine(
                _env.WebRootPath,
                "images",
                "logo.jpg")
        };

        var document =
            new InvoiceGeneratorDocument(model, company);

        var pdf = document.GeneratePdf();
        TempData["Success2"] = "Download Successful";
        return File(
            pdf,
            "application/pdf",
            $"{model.Items.FirstOrDefault().Description}.pdf");
    }

    [HttpPost]
    public async Task<IActionResult> UploadPdf(IFormFile pdfFile)
    {
        if (pdfFile == null || pdfFile.Length == 0)
        {
            return BadRequest("Please select a PDF.");
        }

        var pdf = await _pdfService.AddPaidStampAsync(pdfFile);

        TempData["Success"] = "Download Successful";
        return File(
            pdf,
            "application/pdf",
            $"Paid_{pdfFile.FileName}");
    }
}