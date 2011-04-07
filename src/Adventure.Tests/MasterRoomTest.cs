using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Adventure.Data;
using Adventure.Commands;

namespace MasterRoomTests.Tests
{
    [TestClass]
    public class MasterRoomTest
    {
        private MasterRoom cmd;
        private IRepository<GameObject> repository;

         [TestInitialize]
        public void Before_Each_Test()
        {
            repository = MockRepository.GenerateMock<IRepository<GameObject>>();
            cmd = new MasterRoom(repository);
         }
        [TestMethod]
        public void MasterRoom_Should_Return_A_GameObjID_If_A_Master_Room_Exists()
        {
            // Arrange
            
            
            // Act
            
            
            // Assert
            Assert.Fail();
        }
        [TestMethod]
        public void MasterRoom_Should_Create_A_New_MasterRoom_Obj_And_Return_Its_Id_If_It_Didnt_Already_Exist()
        {
            // Arrange
            

            // Act
            

            // Assert
            Assert.Fail();
        }
    }
}
