namespace GerenciamentoUsuarios.Models.Usuario
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; }
    }
}