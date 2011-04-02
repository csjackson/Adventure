//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Rhino.Mocks;
//using Adventure.Commands;
//using Adventure.Data;


//namespace Adventure.Tests
//{
//    [TestClass]
//    public class TakeCommandTest
//    {
//        private IConsoleFacade mock;
//        private TakeCommand cmd;
//        private IRepository<Item> repository;
//        
//        [TestInitialize]
//        public void Before_Each_Test()
//        {
//            mock = MockRepository.GenerateMock<IConsoleFacade>();
//            repository = MockRepository.GenerateMock<IRepository<Item>>();
//            cmd = new TakeCommand(mock, repository);

//        }

//        [TestMethod]
//        public void IsValid_Should_Return_False_for_Invalid_String()
//        {
//            // Arrange
//            // Already created via TestInitialize

//            // Act
//            var result = cmd.IsValid("Stupid stuff");
//            // Assert
//            Assert.IsFalse(result);
//        }

//        [TestMethod]
//        public void isValid_Should_Return_True_for_Valid_String()
//        {
//            // Arrange

//            // Act
//            var result = cmd.IsValid("take sword");

//            // Assert
//            Assert.IsTrue(result);
//        }
//        [TestMethod]
//        public void Execute_Should_Make_Sure_Player_Does_Not_Already_Carry_Item()
//        {
//            Assert.Fail();
//        }
//        [TestMethod]
//        public void Execute_Should_Change_Item_Location_To_Inventory()
//        {
//            // Arrange
//            var ball = new Item() { ItemName = "ball", RoomId = 3 };
//            var sword = new Item() { ItemName = "sword", RoomId = 3 };
//            repository.Stub(qq => qq.AsQueryable()).Return((new List<Item>() { ball, sword }).AsQueryable());



//            // Act
//            

//            // Assert
//            Assert.Fail();
//        }
//    }
//}
