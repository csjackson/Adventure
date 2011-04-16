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
    public class DropCommandTest
    {
        private GameObject dbHallway;
        private List<GameObject> dbList;
        private GameObject dbRing;
        private GameObject dbBall;
        private GameObject dbPlayer;
           private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;

        private DropCommand cmd;


        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            dbPlayer = new GameObject() { GameObjectId = 3, Location = dbHallway, Location_Id = 8 };
            player.Stub(qq => qq.Id).Return(3);
            dbHallway = new GameObject() {  Name = "Hallway", Description =" It's a hallway", GameObjectId = 8 };
            dbBall = new GameObject() { Name = "Ball", Description= "A shiny rubber ball", Location = dbPlayer, Location_Id = 3 };
            dbRing = new GameObject() { Name = "Ring", Description= "A simple gold ring", Location = dbHallway, Location_Id = 8 };
            dbPlayer.Inventory.Add(dbBall);
            dbHallway.Inventory.Add(dbPlayer);
            dbHallway.Inventory.Add(dbRing);
            dbList = new List<GameObject>() { dbPlayer, dbBall, dbRing };
            repository.Stub(qq => qq.AsQueryable()).Return(dbList.AsQueryable());

            cmd = new DropCommand(console, repository, player);

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
            var result = cmd.IsValid("drop ball");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Inform_If_Item_Not_in_Inventory()
        {
            // arrange
            


            // Act
            cmd.Execute("drop ring");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("'{0}' not in inventory.", "ring"));
        }
        [TestMethod]
        public void Execute_Should_Change_Location_Of_Item_From_Inventory_to_Location_of_Player()
        {
        	 // arrange
           
            // Act
            cmd.Execute("drop ball");
            // Assert
            Assert.AreEqual(dbBall.Location_Id, dbHallway.GameObjectId);
            console.AssertWasCalled(qq => qq.WriteLine("You put down your {0}", dbBall.Name));
        }
    }
}
