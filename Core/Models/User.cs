namespace Core.Models
{
    public class User
    {
        public required string Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        private User() { }

        public User(string id, string nome, string apelido, string cpf, DateTime dataNascimento, string genero, string telefone, string email, string senha)
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
