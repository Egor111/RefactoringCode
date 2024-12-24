using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using M13.InterviewProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M13.InterviewProject.Controllers
{
    /// <summary>
    /// sample usage:
    /// 1) check xpath rule '//span' for site wikipedia.org: http://localhost:56660/api/rules/add?site=wikipedia.org&rule=%2f%2fspan
    /// 2) check rule is saved:  http://localhost:56660/api/rules/get?site=wikipedia.org
    /// 3) view text parsed by rule: http://localhost:56660/api/rules/test?page=wikipedia.org
    /// 4) view errors in text: http://localhost:56660/api/spell/errors?page=wikipedia.org
    /// 5) view errors count in text: http://localhost:56660/api/spell/errorscount?page=wikipedia.org
    /// </summary>
    [ApiController]
    [Route("api")]
    public class ValuesController : Controller
    {
        private readonly IRuleService _ruleService;
        private readonly IWebPageService _webPageService;
        private readonly ISpellCheckerService _spellCheckerService;

        public ValuesController(
            IRuleService ruleService,
            IWebPageService webPageService,
            ISpellCheckerService spellCheckerService)
        {
            _ruleService = ruleService ?? throw new ArgumentNullException(nameof(ruleService));
            _webPageService = webPageService ?? throw new ArgumentNullException(nameof(webPageService));
            _spellCheckerService = spellCheckerService ?? throw new ArgumentNullException(nameof(spellCheckerService));
        }

        [HttpGet("rules/add")]
        public IActionResult AddRule(string site, string rule)
        {            
            _ruleService.AddRule(site, rule);
            return Ok();
        }

        [HttpGet("rules/get")]
        public IActionResult GetRule(string site)
        {
            try
            {
                var rule = _ruleService.GetRule(site);
                return Ok(rule);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("rules/delete")]
        public IActionResult DeleteRule(string site)
        {
            _ruleService.DeleteRule(site);
            return Ok();
        }

        [HttpGet("rules/test")]
        public async Task<IActionResult> Test(string page, string rule = null)
        {          
            var xpath = rule ?? _ruleService.GetRule(CreateUrl(page));

            var text = await _webPageService.GetPageTextAsync("http://" + page, xpath);
            return Ok(text);
        }

        [HttpGet("spell/errors")]
        public async Task<IActionResult> SpellErrors(string page)
        {
            var xpath = _ruleService.GetRule(CreateUrl(page));

            var text = await _webPageService.GetPageTextAsync("http://" + page, xpath);
            var errors = await _spellCheckerService.GetErrorsAsync(text);

            return Ok(errors);
        }

        [HttpGet("spell/errorscount")]
        public async Task<IActionResult> SpellErrorsCount(string page)
        {     
            var xpath = _ruleService.GetRule(CreateUrl(page));

            var text = await _webPageService.GetPageTextAsync("http://" + page, xpath);
            var count = await _spellCheckerService.GetErrorCountAsync(text);

            return Ok(count);
        }

        /// <summary>
        /// Создание Url
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private string CreateUrl(string page) 
        {
            return new Uri("http://" + page).Host;
        }
    }
}