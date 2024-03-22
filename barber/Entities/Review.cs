namespace barber.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int fk_ClientId { get; set; }
        public int fk_BarberId { get; set; }
        public string? Text { get; set; }
        public float Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
