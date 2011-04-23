using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class AliasExitCommand : BaseCommand, ICommand
    {
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;

        public AliasExitCommand(IConsoleFacade console,
            IRepository<GameObject> repository, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;

        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "aliasexit");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            string ExitName = output.TrimStart().Split('=')[0];
            string holder = output.TrimStart().Split('=')[1];
            string[] commands = holder.Split(',');

            using (repository)
            {
                var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                var exit = pObj.Location.Inventory.Where(qq => qq.Type == "Exit")
                    .FirstOrDefault(qq =>
                    qq.Name.Equals(ExitName, StringComparison.CurrentCultureIgnoreCase));
                if (exit != null)
                {


                    foreach (var member in commands)
                    {
                        exit.ExitAliases.Add(new ExitAlias() { ExitId = exit.GameObjectId, Alais = member.Trim() });
                        console.Write(" '{0}'", member);
                    }
                    console.WriteLine("Aliases for exit '{0}' now include: {1}", exit.Name, 
                        string.Join(" ", commands.Select(qq => string.Format("'{0}'", qq.Trim()))));

                }
                else
                {
                    console.WriteLine("Exit '{0}' not visible.", ExitName);
                }
            }
        }
    }
}