using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Adventure.Tests
{
    [TestClass]
    public class DescribeRoomCommandTest
    {   private IConsoleFacade mock;
        private DescribeRoomCommand cmd;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            cmd = new DescribeRoomCommand(mock);
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
        public void isValid_Should_Return_False_Without_dotName_in_String()
        {
            // Arrange
            // created vua TestInitialize
        
            // Act
           var result = cmd.IsValid("set Hallway.foo = It's a hallway.");
        
            // Assert
            Assert.IsFalse(result);
            
         }
        [TestMethod]
        public void isValid_Should_Return_True_for_Valid_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("set Hallway.name = It's a hallway.");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Set_the_Description_of_Hallway_to_Its_a_Hallway_Period()
        {
            // Arrange

            // Act
            cmd.Execute("set Hallway.name = It's a hallway.");

            // Assert
            Assert.Fail();
          }
}
