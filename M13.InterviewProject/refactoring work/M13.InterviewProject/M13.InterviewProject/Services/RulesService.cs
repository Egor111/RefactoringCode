using M13.InterviewProject.Interfaces;
using System.Collections.Generic;

namespace M13.InterviewProject.Services
{
    public class RulesService : IRulesService
    {
        private readonly Dictionary<string, string> _rules = new();

        public void AddRule(string site, string rule)
        {
            lock (_rules)
            {
                _rules[site] = rule;
            }
        }

        public string GetRule(string site)
        {
            lock (_rules)
            {
                return _rules.TryGetValue(site, out var rule) ? rule : throw new KeyNotFoundException();
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