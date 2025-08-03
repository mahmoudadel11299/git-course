using System.ComponentModel.DataAnnotations;

namespace LapShop.Utlities
{
    public class ValidatEmailDomainAttribute:ValidationAttribute
    {
        string _alloweddomain;
        public ValidatEmailDomainAttribute(string alloweddomain)
        {
            _alloweddomain = alloweddomain;
        }
        public override bool IsValid(object? value)
        {
            string[] strings = value.ToString().Split('@');

            return strings[1].ToUpper() == _alloweddomain.ToUpper();
        }
    }
}
