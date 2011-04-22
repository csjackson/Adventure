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
        public void Execute_Should_Add_Aliases_For_An_Exit()
           {
               // Arrange

                // Act
                cmd.Execute("aliasexit exit=north, n");

                // Assert
                console.AssertWasCalled(m => m.WriteLine("Aliases for exit 'exit' now include: 'north' 'n'"));
              
           }
        [TestMethod]
        public void Execute_Should_Test_If_Exit_In_Room_W_Player()
        {
        	// arrange

            //act
            cmd.Execute("alaisexit foo=north");

            // Assert
            console.AssertWasCalled(m => m.WriteLine("Exit '{0}' not visible.", "foo"));
        }
    }
}
