using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Cars.Infrastructure.Dto
{
    public class HttpResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public string Content { get; set; }
        public bool HasTimedOut { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public bool IsContentValidJson { get; set; }
        public JObject JObjectContent { get; set; }
        public string JsonParsingErrorMessage { get; set; }
    }
}
