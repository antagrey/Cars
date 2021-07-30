using System.Net;
using Newtonsoft.Json.Linq;

namespace Cars.Infrastructure.Adapters
{
    public class HttpResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string Content { get; set; }
        public bool HasTimedOut { get; set; }
    }
}
