namespace SmartITSolution_Invoice.Models
{
    public class InvoiceViewModel
    {
        public string InvoiceNo { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string CustomerDetails { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string InWord { get; set; } = string.Empty;

        public List<InvoiceItemViewModel> Items { get; set; } = new();
    }
    public class InvoiceItemViewModel
    {
        public string Description { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount => Quantity * Rate;
    }
}
