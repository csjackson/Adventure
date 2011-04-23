using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class MoveCommand : BaseCommand, ICommand
    {
             private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;
        private IRepository<ExitAlias> aliases;
        private IFormatter format;

        public MoveCommand(IConsoleFacade console,
            IRepository<GameObject> repository, IPlayer player, IRepository<ExitAlias> aliases, IFormatter format)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
            this.aliases = aliases;
            this.format = format;

        }
        public bool IsValid(string input)
        {
            bool placator = ((IsFirstWord(input, "move")) || (IsFirstWord(input, "go")));
            return placator;
        }
          public void Execute(string input)
        {
            string Output = GetAllButFirstWord(input);
              ExitAlias ChosenExit = null;
            using (aliases)
            {
                 ChosenExit = aliases.AsQueryable().First(qq => 
                     qq.Alais.Equals(Output, StringComparison.CurrentCultureIgnoreCase) );
            }
              using (repository)
              {
                  var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                  pObj.Location_Id = ChosenExit.ExitId;
                  var LocNow = repository.AsQueryable().FirstOrDefault(qq => qq.GameObjectId == ChosenExit.ExitId);
                  format.Output(LocNow);
              }
            
        }
    }
}
