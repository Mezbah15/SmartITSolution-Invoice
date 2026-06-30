using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SmartITSolution_Invoice.Models;

namespace SmartITSolution_Invoice.Services;

public class InvoiceDocument : IDocument
{
    private readonly InvoiceViewModel _invoice;
    private readonly CompanyInfo _company;

public InvoiceDocument(
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
                                //info.Item().Text(_company.Email);
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
                .PaddingVertical(15)
                .LineHorizontal(1);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(25);

            column.Item()
                .Border(1)
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
                .Element(ComposeItemsTable);

            column.Item()
                .AlignRight()
                .Width(250)
                .Border(1)
                .Padding(20)
                .Column(total =>
                {
                    total.Item()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Text("Grand Total");

                            row.ConstantItem(120)
                                .AlignRight()
                                .Text($"{_invoice.TotalAmount:N2}");
                        });
                });

            column.Item()
                .Border(1)
                .Padding(10)
                .Column(c =>
                {
                    c.Item()
                        .Text("Amount In Words")
                        .Bold();

                    c.Item()
                        .Text(_invoice.AmountInWords)
                        .Italic();
                });

            if (!string.IsNullOrWhiteSpace(_invoice.Note))
            {
                column.Item()
                    .Border(1)
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

            //column.Item()
            //    .PaddingTop(30)
            //    .AlignRight()
            //    .Width(200)
            //    .Column(c =>
            //    {
            //        c.Item()
            //            .LineHorizontal(1);

            //        c.Item()
            //            .AlignCenter()
            //            .Text("Authorized Signature");
            //    });
        });
    }

    private void ComposeItemsTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(40);
                columns.RelativeColumn(4);
                columns.RelativeColumn(1);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle)
                    .Text("SL");

                header.Cell().Element(CellStyle)
                    .Text("DESCRIPTION");

                header.Cell().Element(CellStyle)
                    .Text("QTY");

                header.Cell().Element(CellStyle)
                    .Text("RATE");

                header.Cell().Element(CellStyle)
                    .Text("AMOUNT");
            });

            int serial = 1;

            foreach (var item in _invoice.Items)
            {
                table.Cell().Element(DataStyle)
                    .Text(serial++.ToString());

                table.Cell().Element(DataStyle)
                    .Text(item.Description);

                table.Cell().Element(DataStyle)
                    .Text(item.Quantity.ToString());

                table.Cell().Element(DataStyle)
                    .Text(item.Rate.ToString("N2"));

                table.Cell().Element(DataStyle)
                    .Text(item.Amount.ToString("N2"));
            }
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Column(column =>
        {
            column.Item()
                .PaddingTop(10)
                .LineHorizontal(1);

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
                        .Border(1)
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
                        .Border(1)
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
            .Border(1)
            .Padding(5)
            .DefaultTextStyle(x => x.FontColor(Colors.White).Bold());
    }

    private static IContainer DataStyle(IContainer container)
    {
        return container
            .Border(1)
            .Padding(5);
    }
}
