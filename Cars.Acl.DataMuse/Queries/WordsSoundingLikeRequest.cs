using Cars.Acl.DataMuse.Results;
using Cars.Infrastructure.Query;

namespace Cars.Acl.DataMuse.Queries
{
    public class WordsSoundingLikeRequest : IQuery<SoundsLikeWordResults>
    {
        public WordsSoundingLikeRequest(string word)
        {
            Word = word;
        }

        public string Word { get; }
    }
}
