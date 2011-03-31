using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;


namespace Adventure.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        
        private IConsoleFacade console;
        private IRepository<Item> itemRepository;
        private IRepository<Room> roomRepository;

        public LookCommand(IConsoleFacade console, IRepository<Item> itemRepository, IRepository<Room> roomRepository)
        {
            this.console = console;
            this.itemRepository = itemRepository;
            this.roomRepository = roomRepository;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "look"); 
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (roomRepository)
            {
                var LookedAt = roomRepository.AsQueryable()
                    .FirstOrDefault(qq => qq.RoomName == output);
                if (LookedAt == null)
                {
                    console.WriteLine("It must be an item");
                    return;
                }
                console.WriteLine(LookedAt.Description);
            }
        }
    }
}
