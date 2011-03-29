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
    public class RenameItemCommandTest
    {
        private IConsoleFacade mock;
        private RenameItemCommand cmd;
        private IRepository<Item> repository;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<Item>>();
            cmd = new RenameItemCommand(mock, repository);
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
        public void isValid_Should_Return_False_Without_dotName_in_String()
        {
            // Arrange
            // created vua TestInitialize

            // Act
            var result = cmd.IsValid("item Ball.foo = It's a hallway.");

            // Assert
            Assert.IsFalse(result);

        }
        [TestMethod]
        public void isValid_Should_Return_True_for_Valid_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("item Ball.name= Sphere");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Set_the_Name_of_Ball_to_Orb()
        {
            // Arrange
            var ball = new Item() { ItemName = "Ball" };
            var ring = new Item() { ItemName = "Ring" };
            var list = new List<Item>() { ball, ring };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            // Act
            cmd.Execute("item Ball.name = Orb");

            // Assert
            Assert.AreEqual("Orb", ball.ItemName);
            
            repository.AssertWasCalled(m => m.Dispose());
        }
    }
}
