using System.ComponentModel.DataAnnotations;
using TestK1.Models;

namespace LoanCalculator.Models
{
    public class LoanDailyInputModel : LoanInputBase
    {
        public int PaymentStep { get; set; }

        public bool Validate(out List<string> errors)
        {
            errors = new List<string>();
                        
            if (!base.ValidateBase(out List<string> baseErrors))
            {
                errors.AddRange(baseErrors);
            }

            if (PaymentStep % 1 != 0)
            {
                errors.Add("Шаг платежа займа должен быть целым числом");
            }

            if (PaymentStep > LoanTerm)
            {
                errors.Add("Щаг платежа должен быть меньше срока займа");
            }

            if (PaymentStep < 1 || PaymentStep > 365)
            {
                errors.Add("Шаг платежа должен быть в диапазоне от 1 до 365 дней");
            }

            if (LoanTerm % PaymentStep != 0)
            {
                errors.Add("Щаг платежа должен быть кратен сроку займа");
            }
                   

            return errors.Count == 0;
        }
    }
}