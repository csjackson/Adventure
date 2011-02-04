﻿using System;
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
            commands.Add(new EchoCommand());
            commands.Add(new YellCommand());
            commands.Add(new DanceCommand());
            commands.Add(new WaveCommand(new ConsoleFacade()));
            commands.Add(new AngryCommand());
            commands.Add(new ClapCommand());
            commands.Add(new BonkCommand());
            commands.Add(new GlareCommand());
            commands.Add(new PanicCommand());
            ICommand defaultCommand = new UnknownCommand();
            
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