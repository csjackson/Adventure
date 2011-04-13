using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;


namespace Adventure.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IFormatter format;
        private IPlayer player;

        public LookCommand(IConsoleFacade console,  IRepository<GameObject> repository, IFormatter format, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.format = format;
            this.player = player;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "look"); 
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            if (input == "look")
                output = "here";
            using (repository)
            {
                GameObject LookedAt = null;
              var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
              if (output == "here")
              {
                  LookedAt = repository.AsQueryable().FirstOrDefault(qq => qq == pObj.Location);
              }
              else
              {
                  /* What I Wanted:
                 LookedAt = repository.AsQueryable().FirstOrDefault
                     (qq => (qq.Name == output) && (qq.Location == pObj.Location || qq.Location == pObj));
                 */
                 LookedAt = repository.AsQueryable().FirstOrDefault
                      (qq => qq.Location == pObj.Location && qq.Name == output);
                if (LookedAt == null)
                {
                    LookedAt = repository.AsQueryable().FirstOrDefault
                           (qq => qq.Location_Id == player.Id && qq.Name == output);
                }

                  // The Old Way:
                  //  var LookedAt = repository.AsQueryable()
                  //      .FirstOrDefault(qq => qq.Name == output);
              }
                    if (LookedAt == null)
                    {
                        console.WriteLine("I don't see that here.");
                        return;
                    }
              
                format.Output(LookedAt);
            }
        }
    }
}
