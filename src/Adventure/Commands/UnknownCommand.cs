using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    public class UnknownCommand : ICommand
    {
          private IConsoleFacade storage;

        public UnknownCommand(IConsoleFacade console)
        {
            this.storage = console;
        }

        public void Execute(string input)
        {
            storage.WriteLine("I don't understand.");
        }

        public bool IsValid(string input)
        {
            return true;
        }

    }
}
