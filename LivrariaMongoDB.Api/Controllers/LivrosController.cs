using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Handlers;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Domain.Query;
using Livraria.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LivrariaMongoDB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {

        private readonly ILivroRepository _repository;
        private readonly LivroHandler _handler;

        public LivrosController(ILivroRepository repository, LivroHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        [Route("v1/livros")]
        public ICommandResult InserirLivro([FromBody] AdicionarLivroCommand command)
        {
            var result = _handler.Handle(command);
            return result;
        }

        [HttpPut]
        [Route("v1/livros/{livroId}")]
        public ICommandResult AtualizarLivro([FromRoute] string livroId, [FromBody] AtualizarLivroCommand command)
        {
            command.Id = livroId;

            var result = _handler.Handle(command);
            return result;
        }

        [HttpDelete]
        [Route("v1/livros/{livroId}")]
        public ICommandResult ExcluirLivro([FromRoute] string livroId, [FromBody] ExcluirLivroCommand command)
        {
            command.Id = livroId;

            var result = _handler.Handle(command);
            return result;
        }

        [HttpGet]
        [Route("v1/livros")]
        public IList<LivroQueryResult> ObterLivros()
        {
            var result = _repository.Listar();
            return result;
        }

        [HttpGet]
        [Route("v1/livros/{livroId}")]
        public LivroQueryResult ObterLivro([FromRoute] string livroId)
        {
            var result = _repository.Obter(livroId);
            return result;
        }

        [HttpGet]
        [Route("v1/livros/jogarerro")]
        public void JogarErro()
        {
            throw new Exception("Uma mensagem de erro para teste");
        }
    }
}
