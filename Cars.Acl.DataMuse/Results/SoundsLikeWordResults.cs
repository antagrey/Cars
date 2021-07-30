using System.Collections.Generic;
using Cars.Infrastructure.Query;

namespace Cars.Acl.DataMuse.Results
{
    public class SoundsLikeWordResults : QueryResult
    {
        public SoundsLikeWordResults(List<string> words, bool isSuccessful = true, bool hasTimedOut = false)
        {
            Words = words;
            Success = isSuccessful;
            HasTimedOut = hasTimedOut;
        }

        public List<string> Words { get; }

        public bool HasTimedOut { get; }

        public bool Success { get; }
    }
}
