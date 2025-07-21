namespace Acudir.Test.Domain.Models.DTOs
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
    }
}