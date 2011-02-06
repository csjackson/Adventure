using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Adventure.Tests
{
    [TestClass]
    public class LolCommandTest
    {
        private IConsoleFacade mock;
        private LolCommand cmd;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            cmd = new LolCommand(mock);
        }

        [TestMethod]
        public void IsValid_Should_Return_False_for_Invalid_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("Stupid stuff");
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void isValid_Should_Return_True_for_LOL_Followed_By_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("lol blah");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void isValid_Should_Return_True_for_LOL_Sans_Args()
        {
            // Arrange


            // Act
            var result = cmd.isValid("lol");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Write_to_Console_LOL_Plus_All_But_First_Word()
        {
            // Arrange
            

            // Act
            cmd.Execute("lol blah");

            // Assert
            mock.AssertWasCalled(m => m.WriteLine("You laugh at {0}. Tee Hee!", "blah"));
        }
        [TestMethod]
        public void Execute_Should_Write_LOL_to_Console()
        {
            // Arrange
            

            // Act
            cmd.Execute("lol");

            // Assert
            mock.AssertWasCalled(m => m.WriteLine("You laugh. Tee Hee!"));
        }
    
    }
}
