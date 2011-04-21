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

        }
    }
}