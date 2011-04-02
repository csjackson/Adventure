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
    public class RenameCommandTest
    {
        private IConsoleFacade mock;
        private RenameCommand cmd;
        private IRepository<GameObject> repository;
        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            cmd = new RenameCommand(mock, repository);
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
            var result = cmd.IsValid("rename Ball=Sphere");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Set_the_Name_of_Ball_to_Orb()
        {
            // Arrange
            var ball = new GameObject() { Name = "Ball" };
            var ring = new GameObject() { Name = "Ring" };
            var list = new List<GameObject>() { ball, ring };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            // Act
            cmd.Execute("rename Ball=Orb");

            // Assert
            Assert.AreEqual("Orb", ball.Name);
            
            repository.AssertWasCalled(m => m.Dispose());
        }
    }
}
