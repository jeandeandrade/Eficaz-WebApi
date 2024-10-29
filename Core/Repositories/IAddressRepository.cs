using Core.Models;

namespace Core.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> AddAddress(Address address);
        Task<Address> UpdateAddress(Address address);
        Task<bool> DeleteAddress(string addressId);

    }
}
