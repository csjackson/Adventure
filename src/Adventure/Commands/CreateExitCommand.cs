using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class CreateExitCommand : BaseCommand, ICommand
    {
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;
        private IRepository<ExitAlias> aliases;
        public void CreateExitCommand(IConsoleFacade console, 
            IRepository<GameObject> repository, IPlayer player, IRepository<ExitAlias> aliases)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
            this.aliases = aliases;

        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "createexit");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            string ExitName = output.TrimStart().Split('=')[0];
            string Destination = output.TrimStart().Split('=')[1];
        }
    }
}
