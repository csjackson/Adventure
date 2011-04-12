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
    public class CreateCommandTest
    {
           private IConsoleFacade mock;
        private CreateCommand cmd;
        private IRepository<GameObject> repository;
        private IPlayer player;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            player = MockRepository.GenerateMock<IPlayer>();
            cmd = new CreateCommand(mock, repository, player);
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
            var result = cmd.IsValid("create Hallway");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Create_a_New_DB_Item_Named_Hallway_And_Advise_Same()
        {
            // Arrange

            // Act
            cmd.Execute("create Hallway");

            // Assert
            repository.AssertWasCalled(m => m.Add(Arg<GameObject>.Is.Anything));
            repository.AssertWasCalled(m => m.Dispose());
            mock.AssertWasCalled(m => m.WriteLine("Object '{0}' created.", "Hallway"));

        }
    
        
    }
}
