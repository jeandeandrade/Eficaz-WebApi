using Core.Models;

namespace Core.Services
{
    public interface IAddressService
    {
        public Task<Address> AddAddress(string userId, Address address);
        public Task<Address> UpdateAddress(string userId, Address address);
        public Task<bool> DeleteAddress(Address address);
    }
}
