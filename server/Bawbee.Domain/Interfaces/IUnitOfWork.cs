namespace Bawbee.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void CommitTransaction();
    }
}
