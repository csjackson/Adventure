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
    public class DropCommandTest : BaseTest
    {
    

        private DropCommand cmd;


        [TestInitialize]
        public void Before_Every_Test()
        {
            
            cmd = new DropCommand(console, repository, player);

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
            var result = cmd.IsValid("drop ball");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Execute_Should_Inform_If_Item_Not_in_Inventory()
        {
            // arrange
            


            // Act
            cmd.Execute("drop ring");
            // Assert
            console.AssertWasCalled(qq => qq.WriteLine("'{0}' not in inventory.", "ring"));
        }
        [TestMethod]
        public void Execute_Should_Change_Location_Of_Item_From_Inventory_to_Location_of_Player()
        {
        	 // arrange
           
            // Act
            cmd.Execute("drop ball");
            // Assert
            Assert.AreEqual(dbBall.Location_Id, dbHallway.GameObjectId);
            console.AssertWasCalled(qq => qq.WriteLine("You put down your {0}", dbBall.Name));
        }
    }
}
