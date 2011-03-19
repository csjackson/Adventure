using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;
using Adventure.Commands;

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
            commands.Add(new CreateRoomCommand(new ConsoleFacade(), new Repository<Room>(new UnitOfWork())));
            commands.Add(new CreateItemCommand(new ConsoleFacade(), new Repository<Item>(new UnitOfWork())));
            commands.Add(new DescribeRoomCommand(new ConsoleFacade(), new Repository<Room>(new UnitOfWork())));
            commands.Add(new DescribeItemCommand(new ConsoleFacade(), new Repository<Item>(new UnitOfWork())));
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
