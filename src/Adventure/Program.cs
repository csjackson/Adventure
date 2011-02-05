using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new EchoCommand(new ConsoleFacade()));
            commands.Add(new YellCommand(new ConsoleFacade()));
            commands.Add(new DanceCommand(new ConsoleFacade()));
            commands.Add(new WaveCommand(new ConsoleFacade()));
            commands.Add(new AngryCommand(new ConsoleFacade()));
            commands.Add(new ClapCommand(new ConsoleFacade()));
            commands.Add(new BonkCommand(new ConsoleFacade()));
            commands.Add(new GlareCommand(new ConsoleFacade()));
            commands.Add(new PanicCommand(new ConsoleFacade()));
            ICommand defaultCommand = new UnknownCommand(new ConsoleFacade());
            
            do 
            {
                var input = Console.ReadLine();
                if (input.Trim() == "exit") break;

                var cmd = commands.FirstOrDefault(c => c.IsValid(input));
                if (cmd == null) cmd = defaultCommand;
                cmd.Execute(input);
            }
            while (true); ;
        }
    }

}
