namespace Cars.Api.Requests
{
    public class AddCarRequest
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int Year { get; set; }
    }
}
