using LoanCalculator.Models;
using System;
using System.Collections.Generic;
using TestK1.Models;

namespace LoanCalculator.Services
{
    public enum PaymentStepType
    {
        Days,
        Months
    }

    public class LoanCalculatorService : ILoanCalculatorService
    {
        public LoanResultModel CalculateLoanInDays(LoanDailyInputModel dailyInputModel)
        {
            decimal dailyInterestRate = dailyInputModel.InterestRate * dailyInputModel.PaymentStep / 100;
            int term = dailyInputModel.LoanTerm / dailyInputModel.PaymentStep;
            decimal loanAmount = dailyInputModel.LoanAmount;

            return CalculateLoan(loanAmount, term, dailyInterestRate, PaymentStepType.Days, dailyInputModel.PaymentStep);
        }

        public LoanResultModel CalculateLoan(Models.LoanInputModel inputModel)
        {
            decimal monthlyInterestRate = inputModel.InterestRate / 100 / 12;
            int term = inputModel.LoanTerm;
            decimal loanAmount = inputModel.LoanAmount;

            return CalculateLoan(loanAmount, term, monthlyInterestRate, PaymentStepType.Months, 1);
        }

        public LoanResultModel CalculateLoan(decimal loanAmount, int term, decimal interestRate, PaymentStepType stepType, int paymentStep)
        {
            decimal payment = loanAmount * interestRate / (1 - (decimal)Math.Pow(1 + (double)interestRate, -term));

            var result = new LoanResultModel
            {
                MonthlyPayment = payment,
                Schedule = GeneratePaymentSchedule(loanAmount, term, interestRate, payment, stepType, paymentStep)
            };

            result.TotalOverpayment = result.Schedule.Sum(s => s.InterestPayment);
            return result;
        }

        private List<PaymentScheduleItem> GeneratePaymentSchedule(decimal loanAmount, int loanTerm, decimal interestRate, decimal payment, PaymentStepType stepType, int paymentStep)
        {
            var schedule = new List<PaymentScheduleItem>();
            decimal remainingBalance = loanAmount;

            for (int i = 1; i <= loanTerm; i++)
            {
                decimal interestPayment = remainingBalance * interestRate;
                decimal principalPayment = payment - interestPayment;
                remainingBalance -= principalPayment;

                DateTime paymentDate;
                if (stepType == PaymentStepType.Days)
                {
                    paymentDate = DateTime.Now.AddDays(i * paymentStep);
                }
                else
                {
                    paymentDate = DateTime.Now.AddMonths(i * paymentStep);
                }

                schedule.Add(new PaymentScheduleItem
                {
                    PaymentNumber = i,
                    PaymentDate = paymentDate,
                    PrincipalPayment = principalPayment,
                    InterestPayment = interestPayment,
                    RemainingBalance = remainingBalance
                });
            }
            return schedule;
        }
    }
}