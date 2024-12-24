using M13.InterviewProject.Interfaces;
using System.Collections.Generic;

namespace M13.InterviewProject.Services
{
    public class RuleService : IRuleService
    {
        private readonly Dictionary<string, string> _rules;

        public RuleService() 
        {
            _rules = new Dictionary<string, string>();
        }
        public string GetRule(string site)
        {
            lock (_rules)
            {
                return _rules.TryGetValue(site, out var rule) ?
                    rule :
                    throw new KeyNotFoundException();
            }
        }

        public void AddRule(string site, string rule)
        {
            lock (_rules)
            {
                _rules[site] = rule;
            }
        }

        public void DeleteRule(string site)
        {
            lock (_rules)
            {
                _rules.Remove(site);
            }
        }
    }
}