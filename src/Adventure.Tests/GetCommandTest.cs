using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Adventure.Data;
using Adventure.Commands;

namespace Adventure.Tests
{
    [TestClass]
    public class GetCommandTest
    {
        private GameObject dbHallway;
        private List<GameObject> dbList;
        private GameObject dbRing;
        private GameObject dbBall;
        private GameObject dbPlayer;
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;
        private GetCommand cmd;

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


            cmd = new GetCommand(console, repository, player);
        }
        [TestMethod]
        public void IsValid_Should_Return_False_for_Invalid_String()
        {
            // Arrange
            // Already created via TestInitialize

            // Act
            var result = cmd.IsValid("Stupid stuff");
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void isValid_Should_Return_True_for_Valid_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("get ball");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Inform_When_Item_Already_Held()
        {
            // Arrange
            
            // Act
            cmd.Execute("get ball");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("You already have that."));
        }
        [TestMethod]
        public void Execute_Should_Inform_When_Item_Not_In_Room_with_Player()
        {
            // Arrange
          

            // Act
            cmd.Execute("get foo");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("I do not see that, here."));
        }

        [TestMethod]
        public void Execute_Should_Add_Item_To_Inventory()
        {
            // Arrange
           

            // Act
            cmd.Execute("get ring");
            // Assert
            Assert.AreEqual(dbRing.Location, dbPlayer);
            console.AssertWasCalled(qq => qq.WriteLine("You pick up {0}", dbRing.Name));
        }
    }
}
