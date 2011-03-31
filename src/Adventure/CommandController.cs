using System;
using System.Linq;

namespace Adventure
{
    public class CommandController
    {
        private ICommand[] commands;
        private IUnknownInputHandler unknown;

        public CommandController(ICommand[] commands, IUnknownInputHandler unknown)
        {
            this.commands = commands;
            this.unknown = unknown;

        }
        public bool Parse(string input)
        {
            if (input.Trim() == "exit") return false;

            var cmd = commands.FirstOrDefault(c => c.IsValid(input));
            if (cmd == null)
            {
                unknown.HandleUnknownInput(input);
                return true;
            }
            cmd.Execute(input);
            return true;
        }
    }
}
