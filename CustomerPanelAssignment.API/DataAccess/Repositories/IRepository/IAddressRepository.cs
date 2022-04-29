using CustomerPanelAssignment.API.Models;

namespace DataAccess.Repositories.IRepository
{
    public interface IAddressRepository : IRepository<Address>
    {
        void Update(Address obj);
    }
}
