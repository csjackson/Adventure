using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class DescribeCommand : BaseCommand, ICommand 
    {

        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private ICommandController control;

        public DescribeCommand(IConsoleFacade console, IRepository<GameObject> repository )
        {
            this.console = console;
            this.repository = repository;
            
        }

        public bool IsValid(string input)
        {
           return (IsFirstWord(input, "describe"));
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            var thing = output.TrimStart().Split('=')[0];
            var description = string.Join("=", input.Trim().Split('=').Skip(1)).Trim();

            using (repository)
            {
                var item = repository.AsQueryable()
                    .FirstOrDefault(qq => qq.Name == thing);
                if (item == null)
                {
                    console.WriteLine("What is '{0}'?", thing);
                    return;
                }
                item.Description = description;
            }
            console.WriteLine("Description of object '{0}' changed.", thing);
            console.WriteLine("> LOOK {0}", thing.ToUpper());
            control.Parse(String.Join("look ", thing));
            // control.Parse spits exception: Object reference not set to an instance of an object. 
        }

    }
}
