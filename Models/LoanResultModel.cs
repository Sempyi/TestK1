using System.Collections.Generic;

namespace LoanCalculator.Models
{
    /// <summary>
    /// Выво дданных
    /// </summary>
    public class LoanResultModel
    {
        public decimal MonthlyPayment { get; set; }    
        public decimal TotalOverpayment { get; set; }  
        public List<PaymentScheduleItem> Schedule { get; set; } 
    }

    /// <summary>
    /// Табличка
    /// </summary>
    public class PaymentScheduleItem
    {
        public int PaymentNumber { get; set; }         
        public DateTime PaymentDate { get; set; }      
        public decimal PrincipalPayment { get; set; }  
        public decimal InterestPayment { get; set; }   
        public decimal RemainingBalance { get; set; }  
    }
}
