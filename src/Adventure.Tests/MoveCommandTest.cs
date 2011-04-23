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
    public class MoveCommandTest : BaseTest
    {
        private IFormatter format;
        private MoveCommand cmd;
        [TestInitialize]
        public void Before_Every_Test()
        {
            format = new Formatter(console, repository);
            cmd = new MoveCommand(console, () => repository, player, format);
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
        public void isValid_Should_Return_True_for_Move_exit()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("move exit");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void isValid_Should_Return_True_for_Go_Exit()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("go exit");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void isValid_Should_Return_True_for_North()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("north");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Update_Location_of_Player()
        {
            // Arrange

            // Act
            cmd.Execute("go north");

            // Assert
            Assert.AreEqual(dbPlayer.Location_Id, dbExit.Destination);
        }
        [TestMethod]
      public void Execute_Should_display_Desc_of_New_Location_of_Player()
      {
            // Arrange

            // Act
          cmd.Execute("go north");

           // Assert
          console.AssertWasCalled(qq => qq.WriteLine(dbHallway.Description));
      }
    }
}
