using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLogin.Repository;
using ProjetoLogin.Models;
using System.Reflection.Metadata.Ecma335;

namespace ProjetoLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly PessoaRepository pessoaRepository;
        public PessoaController()
        {
            pessoaRepository = new PessoaRepository();
        }

        [HttpGet]
        [Route("Listar")]

        public IActionResult Listar()
        {
            try
            {
                var listaPessoas = pessoaRepository.Listar();
                return Ok(listaPessoas);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Não existe nenhum cadastro para ser listado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("Consultar/{id}")]
        public IActionResult Consultar(int id)
        {
            try
            {
                var pessoa = pessoaRepository.Consultar(id);

                if (pessoa.IdPessoa != id) 
                {
                    return NotFound("Cadastro não foi encontrado!");
                }
                return Ok(pessoa);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(Models.PessoaRequest cadastroPessoa)
        {
            try
            {
                pessoaRepository.Cadastrar(cadastroPessoa);

                return Ok("Cadastro efetuado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPut]
        [Route("Editar")]
        public ActionResult Editar(Models.Pessoa pessoa)
        {
            var retornoPessoa = pessoaRepository.Consultar((int)pessoa.IdPessoa);
            if (pessoa.IdPessoa == retornoPessoa.IdPessoa)
            {
                try
                {
                    pessoaRepository.Editar(pessoa);
                    return Ok("Cadastro alterado com sucesso!");
                }
                catch (KeyNotFoundException ex)
                {
                    return BadRequest(ex.Message);
                }
            } 
            else
            {
                return NotFound("Cadastro não foi encontrado!");
            }
            
        }      

        [HttpDelete]
        [Route("Excluir")]

        public ActionResult Excluir(int Id)
        {
            try
            {
                pessoaRepository.Excluir(Id);
                return Ok("Cadastro excluído com sucesso!");
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("Cadastro não foi encontrado!");
            }
            
        }
    }
}
