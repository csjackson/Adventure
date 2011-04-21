using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Adventure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adventure.Tests
{
    [TestClass]
    public abstract class BaseTest
    {
        protected GameObject dbHallway;
        protected List<GameObject> dbList;
        protected GameObject dbRing;
        protected GameObject dbBall;
        protected GameObject dbPlayer;
        protected GameObject dbExit;
        protected IConsoleFacade console;
        protected IRepository<GameObject> repository;
        protected IPlayer player;
        protected IRepository<ExitAlias> aliasRepo;
        protected List<ExitAlias> aliasList;
        protected ExitAlias exit;
        protected ExitAlias exit2;

        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            dbPlayer = new GameObject() { GameObjectId = 3, Location = dbHallway, Location_Id = 8, Description = "Just some dude." };
            player.Stub(qq => qq.Id).Return(3);
            dbHallway = new GameObject() { Name = "Hallway", Description = " It's a hallway", GameObjectId = 8 };
            dbBall = new GameObject() { Name = "Ball", Description = "A shiny rubber ball", Location = dbPlayer, Location_Id = 3 };
            dbRing = new GameObject() { Name = "Ring", Description = "A simple gold ring", Location = dbHallway, Location_Id = 8 };
            dbExit = new GameObject() {Name = "Exit", Description ="", Location= dbHallway, Location_Id = 8, GameObjectId = 16, Type = "Exit", Destination = 8 };
            dbPlayer.Inventory.Add(dbBall);
            dbHallway.Inventory.Add(dbPlayer);
            dbHallway.Inventory.Add(dbRing);
            dbHallway.Inventory.Add(dbExit);
            dbList = new List<GameObject>() { dbPlayer, dbBall, dbRing, dbExit };
            repository.Stub(qq => qq.AsQueryable()).Return(dbList.AsQueryable());
            aliasRepo = MockRepository.GenerateMock<IRepository<ExitAlias>>();
            exit = new ExitAlias() { AliasId = 2, ExitId = 16, Alais = "Hallway" };
            exit2 = new ExitAlias() { AliasId = 2, ExitId = 16, Alais = "Hall" };
            aliasList = new List<ExitAlias> { exit, exit2 };
            aliasRepo.Stub(qq => qq.AsQueryable()).Return(aliasList.AsQueryable());

        }
    }

}
