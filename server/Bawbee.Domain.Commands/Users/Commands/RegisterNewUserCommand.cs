using Bawbee.Domain.Core.Commands;

namespace Bawbee.Domain.Commands.Users.Commands
{
    public class RegisterNewUserCommand : Command
    {
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Password { get; }

        public RegisterNewUserCommand(string name, string lastName, string email, string password)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
