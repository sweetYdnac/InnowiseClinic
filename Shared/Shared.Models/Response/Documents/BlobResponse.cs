namespace Shared.Models.Response.Documents
{
    public class BlobResponse
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
