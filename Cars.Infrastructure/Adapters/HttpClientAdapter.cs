using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cars.Infrastructure.Adapters
{
    [ExcludeFromCodeCoverage]
    public class HttpClientAdapter : IHttpClientAdapter
    {
        public Task<HttpResponseDto> GetAsync(string url, int timeoutSeconds = HttpConstants.DefaultTimeoutSeconds)
        {
            return SendAsync(HttpMethod.Get, url, timeoutSeconds);
        }

        private async Task<HttpResponseDto> SendAsync(
            HttpMethod httpMethod,
            string url,
            int timeoutSeconds,
            IDictionary<string, string> headers = null)
        {
            var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(timeoutSeconds) };

            using (var httpRequestMessage = new HttpRequestMessage(httpMethod, url))
            {
                HttpResponseDto httpResponseDto;

                httpResponseDto = await SendWithoutDataAsync(httpClient, httpRequestMessage);

                return httpResponseDto;
            }
        }

        private async Task<HttpResponseDto> SendWithoutDataAsync(HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            try
            {
                using (var response = await httpClient.SendAsync(httpRequestMessage))
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return new HttpResponseDto
                    {
                        Content = content,
                        StatusCode = response.StatusCode,
                        IsSuccessStatusCode = response.IsSuccessStatusCode,
                        ReasonPhrase = response.ReasonPhrase
                    };
                }
            }
            catch (TaskCanceledException)
            {
                return new HttpResponseDto
                {
                    HasTimedOut = true
                };
            }
        }
    }
}
