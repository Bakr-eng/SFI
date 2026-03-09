using SFI.Models;
using SFI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Services
{
    internal class MessageService
    {
        // Singleton Pattern

        private static MessageService _instance;
        public static MessageService Instance
        {
            get
            {
                if(_instance == null )
                    _instance = new MessageService();

                return _instance;

            }
        }

        private readonly IMeddelandeRepository _medRepo;
        private readonly IPersonRepository _personRepo;

        private MessageService()
        {
            _medRepo = MeddelandeRepository.Instance;
            _personRepo = PersonRepository.Instance;
        }


        // Facade Pattern
        // Denna metod döljer all logik för att hämta meddelanden,
        // fylla i avsändarens namn och hantera roller.

        public async Task<List<Meddelande>> GetMessagesForUser(Person user)
        {
            List<Meddelande> messages;

            if (user.Roll == "Elev")
            {
                messages = await _medRepo.GetMessagesForStudent(user.Id, user.KlassId.Value);
            }
            else // Lärare
            {
                messages = await _medRepo.GetMessagesForTeacher(user.Id, user.KlassId.Value);
            }

            foreach (var m in messages)
            {
                var sender = await _personRepo.GetById(m.AvsändareId);
                m.AvsändareNamn = sender?.Name ?? "Okänd";
            }

            return messages;
        }

    }
}
