namespace Core.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? ImageUrl { get; set; }
        public List<Address> Enderecos { get; set; }

        public User()
        {
            Enderecos = new List<Address>();
        }

        public User(string id, string nome, string apelido, string cpf, DateTime dataNascimento, string genero, string telefone, string email, string senha)
            : this()
        {
            Id = id;
            Nome = nome;
            Apelido = apelido;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Genero = genero;
            Telefone = telefone;
            Email = email;
            Senha = senha;
        }

        public User(string nome, string apelido, string cpf, DateTime dataNascimento, string genero, string telefone, string email, string senha)
            : this()
        {
            Nome = nome;
            Apelido = apelido;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Genero = genero;
            Telefone = telefone;
            Email = email;
            Senha = senha;
        }
    }
}
