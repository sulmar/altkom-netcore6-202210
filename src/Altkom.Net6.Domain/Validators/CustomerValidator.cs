using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Net6.Domain.Validators
{
    // dotnet add package FluentValidation
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Salary).InclusiveBetween(1, 5000);
        }
    }
}
