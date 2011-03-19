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
    public class DescribeItemCommandTest
    {
        private IConsoleFacade mock;
        private DescribeItemCommand cmd;
        private IRepository<Item> repository;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<Item>>();
            cmd = new DescribeItemCommand(mock, repository);
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
            var result = cmd.IsValid("item Ball.desc= A shiny red rubber ball.");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Set_the_Description_of_Ball_to_A_Red_Rubber_Ball_period()
        {
            // Arrange

            // Act
            cmd.Execute("item Ball.desc = A red rubber ball.");

            // Assert
            repository.AssertWasCalled(m => m.Equals(repository));
            // It's not enough to assert that a DB call was made.
            // How can I assert that a specific field was chosen?
            repository.AssertWasCalled(m => m.Dispose());
        }
    }
}
