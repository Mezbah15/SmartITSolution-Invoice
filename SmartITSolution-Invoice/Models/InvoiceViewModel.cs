using System.ComponentModel.DataAnnotations;
using SmartITSolution_Invoice.Helper;

namespace SmartITSolution_Invoice.Models;

public class InvoiceViewModel
{
    [Required]
    public string InvoiceNo { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; } = DateTime.Today;

    [Required]
    public string CustomerDetails { get; set; } = string.Empty;

    public List<InvoiceItemViewModel> Items { get; set; } = new();

    public string? Note { get; set; } = string.Empty;

    public decimal TotalAmount => Items.Sum(x => x.Amount);

    public string AmountInWords =>
        InvoiceHelper.ConvertAmountToWords(TotalAmount);
}

public class InvoiceItemViewModel
{
    [Required]
    public string Description { get; set; } = string.Empty;

public decimal Quantity { get; set; }

    public decimal Rate { get; set; }

    public decimal Amount => Quantity * Rate;
}
