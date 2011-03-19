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
           return (IsFirstWord(input, "place") && (input.Contains(".desc")));
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            var place = output.Split('.')[0];
            var roomDescription = string.Join("=", input.Trim().Split('=').Skip(1)).Trim();

            using (repository)
            {
                var room = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.RoomName == place);
                if (room == null)
                {
                    console.WriteLine("I do not recognize \"{0}\".", place);
                    return;
                }
                room.Description = roomDescription;
            }
          
        }

    }
}
