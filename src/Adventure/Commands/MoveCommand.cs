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
        private IFormatter format;
        private Func<IRepository<GameObject>> repoFactory;

        public MoveCommand(IConsoleFacade console,
            Func<IRepository<GameObject>> repoFactory, IPlayer player, IFormatter format)
        {
            this.console = console;
            this.repoFactory = repoFactory;
            this.player = player;
            this.format = format;

        }
        public bool IsValid(string input)
        {
            string InputHolder = input;
            if  (((IsFirstWord(InputHolder, "move")) || (IsFirstWord(InputHolder, "go"))))
            {
                InputHolder = GetAllButFirstWord(input);
            }
            using (repository = repoFactory())
            {
                var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                var exit = pObj.Location.Inventory.Where(qq => qq.Type == "Exit").
                    FirstOrDefault(qq => qq.ExitAliases.Any(ww => ww.Alais.Equals(InputHolder, StringComparison.CurrentCultureIgnoreCase))
                        || qq.Name.Equals(InputHolder, StringComparison.CurrentCultureIgnoreCase));
                return exit != null;
            }
          
        }
          public void Execute(string input)
        {
            string Output = GetAllButFirstWord(input);
            
              using (repository = repoFactory())
              {
                  var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                  var WhichExit = pObj.Location.Inventory.FirstOrDefault(qq => (qq.Name.Equals
                      (Output, StringComparison.CurrentCultureIgnoreCase)) && qq.Type=="Exit"); 
                  if (WhichExit != null)
                  {
                      pObj.Location = WhichExit.DestinationLocation;
                  }
                  format.Output(pObj.Location);
              }
            
        }
    }
}
