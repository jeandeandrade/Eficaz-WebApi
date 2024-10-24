using Core.Models;

namespace Core.Repositories
{
    public interface IAddressRepository
    {
        public Task<Address> SaveAddress(Address address);
    }
}
