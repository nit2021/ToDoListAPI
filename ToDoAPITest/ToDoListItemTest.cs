using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListAPI.ToDoAPI;
using ToDoListAPI.ToDoAPI.Controllers;
using ToDoAPI.Core.Models;
using ToDoAPI.MockService.Test;

namespace ToDoAPI.Test
{
    [TestClass]
    public class ToDoListItemTest
    {
        /// <summary>
        /// The MockToDoAPI service
        /// </summary>
        private MockToDoService _toDoService;

        /// <summary>
        /// The ToDoList controller
        /// </summary>
        private ToDoListController toDoListController;

        /// <summary>
        /// The Owner Parameter
        /// </summary>
        private OwnerParameters op;


        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            op = new OwnerParameters();
            _toDoService = new MockToDoService();
            _toDoService.toDoLists = new List<ToDoList>()
            {
                new ToDoList { ListId = 21, Description = "ToDoList1", CreatedDate=DateTime.UtcNow, Owner = 101 },
                new ToDoList { ListId = 22, Description = "ToDoList2", CreatedDate=DateTime.UtcNow, Owner = 101 },
                new ToDoList { ListId = 23, Description = "ToDoList3", CreatedDate=DateTime.UtcNow, Owner = 102 },
                new ToDoList { ListId = 24, Description = "ToDoList4", CreatedDate=DateTime.UtcNow, Owner = 102 }
            };
            toDoListController = new ToDoListController(_toDoService);
        }

        /// <summary>
        /// Gets the ToDoList valid data.
        /// </summary>
        [TestMethod]
        public void GetToDoList_Returns_ToDoListData()
        {
            var response = (toDoListController.GetTodoListItem(op).Result.Result) as ObjectResult;
            var jj = ((PagedList<ToDoList>)(response.Value)).ToList();
            Assert.AreEqual(jj.Count, 4);
        }

        /// <summary>
        /// Gets the ToDoList valid data.
        /// </summary>
        [TestMethod]
        public void GetToDoList_Should_ThroughException()
        {
            _toDoService.FailGet = true;
            var response = (toDoListController.GetTodoListItem(op).Result.Result) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetToDoList_Should_Return_HttpOkResult_With_ToDoList()
        {
            var response = (toDoListController.GetTodoListItem(op).Result.Result) as ObjectResult;
            var ToDoLists = ((PagedList<ToDoList>)(response.Value)).ToList();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(ToDoLists.Count, 4);
        }

        [TestMethod]
        public void SearchToDoList_Should_ThroughException()
        {
            _toDoService.FailGet = true;
            var response = (toDoListController.SearchTodoListItem("ToDoList2", op).Result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, (response.Result as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public void SearchToDoList_Should_Return_HttpOkResult_With_ToDoList()
        {
            var response = (toDoListController.SearchTodoListItem("ToDoList2", op).Result.Result) as ObjectResult;
            var ToDoLists = ((PagedList<ToDoList>)(response.Value)).ToList();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(ToDoLists.Count, 1);
        }

        [TestMethod]
        public void CreateToDoList_Returns_NewToDoListData()
        {
            var response = (toDoListController.PostTodoListItem("newToDoList").Result.Result) as ObjectResult;
            var newToDoList = ((ToDoList)(response.Value));

            Assert.AreNotEqual(newToDoList.ListId, 0);
            Assert.AreEqual(newToDoList.Description, "newToDoList");
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void CreateToDoList_Returns_HttpBadRequest_NoData()
        {
            _toDoService.FailGet = true;
            var response = (toDoListController.PostTodoListItem(null).Result);
            var newToDoList = ((ToDoList)(response.Value));

            Assert.AreEqual(newToDoList, null);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (response.Result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public void UpdateToDoList_Returns_HttpNoContentResult_OnSuccess()
        {
            var response = (toDoListController.PutTodoListItem(21, "updateToDoList").Result);
            Assert.AreEqual(((int)HttpStatusCode.NoContent), (response as NoContentResult).StatusCode);
        }

        [TestMethod]
        public void UpdateToDoList_Returns_HttpNotFound_OnRecord()
        {
            var response = (toDoListController.PutTodoListItem(9999, "updateToDoList").Result);
            Assert.AreEqual(((int)HttpStatusCode.NotFound), (response as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public void DeleteToDoList_Returns_NoContentHttpResult()
        {
            var response = ((toDoListController.DeleteTodoListItem(2)).Result);
            var deleteRequestResult = (response as NoContentResult);
            Assert.AreEqual(((int)HttpStatusCode.NoContent), deleteRequestResult.StatusCode);
        }
    }
}