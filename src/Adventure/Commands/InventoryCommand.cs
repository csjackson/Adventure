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
        private IRepository<GameObject> repository;
        private IPlayer player;
        

        public InventoryCommand(IConsoleFacade console, IRepository<GameObject> repository, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
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
                var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                foreach (var item in pObj.Inventory)
                {
                    console.Write("{0}  ", item.Name);
                }
               
            }

        }
    }
}