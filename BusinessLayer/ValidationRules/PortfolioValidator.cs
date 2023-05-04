using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PortfolioValidator: AbstractValidator<Portfolio>
    {
        /// <summary>
        /// Oluşturulacak olan fluent validation kuralları constructor içinde tanımlanacak. Formsta iflerle yaptığın validasyonlar gibi.
        /// </summary>
        public PortfolioValidator() 
        {
            // RuleFor(x=>x.Name) : x.'in name'i için kural oluşturuyoruz. Ne kuralı? NotEmpty kuralı. Ve bu kuralı mesajla birlikte oluşturuyoruz. Boş geçilirse aşağıdaki meaj veriliyor.
            RuleFor(x => x.Name).NotEmpty().WithMessage("Proje adı boş geçilemez.");
            RuleFor(x=>x.ImageUrl).NotEmpty().WithMessage("Proje görsel alanı boş geçilemez.");
            RuleFor(x => x.Name).MinimumLength(5).WithMessage("Proje adı en az 5 karakterden oluşmalıdır.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Proje adı en falza 100 karakterden oluşmalıdır.");
            
        }
    }
}
