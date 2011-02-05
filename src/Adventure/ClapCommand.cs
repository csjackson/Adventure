using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure 
{
    public class ClapCommand : BaseCommand, ICommand
    {
        private IConsoleFacade storage;

        public ClapCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
           public bool IsValid(string input)
        {
            return IsFirstWord(input, "clap");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            storage.WriteLine(String.Format("You applaud at {0}. Bravo!", output));
         }
}
}
