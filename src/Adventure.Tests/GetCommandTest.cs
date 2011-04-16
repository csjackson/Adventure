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
    public class GetCommandTest : BaseTest
    {
     
        private GetCommand cmd;

     [TestInitialize]
        public void Before_Every_Test()
        {
            cmd = new GetCommand(console, repository, player);
            
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
            var result = cmd.IsValid("get ball");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Inform_When_Item_Already_Held()
        {
            // Arrange
            
            // Act
            cmd.Execute("get ball");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("You already have that."));
        }
        [TestMethod]
        public void Execute_Should_Inform_When_Item_Not_In_Room_with_Player()
        {
            // Arrange
          

            // Act
            cmd.Execute("get foo");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("I do not see that, here."));
        }

        [TestMethod]
        public void Execute_Should_Add_Item_To_Inventory()
        {
            // Arrange
           

            // Act
            cmd.Execute("get ring");
            // Assert
            Assert.AreEqual(dbRing.Location, dbPlayer);
            console.AssertWasCalled(qq => qq.WriteLine("You pick up {0}", dbRing.Name));
        }
    }
}
