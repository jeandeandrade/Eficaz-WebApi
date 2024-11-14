using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;

        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
        }

        public async Task<Address> AddAddress(string userId, Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            address.Id = Guid.NewGuid().ToString();
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentNullException("UserId inválido");
            }

            address.User = user;
            await _addressRepository.AddAddressAsync(address);

            return address;
        }

        public async Task<Address> UpdateAddress(string userId, Address address)
        {
            if (address == null || string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(address));
            }

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }

            var existingAddress = user.Enderecos.FirstOrDefault(a => a.Id == address.Id);
            if (existingAddress == null)
            {
                throw new ArgumentException("Endereço não encontrado");
            }

            existingAddress.NomeRua = address.NomeRua;
            existingAddress.Bairro = address.Bairro;
            existingAddress.Cep = address.Cep;
            existingAddress.Complemento = address.Complemento;
            existingAddress.Cidade = address.Cidade;
            existingAddress.NumeroResidencia = address.NumeroResidencia;

            await _addressRepository.UpdateAddressAsync(existingAddress.Id, existingAddress);

            return existingAddress;
        }

        public async Task<bool> DeleteAddress(string addressId)
        {
            if (string.IsNullOrEmpty(addressId))
            {
                throw new ArgumentNullException("AddressID não pode ser nulo");
            }

            var address = await _addressRepository.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                throw new ArgumentException("Endereço não existe");
            }

            await _addressRepository.DeleteAddressAsync(addressId);

            return true;
        }
    }
}
