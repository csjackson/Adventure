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
    public class DescribeCommandTest
    {
        private IConsoleFacade console;
        private DescribeCommand cmd;
        private IRepository<GameObject> repository;
        private ICommandController control; 

        [TestInitialize]
        public void Before_Each_Test()
        {
            console = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            cmd = new DescribeCommand(console, repository);
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
            var result = cmd.IsValid("describe Ball= A shiny red rubber ball.");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Notify_if_Name_Not_Recognized()
        {
            // Arrange
            var ball = new GameObject() { Name = "Ball" };
            var ring = new GameObject() { Name = "Ring" };
            var list = new List<GameObject>() { ball, ring };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());

            // Act
            cmd.Execute("describe stupid=foo");

            // Assert
            console.AssertWasCalled(m => m.WriteLine("What is '{0}'?", "stupid"));

        }

        [TestMethod]
        public void Execute_Should_Set_the_Description_of_Something()
        {
            // Arrange
            var ball = new GameObject() { Name = "Ball" };
            var ring = new GameObject() { Name = "Ring" };
            var list = new List<GameObject>() { ball, ring };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());
            // Act
            cmd.Execute("describe Ball=A red rubber ball.");

            // Assert
            Assert.AreEqual("A red rubber ball.", ball.Description);
            //repository.AssertWasCalled(m => m.Dispose());
        }
        [TestMethod]
        public void Execute_Should_Call_LookCommand()
        {
            // Arrange
            // handled in initialize

            // Act
            cmd.Execute("describe Ball= A shiny red rubber ball.");

            // Assert
            //console.AssertWasCalled(m => m.WriteLine("> LOOK {0}", "BALL"));
            control.AssertWasCalled(m => m.Parse("Look Ball"));
        }
    }
}
