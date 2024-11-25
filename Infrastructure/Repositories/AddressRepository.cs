using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EficazDbContext _context;

        public AddressRepository(EficazDbContext context)
        {
            _context = context;
        }

        public async Task<Address?> GetAddressByIdAsync(string addressId)
        {
            return await _context.Address.FirstOrDefaultAsync(a  => a.Id == addressId);
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<Address> UpdateAddressAsync(string addressId, Address address)
        {
            var existingAddress = await _context.Address.FirstOrDefaultAsync(a => a.Id == addressId);

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
            existingAddress.User = address.User;

            await _context.SaveChangesAsync();

            return existingAddress;
        }

        public async Task<bool> DeleteAddressAsync(string addressId)
        {
            var address = await _context.Address.FindAsync(addressId);
            if (address == null)
            {
                throw new ArgumentException("Endereço não encontrado");
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
