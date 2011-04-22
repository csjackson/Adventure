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
        private IRepository<ExitAlias> aliases;
        public AliasExitCommand(IConsoleFacade console,
            IRepository<GameObject> repository, IPlayer player, IRepository<ExitAlias> aliases)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
            this.aliases = aliases;

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
                var exit = repository.AsQueryable().FirstOrDefault(qq => 
                    qq.Name.Equals(ExitName, StringComparison.CurrentCultureIgnoreCase));
                if ((exit != null) && (pObj.Location == exit.Location))
                {
                    using (aliases)
                    {
                        console.WriteLine("Aliases for exit '{0}' now include: ", exit.Name);

                        foreach (var member in commands)
                        {
                            aliases.Add(new ExitAlias() { ExitId = exit.GameObjectId, Alais = member.Trim() });
                            console.Write(" '{0}'", member);
                        }
                    }
                }
                else
                {
                    console.WriteLine("Exit '{0}' not visible.", ExitName);
                }
            }
        }
    }
}