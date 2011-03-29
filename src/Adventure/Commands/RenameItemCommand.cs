using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class RenameItemCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<Item> repository;

        public RenameItemCommand(IConsoleFacade console, IRepository<Item> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "item") && (input.Contains(".name")));
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            var name = output.Split('.')[0];
            var itemNewName = string.Join("=", input.Trim().Split('=').Skip(1)).Trim();

            using (repository)
            {
                var item = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.ItemName == name);
                if (item == null)
                {
                    console.WriteLine("I do not recognize \"{0}\".", item);
                    return;
                }
                console.WriteLine("The item's name has been changed from {0} to {1}.",
                    item.ItemName, itemNewName);

                item.ItemName = itemNewName;
            }

        }

    }
}
