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
    public class LookCommandTest : BaseTest
    {
        private LookCommand cmd;
        private IFormatter format;
     
        [TestInitialize]
        public void Before_Every_Test()
        {
            format = new Formatter(console, repository);
            cmd = new LookCommand(console, repository, format, player);
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
        public void Look_Plus_Garbage_Should_Return_Quote_I_Dont_See_That_Here()
        {
            // Arrange
            // Already created via TestInitialize

            // Act
            cmd.Execute("look stupid");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("I don't see that here."));
        }

        [TestMethod]
        public void Look_and_Name_Should_Return_Desc_of_Name()
        {
            // Arrange
           
            // Act
            cmd.Execute("l ball");

            // Assert
            console.AssertWasCalled(qq => qq.WriteLine(dbBall.Description));
            repository.AssertWasCalled(m => m.Dispose());
        }
        [TestMethod]
        public void Look_Alone_Should_Look_Here()
        {
            // Arrange
           
            // Act
            cmd.Execute("look");

            // Assert
            console.AssertWasCalled(qq => qq.WriteLine(dbHallway.Description));
            repository.AssertWasCalled(m => m.Dispose());
        }

        [TestMethod]
        public void Look_Command_Should_Display_Inv_Of_Viewed()
        {
        	// Arrange

            // Act 
            cmd.Execute("l");

            // Asserrt
            console.AssertWasCalled(qq => qq.Write("{0}  ", dbBall.Name));
            console.AssertWasCalled(qq => qq.Write("{0}  ", dbPlayer.Name));
        }
        [TestMethod]
        public void LOOK_ME_Should_Return_Player_Desc()
        {
            // Arrange

            // Act
            cmd.Execute("look me");

            // Assert
            console.AssertWasCalled(qq => qq.WriteLine(dbPlayer.Description));
        }

    }
}
