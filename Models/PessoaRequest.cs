﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProjetoLogin.Models
{
    public class PessoaRequest
    {

        public string Nome { get; set; }

        public string Cidade { get; set; }

        public string Email { get; set; }

    }
}  
