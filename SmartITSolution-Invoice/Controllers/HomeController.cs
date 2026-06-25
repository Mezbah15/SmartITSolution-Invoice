using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SmartITSolution_Invoice.Models;
using SmartITSolution_Invoice.Services;

namespace SmartITSolution_Invoice.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _env;

    public HomeController(IWebHostEnvironment env)
    {
        _env = env;
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

        if (string.IsNullOrWhiteSpace(model.InvoiceNo))
        {
            model.InvoiceNo =
                $"INV-{DateTime.Now:yyyyMMddHHmmss}";
        }

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
            new InvoiceDocument(model, company);

        var pdf = document.GeneratePdf();

        return File(
            pdf,
            "application/pdf",
            $"{model.Items.FirstOrDefault().Description}.pdf");
    }
}