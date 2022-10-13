using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage iMessage)
        {
            _IMessage = iMessage;
        }

        public async Task Add(Message Objeto)
        {
            var validatestitle = Objeto.validatePropertyString(Objeto.Title, "title");
            if (validatestitle)
            {
                Objeto.RegistratioDate = DateTime.Now;
                Objeto.ChangeDate = DateTime.Now;
                Objeto.Active = true;
                await _IMessage.Add(Objeto);
            }
        }

        public async Task<List<Message>> ListActiveMessage()
        {
            return await _IMessage.ListMessage(n => n.Active);
        }

        public async Task Update(Message Objeto)
        {
            var validatestitle = Objeto.validatePropertyString(Objeto.Title, "title");
            if (validatestitle)
            {
                Objeto.ChangeDate = DateTime.Now;
                await _IMessage.Update(Objeto);
            }
        }
    }
}
