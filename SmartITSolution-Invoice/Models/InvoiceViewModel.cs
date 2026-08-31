using System.ComponentModel.DataAnnotations;
using SmartITSolution_Invoice.Services;

namespace SmartITSolution_Invoice.Models;

public class InvoiceViewModel
{
    [Required]
    public string InvoiceNo { get; set; } = AmountConverter.GenerateInvoiceNumber();
    [Required]
    public DateTime InvoiceDate { get; set; } = DateTime.Today;

    [Required]
    public string CustomerDetails { get; set; } = string.Empty;

    public List<InvoiceItemViewModel> Items { get; set; } = new();

    public string? Note { get; set; } = string.Empty;

    public decimal TotalAmount => Items.Sum(x => x.Amount);
    public decimal? Discount { get; set; }//New Property Discount added on 31.08.26
    public decimal? AdvancePayment { get; set; }
    public decimal DueAmount => TotalAmount - ((AdvancePayment ?? 0) + (Discount ?? 0));  //Calculation updated on 31.08.26
    public string AmountInWords =>
        AmountConverter.ConvertAmountToWords(DueAmount);
}

public class InvoiceItemViewModel
{
    [Required]
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount => (Quantity * Rate);
}
