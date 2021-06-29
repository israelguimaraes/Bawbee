using Bawbee.Application.Bus;

namespace Bawbee.Application.UseCases.Categories.CreateCategory
{
    public class CreateCategoryCommand : BaseCommand
    {
        public string Name { get; }
        public int UserId { get; }

        public CreateCategoryCommand(string name, int userId)
        {
            Name = name;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCategoryCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
