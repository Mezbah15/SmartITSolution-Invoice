namespace SmartITSolution_Invoice.IServices
{
    public interface IPdfEditorService
    {
        Task<byte[]> AddPaidStampAsync(IFormFile pdfFile);
    }
}


