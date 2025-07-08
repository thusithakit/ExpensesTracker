using ExpensesTracker.Data.Services;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpensesTracker.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IExpensesService expensesService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IExpensesService _expensesService = expensesService;

        public async Task<IActionResult> Index()
        {
            var expenses = await _expensesService.GetAll();
            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if ((ModelState.IsValid))
            {
                await _expensesService.Add(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var data = await _expensesService.Edit(Id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, Expense expense)
        {
            if(await _expensesService.Edit(Id,expense))
            {
                return RedirectToAction("Index");
            }
            return View(expense);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Id) {
            await _expensesService.Delete(Id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetChart()
        {
            var data = _expensesService.GetChartData();
            return Json(data);
        }
    }
}
