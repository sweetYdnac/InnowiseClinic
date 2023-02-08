namespace Shared.Models.Response
{
    public class ValidationFailedResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public IEnumerable<Dictionary<string, string[]>> Errors { get; set; }
    }
}
