using Livraria.Domain.Entidades;
using Livraria.Domain.Query;
using System.Collections.Generic;

namespace Livraria.Domain.Interfaces.Respositories
{
    public interface ILivroRepository
    {
        string Inserir(Livro livro);
        void Atualizar(Livro livro);
        void Excluir(string id);
        List<LivroQueryResult> Listar();
        LivroQueryResult Obter(string id);
        bool CheckId(string id);
    }
}