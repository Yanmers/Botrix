using Botrix.Models;
using Botrix.Services;
using Microsoft.AspNetCore.Mvc;

namespace Botrix.Controllers
{
    public class AdminController : Controller
    {


        private readonly RuleEngineService _ruleEngine;

        public AdminController(RuleEngineService ruleEngine)
        {
            _ruleEngine = ruleEngine;
        }
        public IActionResult Stats()
        {
            return View();
        }

        public IActionResult Index()
        {
            var rules = _ruleEngine.GetAllRules();
            return View(rules);
        }


        [HttpPost]
        public IActionResult Create(ResponseRule rule)
        {
            _ruleEngine.AddRule(rule);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(ResponseRule rule)
        {
            _ruleEngine.UpdateRule(rule);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string keyword)
        {
            _ruleEngine.DeleteRule(keyword);
            return RedirectToAction("Index");
        }
    }
}
