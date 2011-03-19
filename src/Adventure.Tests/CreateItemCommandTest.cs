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
    public class CreateItemCommandTest
    {
        private IConsoleFacade mock;
        private CreateItemCommand cmd;
        private IRepository<Item> repository;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<Item>>();
            cmd = new CreateItemCommand(mock, repository);
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
            var result = cmd.IsValid("createitem ball");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Create_a_New_DB_Item_Named_Hallway()
        {
            // Arrange

            // Act
            cmd.Execute("createroom Hallway");

            // Assert
            repository.AssertWasCalled(m => m.Add(Arg<Item>.Is.Anything));
            repository.AssertWasCalled(m => m.Dispose());
        }

    }
}
