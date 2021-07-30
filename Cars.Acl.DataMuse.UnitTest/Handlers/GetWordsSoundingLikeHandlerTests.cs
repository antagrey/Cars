using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Cars.Acl.DataMuse.Dto;
using Cars.Acl.DataMuse.Handlers;
using Cars.Acl.DataMuse.Queries;
using Cars.Application.Providers;
using Cars.Infrastructure.Adapters;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Cars.Acl.DataMuse.UnitTest.Handlers
{
    public class GetWordsSoundingLikeHandlerTests
    {
        private readonly Fixture fixture;

        private readonly Mock<IHttpClientAdapter> httpClientAdapter;
        private readonly Mock<IUrlsProvider> urlSettings;

        private readonly GetWordsSoundingLikeHandler sut;

        public GetWordsSoundingLikeHandlerTests()
        {
            fixture = new Fixture();
            httpClientAdapter = new Mock<IHttpClientAdapter>();
            urlSettings = new Mock<IUrlsProvider>();

            sut = new GetWordsSoundingLikeHandler(httpClientAdapter.Object, urlSettings.Object);
        }

        [Fact]
        public async Task HandleAsync_WhenRequestHasTimedOut_ReturnsErrorResult()
        {
            // Arrange
            var dataMuseUrl = fixture.Create<string>();
            urlSettings.SetupGet(x => x.DataMuseUrl).Returns(dataMuseUrl);
            var request = fixture.Create<WordsSoundingLikeRequest>();
            var expectedUrl = dataMuseUrl + "?sl=" + request.Word;
            var httpResponse = new HttpResponseDto() {HasTimedOut = true};
            httpClientAdapter.Setup(x => x.GetAsync(expectedUrl, 100)).ReturnsAsync(httpResponse);

            // Act
            var result = await sut.HandleAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.True(result.HasTimedOut);
            Assert.Empty(result.Words);
        }

        [Fact]
        public async Task HandleAsync_WhenRequestHasErrored_ReturnsErrorResult()
        {
            // Arrange
            var dataMuseUrl = fixture.Create<string>();
            urlSettings.SetupGet(x => x.DataMuseUrl).Returns(dataMuseUrl);
            var request = fixture.Create<WordsSoundingLikeRequest>();
            var expectedUrl = dataMuseUrl + "?sl=" + request.Word;
            var httpResponse = new HttpResponseDto() { IsSuccessStatusCode = false };
            httpClientAdapter.Setup(x => x.GetAsync(expectedUrl, 100)).ReturnsAsync(httpResponse);

            // Act
            var result = await sut.HandleAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Empty(result.Words);
        }

        [Fact]
        public async Task HandleAsync_WhenRequestIsSuccessful_ReturnsWordResults()
        {
            // Arrange
            var dataMuseUrl = fixture.Create<string>();
            urlSettings.SetupGet(x => x.DataMuseUrl).Returns(dataMuseUrl);
            var request = fixture.Create<WordsSoundingLikeRequest>();
            var expectedUrl = dataMuseUrl + "?sl=" + request.Word;
            var dataMuseWordsResponse = fixture.CreateMany<SoundsLikeResultDto>().ToList();
            var httpResponse = new HttpResponseDto() { IsSuccessStatusCode = true, Content = JsonConvert.SerializeObject(dataMuseWordsResponse) };
            httpClientAdapter.Setup(x => x.GetAsync(expectedUrl, 100)).ReturnsAsync(httpResponse);

            // Act
            var result = await sut.HandleAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(dataMuseWordsResponse.Count, result.Words.Count);
            for (var wordIdx = 0; wordIdx < dataMuseWordsResponse.Count; wordIdx++)
            {
                Assert.Equal(dataMuseWordsResponse[wordIdx].Word, result.Words[wordIdx]);
            }
        }
    }
}
