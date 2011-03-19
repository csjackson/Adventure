using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class CreateItemCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<Item> repository;

        public CreateItemCommand(IConsoleFacade console, IRepository<Item> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "createitem");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                repository.Add(new Item() { ItemName = output, RoomId = 2 });
                // RoomId 2 is the inventory "Room".
            }
            console.WriteLine("You create the item \"{0}\" in your inventory.", output);
        }

    }
}
