using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adventure.Commands;

namespace Adventure.Tests
{
    [TestClass]
    public class AliasExitTest :BaseTest
    {
        private AliasExitCommand cmd;
        [TestInitialize]
        public void Before_Every_Test()
        {
            cmd = new AliasExitCommand(console, repository, player, aliasRepo);
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
               var result = cmd.IsValid("AliasExit Blah=north, n, Hallway, hall");

               // Assert
               Assert.IsTrue(result);
           }
        [TestMethod]
        public void Execute_Should_Test_If_Exit_In_Room_W_Player()
           {
               // Arrange

                // Act
                var result = cmd.Execute("aleasexit )
           }
    }
}
