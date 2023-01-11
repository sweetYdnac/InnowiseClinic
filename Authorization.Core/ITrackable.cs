namespace Authorization.Core
{
    public interface ITrackable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
