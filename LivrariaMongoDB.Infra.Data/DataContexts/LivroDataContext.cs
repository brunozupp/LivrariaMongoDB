using Livraria.Domain.Entidades;
using Livraria.Infra.Settings;
using MongoDB.Driver;
using System;

namespace Livraria.Infra.Data.DataContexts
{
    public class LivroDataContext : IDisposable
    {
        public IMongoCollection<Livro> MongoConexao { get; set; }

        public LivroDataContext(AppSettings appSettings)
        {
            try
            {
                var client = new MongoClient(appSettings.ConnectionString);

                var database = client.GetDatabase(appSettings.DatabaseName);

                MongoConexao = database.GetCollection<Livro>(appSettings.LivrosCollectionName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {

        }
    }
}