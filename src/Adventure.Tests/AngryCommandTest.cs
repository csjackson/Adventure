using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Adventure.Tests
{
    [TestClass]
    public class AngryCommandTest
    {
        private IConsoleFacade mock;
        private AngryCommand cmd;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            cmd = new AngryCommand(mock);
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
            var result = cmd.IsValid("angry bob");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Write_To_Console_Angry_Plus_All_But_First_Word()
        {
            // Arrange

            // Act
            cmd.Execute("angry blah");

            // Assert
            mock.AssertWasCalled(m => m.WriteLine("You shake your fist at blah in anger."));
        }
    }
}
