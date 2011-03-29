using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Adventure.Data;

namespace Adventure.Tests
{
    [TestClass]
    public class InventoryCommandTest
    {
        private IConsoleFacade mock;
        private InventoryCommand cmd;
        private IRepository<Item> repository;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<Item>>();
            cmd = new InventoryCommand(mock, repository);
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
            var result = cmd.IsValid("inventory");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_List_Contents_Of_Inventory()
        {
            // Arrange

            // Act
            cmd.Execute("inventory");

            // Assert
            Assert.Fail();
            repository.AssertWasCalled(m => m.Dispose());
        }

    }
}
