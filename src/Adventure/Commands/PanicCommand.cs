using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class PanicCommand : BaseCommand, ICommand
    {
          private IConsoleFacade storage;

        public PanicCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "panic");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            storage.WriteLine("One look at {0} sends you into a panic.", output);

        }
    }
}
