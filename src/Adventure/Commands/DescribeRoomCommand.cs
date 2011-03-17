using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class DescribeRoomCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<Room> repository;

        public DescribeRoomCommand(IConsoleFacade console, IRepository<Room> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
           return (IsFirstWord(input, "set") && (input.Contains(".name")));
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            var roomDescription = input.Trim().Split('=').Skip(1).ToString();
          
            var Holder = 
                ;

            using (repository)
            {
                repository.Add(new Room() { RoomName = output });
            }
            console.WriteLine("Room Description Saved.");
        }

    }
}
