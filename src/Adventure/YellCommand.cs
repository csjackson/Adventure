using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class YellCommand : BaseCommand, ICommand
    {
          private IConsoleFacade storage;

        public YellCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "yell");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input).ToUpper();
            storage.ForegroundColor = ConsoleColor.Red;
            storage.WriteLine(output);
            storage.ResetColor();
        }

    }
}
 
