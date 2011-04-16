﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class GetCommand : BaseCommand, ICommand
    {
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;

        public GetCommand(IConsoleFacade console, IRepository<GameObject> repository, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.player = player;
        }
        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "get"));
        }
        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            using (repository)
            {
                var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);
                var item = repository.AsQueryable().FirstOrDefault(qq => qq.Name == output && 
                        (qq.Location_Id == pObj.Location_Id || qq.Location_Id == player.Id));
                if (item == null)
                {
                    console.WriteLine("I do not see that, here.");
                    return;
                }
                else if (item.Location_Id == player.Id)
                {
                    console.WriteLine(("You already have that."));
                    return;
                }
                item.Location_Id = player.Id;
                console.WriteLine("You pick up {0}", item.Name);
            }
        }
    }
}