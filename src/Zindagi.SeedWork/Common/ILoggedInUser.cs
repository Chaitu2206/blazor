using System.Threading.Tasks;

namespace Zindagi.SeedWork
{
    public interface ILoggedInUser
    {
        Task<VendorId> GetId();
        Task<long> GetSerialNumber();
        Task<Result<OpenIdUser>> GetUser();
    }
}
