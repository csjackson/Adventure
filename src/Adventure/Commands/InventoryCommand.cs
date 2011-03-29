using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class InventoryCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<Item> repository;

        public InventoryCommand(IConsoleFacade console, IRepository<Item> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "inventory"));
        }

        public void Execute(string input)
        {
            console.WriteLine("You are carrying:");
            using (repository)
            {
                var sack = repository.AsQueryable().Where(qq => qq.RoomId == 2);
                foreach (var item in sack)
                {
                    write("{0}  ", item.ItemName);
                }
               
                /* for each item in repositroy where repository.RoomId=2
                 * Writeline (item.ItemName);
                 */
            }

        }
    }
}