using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class GetCommand : BaseCommand, ICommand
    {
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;

        public GetCommand(IConsoleFacade console, IRepository<GameObject> repository, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
        }
        public bool IsValid(string input)
        {
            var determinator = ((IsFirstWord(input, "get")) || (IsFirstWord(input, "take")));
            return determinator;
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                var item = repository.AsQueryable().FirstOrDefault(qq => qq.Name.Equals(output, StringComparison.CurrentCultureIgnoreCase) && 
                        (qq.Location_Id == pObj.Location_Id || qq.Location_Id == player.Id));
                if (item == null)
                {
                    console.WriteLine("I do not see that, here.");
                    return;
                }
                else if (item.Location == pObj)
                {
                    console.WriteLine(("You already have that."));
                    return;
                }
                item.Location = pObj;
                console.WriteLine("You pick up {0}", item.Name);
            }
        }
    }
}
