using AutoMapper;
using Livraria.Domain.Commands.Inputs;
using Livraria.Domain.Entidades;
using Livraria.Domain.Query;

namespace LivrariaMongoDB.Api.Configuracoes
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AdicionarLivroCommand, Livro>();
            CreateMap<AtualizarLivroCommand, Livro>();

            CreateMap<Livro, LivroQueryResult>();
        }
    }
}
