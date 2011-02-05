using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class GlareCommand : BaseCommand, ICommand
    {
          private IConsoleFacade storage;

        public GlareCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "glare");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            storage.WriteLine(String.Format("You fix {0} with a baleful glare.", output));

        }
    }
}
