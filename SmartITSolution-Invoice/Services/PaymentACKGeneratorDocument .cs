using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SmartITSolution_Invoice.Models;

namespace SmartITSolution_Invoice.Services;

public class PaymentACKGeneratorDocument : IDocument
{
    private readonly InvoiceViewModel _invoice;
    private readonly CompanyInfo _company;

public PaymentACKGeneratorDocument(
    InvoiceViewModel invoice,
    CompanyInfo company)
    {
        _invoice = invoice;
        _company = company;
    }

    public DocumentMetadata GetMetadata()
    {
        return DocumentMetadata.Default;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(25);
            page.Header().Element(ComposeHeader);

            page.Content().Element(ComposeContent);

            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Row(row =>
            {
                row.RelativeItem(1)
                    .Row(inner =>
                    {
                        if (File.Exists(_company.LogoPath))
                        {
                            inner.ConstantItem(80)
                                .Image(File.ReadAllBytes(_company.LogoPath));
                        }

                        inner.RelativeItem().AlignCenter()
                            .Column(info =>
                            {
                                info.Item().Text(_company.CompanyName)
                                    .FontSize(16)
                                    .Bold();

                                info.Item().Text(_company.Address);
                                info.Item().Text($"Phone: {_company.Phone}");
                                info.Item().Text(_company.Website);
                            });
                    });

                row.RelativeItem()
                    .AlignRight()
                    .Column(col =>
                    {
                        col.Item()
                            .Text("INVOICE")
                            .FontSize(16)
                            .Bold();

                        col.Item()
                            .Text($"{_invoice.InvoiceNo}");

                        col.Item()
                            .Text($"Date: {_invoice.InvoiceDate:dd MMM yyyy}");
                    });
            });

            column.Item()
                .PaddingVertical(15);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(25);

            column.Item()
                .Border(1).BorderColor(Colors.BlueGrey.Medium)
                .Padding(10)
                .Column(c =>
                {
                    c.Item()
                        .Text("BILL TO")
                        .Bold()
                        .FontSize(14);

                    c.Item()
                        .Text(_invoice.CustomerDetails);
                });
         column.Item()
            .AlignRight()
            .Width(250)
            .Padding(20)
            .Column(total =>
            {
                // Amount
                total.Item()
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Text("Amount In Payment").FontColor(Colors.Green.Darken2);

                        row.ConstantItem(100)
                            .AlignRight()
                            .Text(_invoice.PaymentACKViewModel?.Amount.ToString("N2", CultureInfo.InvariantCulture)).FontColor(Colors.Green.Darken2);
                    });
            });

            column.Item()
                .Border(1).BorderColor(Colors.BlueGrey.Medium)
                .Padding(10)
                .Column(c =>
                {
                    c.Item()
                        .Text("Amount In Words")
                        .Bold().FontColor(Colors.Green.Darken2);

                    c.Item()
                        .Text(_invoice.PaymentACKViewModel?.AmountInWords)
                        .Italic().FontColor(Colors.Green.Darken2);
                });

            if (!string.IsNullOrWhiteSpace(_invoice.Note))
            {
                column.Item()
                    .Border(1).BorderColor(Colors.BlueGrey.Medium)
                    .Padding(10)
                    .Column(c =>
                    {
                        c.Item()
                            .Text("Note")
                            .Bold();
                        c.Item()
                            .Text(_invoice.Note);
                    });
            }
            column.Item()
                    .PaddingLeft(180)
                    .PaddingTop(110)
                    .Column(c =>
                    {
                        c.Item()
                            .Text("THANK YOU FOR BEING WITH US");
                    });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Column(column =>
        {
            column.Item()
                .PaddingTop(10 )
                .Text("BANKING INFORMATION")
                .Bold()
                .AlignCenter();

            column.Item()
                .PaddingTop(10)
                .Row(row =>
                {
                    row.RelativeItem()
                        .Border(1).BorderColor(Colors.BlueGrey.Medium)
                        .Padding(5)
                        .Column(bank =>
                        {
                            bank.Item()
                                .Text("City Bank PLC")
                                .Bold();

                            bank.Item()
                                .Text("MD. MIZANUR RAHMAN");

                            bank.Item()
                                .Text("Account No: 2303035702001");

                            bank.Item()
                                .Text("Branch: New Market Branch, Dhaka");
                        });

                    row.RelativeItem()
                        .Border(1).BorderColor(Colors.BlueGrey.Medium)
                        .Padding(5)
                        .Column(bank =>
                        {
                            bank.Item()
                                .Text("Dutch Bangla Bank PLC")
                                .Bold();

                            bank.Item()
                                .Text("MD. MIZANUR RAHMAN");

                            bank.Item()
                                .Text("Account No: 2591510056196");

                            bank.Item()
                                .Text("Branch: Masterbari Branch, Mymensingh");
                        });
                });

            column.Item()
                .PaddingTop(10)
                .AlignCenter()
                .Text("Plant a Tree Today for a Better Tomorrow")
                .Italic();
        });
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container
            .Background(Colors.Blue.Medium)
            .Border(1).BorderColor(Colors.BlueGrey.Medium)
            .Padding(5)
            .DefaultTextStyle(x => x.FontColor(Colors.White).Bold());
    }

    private static IContainer DataStyle(IContainer container)
    {
        return container
            .Border(1).BorderColor(Colors.BlueGrey.Medium)
            .Padding(5);
    }
}
