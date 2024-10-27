using Core.Models;

namespace Core.Repositories
{
    public interface IAddressRepository
    {
        public Task<Address> AddAddress(string userId, Address address);
        public Task<Address> UpdateAddress(string userId, Address address);
        public Task<bool> DeleteAddress(Address address);

    }
}
