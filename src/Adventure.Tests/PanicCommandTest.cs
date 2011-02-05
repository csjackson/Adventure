using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Adventure.Tests
{
    [TestClass]
    public class PanicCommandTest
    {
        private IConsoleFacade mock;
        private PanicCommand cmd;
   
        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            cmd = new PanicCommand(mock);
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
            var result = cmd.IsValid("panic bob");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Write_To_Console_Panic_Plus_All_But_First_Word()
        {
            // Arrange

            // Act
            cmd.Execute("panic the evening news");

            // Assert
            mock.AssertWasCalled(m => m.WriteLine("One look at {0} sends you into a panic.", "the evening news"));
        }
    }
}
