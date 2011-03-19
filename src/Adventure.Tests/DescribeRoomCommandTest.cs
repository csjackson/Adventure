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
    public class DescribeRoomCommandTest
    {
        private IConsoleFacade mock;
        private DescribeRoomCommand cmd;
        private IRepository<Room> repository;

        [TestInitialize]
        public void Before_Each_Test()
        {
            mock = MockRepository.GenerateMock<IConsoleFacade>();
            repository = MockRepository.GenerateMock<IRepository<Room>>();
            cmd = new DescribeRoomCommand(mock, repository);
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
            var result = cmd.IsValid("place Hallway.foo = It's a hallway.");

            // Assert
            Assert.IsFalse(result);

        }
        [TestMethod]
        public void isValid_Should_Return_True_for_Valid_String()
        {
            // Arrange

            // Act
            var result = cmd.IsValid("place Hallway.desc = It's a hallway.");

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Execute_Should_Set_the_Description_of_Hallway_to_Its_a_Hallway_Period()
        {
            // Arrange
            var hallway = new Room() { RoomName = "Hallway" };
            var bar = new Room() { RoomName = "Bar" };
            var list = new List<Room>() { hallway, bar };
            repository.Stub(qq => qq.AsQueryable()).Return(list.AsQueryable());

            // Act
            cmd.Execute("place Hallway.desc = It's a hallway.");

            // Assert
            Assert.AreEqual("It's a hallway.", hallway.Description);
            // It's not enough to assert that a DB call was made.
                // How can I assert that a specific field was chosen?
            repository.AssertWasCalled(m => m.Dispose());
        }
    }
}
