using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;

namespace Domain.Services
{
	public class ServiceMessage : IServiceMessage
	{
		private readonly IMessage _Message;

		public ServiceMessage(IMessage message)
		{
			_Message = message;
		}

		public async Task Add(Message message)
		{
			var validarTitulo = message.ValidarPropriedadeString(message.Titulo, "Titulo");
			if (validarTitulo)
			{
				message.DataCadastro = DateTime.Now;
				message.DataAlteracao = DateTime.Now;
				await _Message.Add(message);
			}
		}

		public async Task Update(Message message)
		{
			var validarTitulo = message.ValidarPropriedadeString(message.Titulo, "Titulo");
			if (validarTitulo)
			{
				message.DataAlteracao = DateTime.Now;
				await _Message.Update(message);
			}
		}

		public async Task<List<Message>> GetAllActive()
		{
			return await _Message.ListMessage(m => m.Ativo);
		}
	}
}
