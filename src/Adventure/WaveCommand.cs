using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class WaveCommand : BaseCommand, ICommand
    {
        private IConsoleFacade console;

        public WaveCommand(IConsoleFacade console)
        {
            this.console = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "wave");
        }
        public void Execute(string input)
        {
            console.WriteLine("You wave at {0}.", GetAllButFirstWord(input));
        }
    }
}
