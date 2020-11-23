using System.Threading.Tasks;

namespace Bawbee.Infra.Data.ReadInterfaces
{
    public interface IAdminRavenDBRepository
    {
        Task DeleteAllDocuments();
        Task CreateInitialData();
    }
}
