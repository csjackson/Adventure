using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class DropCommand : BaseCommand, ICommand
    {
        
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;
        

        public DropCommand(IConsoleFacade console, IRepository<GameObject> repository, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
        }
        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "drop"));
        }
           public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                 var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                var item = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.Name == output && qq.Location_Id == player.Id);
                if (item == null)
                {
                    console.WriteLine("'{0}' not in inventory.", output);
                    return;
                }
                    item.Location_Id = pObj.Location_Id;
                    console.WriteLine("You put down your {0}", item.Name);
            }
        }
    }
}
