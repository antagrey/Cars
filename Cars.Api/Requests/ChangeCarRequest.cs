namespace Cars.Api.Requests
{
    public class ChangeCarRequest
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int Year { get; set; }
    }
}
