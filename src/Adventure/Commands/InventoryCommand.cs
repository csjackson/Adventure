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
        private IMasterRoom master;

        public InventoryCommand(IConsoleFacade console, IRepository<GameObject> repository, IPlayer player, IMasterRoom master)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
            this.master = master;
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
                var sack = repository.AsQueryable().Where(qq => qq.GameObjectId == player.FindPlayer(repository, master));
                foreach (var item in sack)
                {
                    console.Write("{0}  ", item.Name);
                }
               
            }

        }
    }
}