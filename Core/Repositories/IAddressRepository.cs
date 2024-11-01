using Core.Models;

namespace Core.Repositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetAddressByIdAsync(string addressId);
        Task<Address> AddAddressAsync(Address address);
        Task<Address> UpdateAddressAsync(string addressId, Address address);
        Task<bool> DeleteAddressAsync(string addressId);
    }
}
