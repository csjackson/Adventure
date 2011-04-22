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

        public CreateExitCommand(IConsoleFacade console, 
            IRepository<GameObject> repository, IPlayer player) 
        {
            this.console = console;
            this.repository = repository;
            this.player = player;

        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "createexit");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            string ExitName = output.TrimStart().Split('=')[0];
            string DestinationName = output.TrimStart().Split('=')[1];

            using (repository)
            {
              var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
              var destination = repository.AsQueryable().FirstOrDefault(qq => 
                  qq.Name.Equals(DestinationName, StringComparison.CurrentCultureIgnoreCase)) ;
                if (destination != null)
                { repository.Add(new GameObject() 
                    { Name = ExitName, Description = "",
                        Type = "Exit", Location = pObj.Location, Destination = destination.GameObjectId });

                console.WriteLine("A new exit named '{0}' was created, leading to '{1}'", ExitName, destination.Name);
                }
                else
                {
                    console.WriteLine("Destination '{0}' was not found", DestinationName);
                }
            }
        }
    }
}
