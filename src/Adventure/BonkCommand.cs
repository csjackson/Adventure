using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class BonkCommand : BaseCommand, ICommand
    {
        private IConsoleFacade storage;

        public BonkCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "bonk");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            storage.WriteLine("You bonk {0} on the head. Ouch!", output);
        }
    }
}
