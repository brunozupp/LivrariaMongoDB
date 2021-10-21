using Flunt.Notifications;
using Livraria.Infra.Interfaces.Commands;
using System;
using System.Text.Json.Serialization;

namespace Livraria.Domain.Commands.Inputs
{
    public class ExcluirLivroCommand : Notifiable, ICommandPadrao
    {
        [JsonIgnore]
        public string Id { get; set; }

        public bool ValidarCommad()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                    AddNotification("Id", "Id é um campo obrigatório");

                return Valid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
