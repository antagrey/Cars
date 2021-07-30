using System.Threading.Tasks;
using Cars.Infrastructure.Dto;

namespace Cars.Infrastructure.Adapters
{
    public interface IHttpClientAdapter
    {
        Task<HttpResponseDto> GetAsync(string url, int timeoutSeconds = HttpConstants.DefaultTimeoutSeconds);
    }
}
