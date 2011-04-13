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
        private IConsoleFacade mock;
        private LookCommand cmd;
        private IRepository<GameObject> repository;
        private IFormatter format;
        private IPlayer player;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            format = new Formatter(mock, repository);
            player = MockRepository.GenerateMock<IPlayer>();
            cmd = new LookCommand(mock, repository, format, player);
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
            repository.Stub(qq => qq.AsQueryable()).Return((new List<GameObject>() { }).AsQueryable());

            // Act
            cmd.Execute("look stupid");
            // Assert
            mock.AssertWasCalled(qq => qq.WriteLine("I don't see that here."));
        }

        [TestMethod]
        public void Look_and_Name_Should_Return_Desc_of_Name()
        {
            // Arrange
            var hallway = new GameObject() { Name = "Hallway", Description= "It's a hallway." };
            var bar = new GameObject() { Name = "Bar", Description = "It's my kinda of place." };
            var list = new List<GameObject>() { hallway, bar };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            // Act
            cmd.Execute("look Hallway");

            // Assert
            mock.AssertWasCalled(qq => qq.WriteLine("It's a hallway."));
            repository.AssertWasCalled(m => m.Dispose());
        }
        [TestMethod]
        public void Look_Alone_Should_Look_Here()
        {
            // Arrange
            var hallway = new GameObject() { Name = "Hallway", Description = "It's a hallway.", GameObjectId = 8 };
            
            var bar = new GameObject() { Name = "Bar", Description = "It's my kinda of place." };
            var list = new List<GameObject>() { hallway, bar };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            // Act
            cmd.Execute("look Hallway");

            // Assert
            mock.AssertWasCalled(qq => qq.WriteLine("It's a hallway."));
            repository.AssertWasCalled(m => m.Dispose());
        }

    }
}
