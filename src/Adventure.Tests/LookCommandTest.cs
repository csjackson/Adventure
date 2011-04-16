using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Adventure.Commands;
using Adventure.Data;
namespace Adventure.Tests
{
    [TestClass]
    public class LookCommandTest
    {
        private LookCommand cmd;
        private IFormatter format;
        private GameObject dbHallway;
        private List<GameObject> dbList;
        private GameObject dbRing;
        private GameObject dbBall;
        private GameObject dbPlayer;
        private IConsoleFacade console;
        private IRepository<GameObject> repository;
        private IPlayer player;

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


            format = new Formatter(console, repository);
            cmd = new LookCommand(console, repository, format, player);
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
        public void Look_Plus_Garbage_Should_Return_Quote_I_Dont_See_That_Here()
        {
            // Arrange
            // Already created via TestInitialize

            // Act
            cmd.Execute("look stupid");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("I don't see that here."));
        }

        [TestMethod]
        public void Look_and_Name_Should_Return_Desc_of_Name()
        {
            // Arrange
           
            // Act
            cmd.Execute("look ball");

            // Assert
            console.AssertWasCalled(qq => qq.WriteLine(dbBall.Description));
            repository.AssertWasCalled(m => m.Dispose());
        }
        [TestMethod]
        public void Look_Alone_Should_Look_Here()
        {
            // Arrange
           
            // Act
            cmd.Execute("look");

            // Assert
            console.AssertWasCalled(qq => qq.WriteLine(dbHallway.Description));
            repository.AssertWasCalled(m => m.Dispose());
        }

    }
}
