using LoanCalculator.Models;
using LoanCalculator.Services;
using Microsoft.AspNetCore.Mvc;

public class LoanController : Controller
{
    private readonly ILoanCalculatorService _loanCalculatorService;

    public LoanController(ILoanCalculatorService loanCalculatorService)
    {
        _loanCalculatorService = loanCalculatorService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Calculate(LoanInputModel model)
    {
        List<string> errors;
        if (model.Validate(out errors))
        {
            var result = _loanCalculatorService.CalculateLoan(model);
            return View("Result", result);
        }
        else
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            return View("Index", model);
        }
    }

    [HttpGet]
    public IActionResult DailyLoan()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CalculateDaily(LoanDailyInputModel model)
    {
        List<string> errors;
        if (model.Validate(out errors))
        {
            LoanResultModel result = _loanCalculatorService.CalculateLoanInDays(model);
            return View("Result", result);
        }
        else
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
            return View("DailyLoan", model);
        }
    }
}