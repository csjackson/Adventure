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
        protected IConsoleFacade console;
        protected IRepository<GameObject> repository;
        protected IPlayer player;

        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            dbPlayer = new GameObject() { GameObjectId = 3, Location = dbHallway, Location_Id = 8 };
            player.Stub(qq => qq.Id).Return(3);
            dbHallway = new GameObject() { Name = "Hallway", Description = " It's a hallway", GameObjectId = 8 };
            dbBall = new GameObject() { Name = "Ball", Description = "A shiny rubber ball", Location = dbPlayer, Location_Id = 3 };
            dbRing = new GameObject() { Name = "Ring", Description = "A simple gold ring", Location = dbHallway, Location_Id = 8 };
            dbPlayer.Inventory.Add(dbBall);
            dbHallway.Inventory.Add(dbPlayer);
            dbHallway.Inventory.Add(dbRing);
            dbList = new List<GameObject>() { dbPlayer, dbBall, dbRing };
            repository.Stub(qq => qq.AsQueryable()).Return(dbList.AsQueryable());


        }
    }

}
