using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure
{
    public class CreateRoomCommand : BaseCommand, ICommand
    {
        
        private IConsoleFacade console;
        private IRepository<Room> repository;

        public CreateRoomCommand(IConsoleFacade console, IRepository<Room> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return IsFirstWord(input, "createroom");
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                repository.Add(new Room() { RoomName = output });
            }
            console.WriteLine("Room Created.");
        }
                             
    }
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

