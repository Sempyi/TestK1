using LoanCalculator.Models;

namespace LoanCalculator.Services
{
    public interface ILoanCalculatorService
    {
        LoanResultModel CalculateLoanInDays(LoanDailyInputModel dailyInputModel);
        LoanResultModel CalculateLoan(LoanInputModel inputModel);
    }
}