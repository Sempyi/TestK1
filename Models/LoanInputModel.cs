using System.ComponentModel.DataAnnotations;
using TestK1.Models;

namespace LoanCalculator.Models
{
    /// <summary>
    /// Сбор данных
    /// </summary>
    public class LoanInputModel : LoanInputBase
    {
        public bool Validate(out List<string> errors)
        {
            errors = new List<string>();
                        
            if (!base.ValidateBase(out List<string> baseErrors))
            {
                errors.AddRange(baseErrors);
            }
            return errors.Count == 0;
        }
    }
}
