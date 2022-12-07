using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjetoLogin.Models
{
    public class Pessoa
    {
        public int ?IdPessoa { get; set; }

        public string ?Nome { get; set; }

        public string ?Cidade { get; set; }

        public string Email { get; set; }

    }
}  
