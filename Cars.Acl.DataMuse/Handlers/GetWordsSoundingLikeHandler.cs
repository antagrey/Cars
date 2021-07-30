using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cars.Acl.DataMuse.Dto;
using Cars.Acl.DataMuse.Queries;
using Cars.Acl.DataMuse.Results;
using Cars.Infrastructure.Adapters;
using Cars.Infrastructure.Query;
using Newtonsoft.Json;

namespace Cars.Acl.DataMuse.Handlers
{
    public class GetWordsSoundingLikeHandler : IAsyncQueryHandler<WordsSoundingLikeRequest, SoundsLikeWordResults>
    {
        private readonly IHttpClientAdapter httpClientAdapter;

        public GetWordsSoundingLikeHandler(
            IHttpClientAdapter httpClientAdapter)
        {
            this.httpClientAdapter = httpClientAdapter;
        }

        public async Task<SoundsLikeWordResults> HandleAsync(WordsSoundingLikeRequest request)
        {
            var url = "https://api.datamuse.com/words?sl=" + request.Word;
            var httpResponseDto = await httpClientAdapter.GetAsync(url);

            if (httpResponseDto.HasTimedOut)
            {
                return new SoundsLikeWordResults(new List<string>(), false, true);
            }

            if (!httpResponseDto.IsSuccessStatusCode)
            {
                return new SoundsLikeWordResults(new List<string>(), false);
            }

            var soundsLikeWords = JsonConvert.DeserializeObject<List<SoundsLikeResultDto>>(httpResponseDto.Content);
            var words = soundsLikeWords.Select(x => x.Word).ToList();

            return new SoundsLikeWordResults(words, true);
        }
    }
}
