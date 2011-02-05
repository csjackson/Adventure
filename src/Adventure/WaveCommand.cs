using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class WaveCommand : BaseCommand, ICommand
    {
        private IConsoleFacade storage;

        public WaveCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "wave");
        }
        public void Execute(string input)
        {
            storage.WriteLine("You wave at {0}.", GetAllButFirstWord(input));
        }
    }
}
