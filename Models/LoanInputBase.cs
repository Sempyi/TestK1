using System.ComponentModel.DataAnnotations;

namespace TestK1.Models
{
    public abstract class LoanInputBase
    {
        public decimal LoanAmount { get; set; }

        public int LoanTerm { get; set; }

        public decimal InterestRate { get; set; }

        public bool ValidateBase(out List<string> errors)
        {
            errors = new List<string>();

            if (LoanAmount < 1000 || LoanAmount > 10000000)
            {
                errors.Add("Сумма займа должна быть в диапазоне от 1000 до 10 000 000");
            }

            if (LoanTerm < 1 || LoanTerm > 36500)
            {
                errors.Add("Срок займа должен быть в диапазоне от 1 до 36500 дней");
            }

            if (LoanTerm % 1 != 0)
            {
                errors.Add("Срок займа должен быть целым числом");
            }

            if (InterestRate < 0.001m || InterestRate > 100m)
            {
                errors.Add("Процентная ставка должна быть в диапазоне от 0.001% до 100%");
            }         

            return errors.Count == 0;
        }
    }
}