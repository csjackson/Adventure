using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{
    public class EchoCommand : BaseCommand, ICommand
    {
        private IConsoleFacade console;

        public EchoCommand(IConsoleFacade console)
        {
            this.console = console;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "echo");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            console.WriteLine(output);
        }
                             }
}

