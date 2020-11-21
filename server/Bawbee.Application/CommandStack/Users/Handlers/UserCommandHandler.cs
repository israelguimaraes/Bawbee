using Bawbee.Application.Adapters;
using Bawbee.Application.CommandStack.Users.Commands;
using Bawbee.Core.Bus;
using Bawbee.Core.Commands;
using Bawbee.Core.Notifications;
using Bawbee.Core.UnitOfWork;
using Bawbee.Domain.AggregatesModel.Users;
using Bawbee.Domain.Events;
using Bawbee.Domain.Events.BankAccounts;
using Bawbee.Domain.Events.Categories;
using Bawbee.Infra.CrossCutting.Common.Security;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bawbee.Application.CommandStack.Users.Handlers
{
    public class UserCommandHandler : BaseCommandHandler,
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<LoginCommand>,
        ICommandHandler<CreateCategoryCommand>,
        ICommandHandler<CreateBankAccountCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(
            IUnitOfWork unitOfWork,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notificationHandler,
            IJwtService jwtService,
            IUserRepository userReadRepository) : base(mediator, unitOfWork, notificationHandler)
        {
            _mediator = mediator;
            _jwtService = jwtService;
            _userRepository = userReadRepository;
        }

        public async Task<CommandResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var userDatabase = await _userRepository.GetByEmail(command.Email);

            if (userDatabase != null)
            {
                await AddDomainNotification("E-mail already used");
                return CommandResult.Error();
            }

            var user = User.UserFactory.CreateNewPlataformUser(command.Name, command.LastName, command.Email, command.Password);

            await _userRepository.Add(user);

            if (await CommitTransaction())
            {
                var userRegisteredEvent = user.MapToUserRegisteredEvent();
                await _mediator.PublishEvent(userRegisteredEvent);
            }

            return CommandResult.Ok();
        }

        public async Task<CommandResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAndPassword(command.Email, command.Password);

            if (user == null)
            {
                await AddDomainNotification("E-mail or password is invalid");
                return CommandResult.Error();
            }

            var userAccessToken = _jwtService.GenerateSecurityToken(user.Id, user.Name, user.Email);

            await _mediator.PublishEvent(new UserLoggedEvent(user.Id, user.Name, user.Email));

            return CommandResult.Ok(userAccessToken);
        }

        public async Task<CommandResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _userRepository.GetCategoryByName(command.Name, command.UserId);

            if (category != null)
            {
                await _mediator.PublishEvent(new DomainNotification("Category already exists"));
                return CommandResult.Error();
            }

            category = new Category(command.Name, command.UserId);

            await _userRepository.CreateCategory(category);

            if (await CommitTransaction())
            {
                var @event = new CategoryCreatedEvent(category.Id, category.Name, category.UserId);
                await _mediator.PublishEvent(@event);
                return CommandResult.Ok();
            }

            return CommandResult.Error();
        }

        public async Task<CommandResult> Handle(CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var bankAccount = await _userRepository.GetBankAccountByName(command.Name, command.UserId);

            if (bankAccount != null)
            {
                await AddDomainNotification("Bank Account already exists");
                return CommandResult.Error();
            }

            bankAccount = new BankAccount(command.Name, command.InitialBalance, command.UserId);

            await _userRepository.CreateBankAccount(bankAccount);

            if (await CommitTransaction())
            {
                var @event = new BankAccountCreatedEvent(
                    bankAccount.Id, bankAccount.Name,
                    bankAccount.InitialBalance, bankAccount.UserId);

                await _mediator.PublishEvent(@event);
                return CommandResult.Ok();
            }

            return CommandResult.Error();
        }
    }
}
