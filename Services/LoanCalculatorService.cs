using LoanCalculator.Models;
using System;
using System.Collections.Generic;
using TestK1.Models;

namespace LoanCalculator.Services
{
    public class LoanCalculatorService : ILoanCalculatorService
    {
        public LoanResultModel CalculateLoanInDays(LoanDailyInputModel dailyInputModel)
        {
            decimal monthlyInterestRate = dailyInputModel.InterestRate * dailyInputModel.PaymentStep / 100;
            int term = dailyInputModel.LoanTerm / dailyInputModel.PaymentStep;
            decimal loanAmount = dailyInputModel.LoanAmount;

            return CalculateLoan(loanAmount, term, monthlyInterestRate);
        }

        public LoanResultModel CalculateLoan(Models.LoanInputModel inputModel)
        {
            decimal monthlyInterestRate = inputModel.InterestRate / 100 / 12;
            int term = inputModel.LoanTerm;
            decimal loanAmount = inputModel.LoanAmount;

            return CalculateLoan(loanAmount, term, monthlyInterestRate);
        }

        public LoanResultModel CalculateLoan(decimal loanAmount, int term, decimal monthlyInterestRate)
        {
            decimal monthlyPayment = loanAmount * monthlyInterestRate / (1 - (decimal)Math.Pow(1 + (double)monthlyInterestRate, -term));

            var result = new LoanResultModel
            {
                MonthlyPayment = monthlyPayment,
                Schedule = GenerateMonthlyPaymentSchedule(loanAmount, term, monthlyInterestRate, monthlyPayment)
            };

            result.TotalOverpayment = result.Schedule.Sum(s => s.InterestPayment);
            return result;
        }

        private List<PaymentScheduleItem> GenerateMonthlyPaymentSchedule(decimal loanAmount, int loanTerm, decimal monthlyInterestRate, decimal monthlyPayment)
        {
            var schedule = new List<PaymentScheduleItem>();
            decimal remainingBalance = loanAmount;

            for (int i = 1; i <= loanTerm; i++)
            {
                decimal interestPayment = remainingBalance * monthlyInterestRate;
                decimal principalPayment = monthlyPayment - interestPayment;
                remainingBalance -= principalPayment;

                schedule.Add(new PaymentScheduleItem
                {
                    PaymentNumber = i,
                    PaymentDate = DateTime.Now.AddMonths(i),
                    PrincipalPayment = principalPayment,
                    InterestPayment = interestPayment,
                    RemainingBalance = remainingBalance
                });
            }
            return schedule;
        }
    }
}