﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adventure.Data;


namespace Adventure.Commands
{
    public class LookCommand : BaseCommand, ICommand
    {
        
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IFormatter format;
        private IPlayer player;

        public LookCommand(IConsoleFacade console,  IRepository<GameObject> repository, IFormatter format, IPlayer player)
        {
            this.console = console;
            this.repository = repository;
            this.format = format;
            this.player = player;
        }

        public bool IsValid(string input)
        {
            var determination = ( IsFirstWord(input, "look") || IsFirstWord(input, "l"));
            return determination;
        }

        public void Execute(string input)
        {
            var output = GetAllButFirstWord(input);
            bool Placator = (input.Equals("l", StringComparison.CurrentCultureIgnoreCase)
                || input.Equals("look", StringComparison.CurrentCultureIgnoreCase)); 
            if (Placator)
                output = "here";
            using (repository)
            {
                GameObject LookedAt = null;
                var pObj = repository.AsQueryable().First(qq => qq.GameObjectId == player.Id);

                if (output == "here")
              {
                  LookedAt = pObj.Location;
              }
              else if (output== "me")
              {
                  LookedAt = pObj;
              }
              else
              {
                  /* What I Wanted:
                 LookedAt = repository.AsQueryable().FirstOrDefault
                     (qq => (qq.Name == output) && (qq.Location == pObj.Location || qq.Location == pObj));
                 */
                 LookedAt = repository.AsQueryable().FirstOrDefault
                      (qq => qq.Location == pObj.Location && qq.Name.Equals(output, StringComparison.CurrentCultureIgnoreCase));
                if (LookedAt == null)
                {
                    LookedAt = repository.AsQueryable().FirstOrDefault
                           (qq => qq.Location_Id == player.Id && qq.Name.Equals(output, StringComparison.CurrentCultureIgnoreCase));
                }

                  // The Old Way:
                  //  var LookedAt = repository.AsQueryable()
                  //      .FirstOrDefault(qq => qq.Name == output);
              }
                    if (LookedAt == null)
                    {
                        console.WriteLine("I don't see that here.");
                        return;
                    }
              
                format.Output(LookedAt);
                console.WriteLine("{0} holds:", LookedAt.Name);
                foreach (GameObject i in LookedAt.Inventory)
                {
                    console.Write("{0}  ", i.Name);
                }
            }
        }
    }
}
