using HtmlAgilityPack;
using M13.InterviewProject.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace M13.InterviewProject.Services
{
    public class WebPageService : IWebPageService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WebPageService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetPageTextAsync(string url, string xpath)
        {
            var response = await _httpClientFactory.CreateClient("CustomClient").GetStringAsync(url);

            var document = new HtmlDocument();
            document.LoadHtml(response);

            var nodes = document.DocumentNode.SelectNodes(xpath);
            return nodes == null ? string.Empty : string.Join("\n", nodes.Select(n => n.InnerText));
        }
    }
}