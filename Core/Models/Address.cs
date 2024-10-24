namespace Core.Models
{
    public class Address
    {
        public string Id { get; set; }
        public User User { get; set; }
        public string NomeRua { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string NumeroResidencia { get; set; }

        private Address() { }

        public Address(User user, string nomeRua, string bairro, string cep, string complemento, string cidade, string numeroResidencia)
        {
            User = user;
            NomeRua = nomeRua;
            Bairro = bairro;
            Cep = cep;
            Complemento = complemento;
            Cidade = cidade;
            NumeroResidencia = numeroResidencia;
        }
    }
}
