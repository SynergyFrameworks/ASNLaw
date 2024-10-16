using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace LawUI.Domain.Model
{
    public class DocumentModel
    {
        public IReadOnlyList<IBrowserFile> Files { get; set; }
    }

    public class ValidationRules : AbstractValidator<DocumentModel>
    {
        public ValidationRules()
        {
            RuleFor(x => x.Files).NotEmpty().WithMessage("Please upload at least one file.");
        }
    }

}
