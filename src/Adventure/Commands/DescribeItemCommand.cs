using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class DescribeItemCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<Item> repository;

        public DescribeItemCommand(IConsoleFacade console, IRepository<Item> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "item") && (input.Contains(".desc")));
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            var name = output.Split('.')[0];
            var itemDescription = string.Join("=", input.Trim().Split('=').Skip(1));

            using (repository)
            {
                var item = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.ItemName == name);
                if (item == null)
                {
                    console.WriteLine("I do not recognize \"{0}\".", item);
                    return;
                }
                item.ItemDescription = itemDescription;
            }

        }

    }
}
