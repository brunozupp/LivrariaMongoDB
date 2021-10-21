using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Respositories;
using Livraria.Domain.Query;
using Livraria.Infra.Data.DataContexts;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Livraria.Infra.Data.Respositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LivroDataContext _dataContext;
        //private readonly IMapper _mapper;

        public LivroRepository(LivroDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Atualizar(Livro livro)
        {
            _dataContext.MongoConexao.ReplaceOne(book => book.Id == livro.Id, livro);
        }

        public bool CheckId(string id)
        {
            return _dataContext.MongoConexao.Find(e => e.Id == id).Any();
        }

        public void Excluir(string id)
        {
            _dataContext.MongoConexao.DeleteOne(livro => livro.Id == id);
        }

        public string Inserir(Livro livro)
        {
            _dataContext.MongoConexao.InsertOne(livro);
            return livro.Id;
        }

        public List<LivroQueryResult> Listar()
        {
            var livros = _dataContext.MongoConexao.Find(e => true).ToList();

            var livrosQueryResult = new List<LivroQueryResult>();

            livros.ForEach(livro =>
            {
                livrosQueryResult.Add(new LivroQueryResult()
                {
                    Autor = livro.Autor,
                    Edicao = livro.Edicao,
                    Imagem = livro.Imagem,
                    Isbn = livro.Isbn,
                    Nome = livro.Nome,
                    Id = livro.Id
                });
            });

            return livrosQueryResult;
        }

        public LivroQueryResult Obter(string id)
        {
            var livro = _dataContext.MongoConexao.Find(e => e.Id == id).FirstOrDefault();

            if (livro == null) return null;

            return new LivroQueryResult()
            {
                Autor = livro.Autor,
                Edicao = livro.Edicao,
                Imagem = livro.Imagem,
                Isbn = livro.Isbn,
                Nome = livro.Nome,
                Id = livro.Id
            };
        }
    }
}