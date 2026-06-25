namespace SmartITSolution_Invoice.Helper
{
    public static class InvoiceHelper
    {
        public static string ConvertAmountToWords(decimal amount)
        {
            long taka = (long)amount;

            return $"{NumberToWords(taka)} Taka Only";
        }

        private static string NumberToWords(long number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += NumberToWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                string[] units =
                {
                "Zero","One","Two","Three","Four","Five",
                "Six","Seven","Eight","Nine","Ten",
                "Eleven","Twelve","Thirteen","Fourteen",
                "Fifteen","Sixteen","Seventeen",
                "Eighteen","Nineteen"
            };

                string[] tens =
                {
                "Zero","Ten","Twenty","Thirty","Forty",
                "Fifty","Sixty","Seventy","Eighty","Ninety"
            };

                if (number < 20)
                {
                    words += units[number];
                }
                else
                {
                    words += tens[number / 10];

                    if ((number % 10) > 0)
                        words += " " + units[number % 10];
                }
            }

            return words.Trim();
        }
    }
}
