namespace Core.DTOs
{
    public class UserDTO
    {
        public string Nome { get; set; }
        public string? Apelido { get; set; } 
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Genero { get; set; } 
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<AddressDTO> Enderecos { get; set; }
    }
}
