using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class AngryCommand : BaseCommand, ICommand
    {
        private IConsoleFacade storage;

        public AngryCommand(IConsoleFacade console)
        {
            this.storage = console;
        }
        public bool IsValid(string input)
        {
            return IsFirstWord(input, "angry");
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            storage.WriteLine(String.Format("You shake your fist at {0} in anger.", output));
        }
    }
}
