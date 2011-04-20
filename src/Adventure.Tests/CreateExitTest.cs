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
    public class CreateExitTest : BaseTest
    {
        private CreateExitCommand cmd;
        [TestInitialize]
        public void Before_Every_Test()
        {
            cmd = new CreateExitCommand();
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
            var result = cmd.IsValid("CreateExit Blah=Hallway");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CreateExit_Should_Create_An_Exit()
        {
        	// Arrange
            // Act
            cmd.Execute("Createexit blah=Hallway");
            // Assert
            Assert.Fail();
        }
        [TestMethod]
        public void CreateExit_Should_Check_For_Valid_Destination()
        {
            // Arrange

            // Act 
            cmd.Execute("Createexit blah=Foo");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("Destination '{0}' was not found", "Foo"));
        }
    }
}
