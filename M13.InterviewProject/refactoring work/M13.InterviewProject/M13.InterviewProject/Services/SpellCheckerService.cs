using M13.InterviewProject.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using M13.InterviewProject.Models;
using System.Linq;

namespace M13.InterviewProject.Services
{
    public class SpellCheckerService : ISpellCheckerService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SpellCheckerService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<string>> GetErrorsAsync(string text)
        {
            var response = await _httpClientFactory.CreateClient("CustomClient").GetStringAsync($"http://speller.yandex.net/services/spellservice.json/checkText?text={WebUtility.UrlEncode(text)}");

            var errors = JsonConvert.DeserializeObject<SpellerErrorsModel[]>(response);
            return errors.Select(e => e.Word);
        }

        public async Task<int> GetErrorCountAsync(string text)
        {
            var errors = await GetErrorsAsync(text);
            return errors.Count();
        }
    }
}