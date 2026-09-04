using SmartITSolution_Invoice.Services;
using System.ComponentModel.DataAnnotations;

namespace SmartITSolution_Invoice.Models
{
    public class PaymentACKViewModel
    {
        public decimal Amount { get; set; }
        public string AmountInWords =>
        AmountConverter.ConvertAmountToWords(Amount);
    }
}
