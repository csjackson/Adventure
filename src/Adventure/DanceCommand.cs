using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class DanceCommand : BaseCommand, ICommand
    {
          private IConsoleFacade storage;

        public DanceCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
   
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "dance");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            storage.WriteLine("You gracefully dance the {0}.", output);
                
        }
    }
   
}
