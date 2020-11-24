using Bawbee.Core.Commands;

namespace Bawbee.Application.CommandStack.Admin.Commands
{
    public class RecreateDatabaseCommand : Command
    {
        public override bool IsValid()
        {
            return true;
        }
    }
}
