﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;

namespace Adventure.Commands
{
    public class InventoryCommand : BaseCommand, ICommand
    {

        private IConsoleFacade console;
        private IRepository<GameObject> repository;

        public InventoryCommand(IConsoleFacade console, IRepository<GameObject> repository)
        {
            this.console = console;
            this.repository = repository;
        }

        public bool IsValid(string input)
        {
            return (IsFirstWord(input, "inventory"));
        }

        public void Execute(string input)
        {
            console.WriteLine("You are carrying:");
            using (repository)
            {
                var sack = repository.AsQueryable().Where(qq => qq.GameObjectId == 2);
                foreach (var item in sack)
                {
                    console.Write("{0}  ", item.Name);
                }
                /* Reminder: RoomId 2 is the inventory "room".
                */
            }

        }
    }
}