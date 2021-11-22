using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListAPI.ToDoAPI.Controllers;
using ToDoAPI.Core.Models;
using ToDoAPI.MockService.Test;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using ToDoListAPI.ToDoAPI.Mappings;
using ToDoListAPI.ToDoAPI.DTO;

namespace ToDoAPI.Test
{
    [TestClass]
    public class ToDoItemTest
    {
        /// <summary>
        /// The MockToDoAPI service
        /// </summary>
        private MockToDoService _toDoService;

        /// <summary>
        /// The ToDoItem controller
        /// </summary>
        private ToDoItemController ToDoItemController;

        /// <summary>
        /// The Owner Parameter
        /// </summary>
        private OwnerParameters op;

        /// <summary>
        /// The IMapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// The ToDoItemInDTO
        /// </summary>
        private ToDoItemInDTO itemInDTO;

        /// <summary>
        /// The ToDoItemUpDTO
        /// </summary>
        private ToDoItemUpDTO itemUpDTO;
        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var myProfile = new MappingProfile();
            op = new OwnerParameters();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _toDoService = new MockToDoService();
            _toDoService.toDoItems = new List<ToDoItem>()
            {
                new ToDoItem { ItemId = 11, Description = "ToDoItem_11", CreatedDate=DateTime.UtcNow, ToDoListID = 21 },
                new ToDoItem { ItemId = 12, Description = "ToDoItem_12", CreatedDate=DateTime.UtcNow, ToDoListID = 21 },
                new ToDoItem { ItemId = 13, Description = "ToDoItem_13", CreatedDate=DateTime.UtcNow, ToDoListID = 22 },
                new ToDoItem { ItemId = 14, Description = "ToDoItem_14", CreatedDate=DateTime.UtcNow, ToDoListID = 22 }
            };
            ToDoItemController = new ToDoItemController(_toDoService, _mapper);
        }

        /// <summary>
        /// Gets the ToDoItem valid data.
        /// </summary>
        [TestMethod]
        public void GetToDoItem_Returns_ToDoItemData()
        {
            var response = (ToDoItemController.GetTodoItemList(op).Result.Result) as ObjectResult;
            var jj = ((IEnumerable<ToDoItemDTO>)(response.Value)).ToList();
            Assert.AreEqual(jj.Count, 4);
        }

        /// <summary>
        /// Gets the ToDoItem valid data.
        /// </summary>
        [TestMethod]
        public void GetToDoItem_Should_ThroughException()
        {
            _toDoService.FailGet = true;
            var response = (ToDoItemController.GetTodoItemList(op).Result.Result) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetToDoItem_Should_Return_HttpOkResult_With_ToDoItem()
        {
            var response = (ToDoItemController.GetTodoItemList(op).Result.Result) as ObjectResult;
            var ToDoItems = ((IEnumerable<ToDoItemDTO>)(response.Value)).ToList();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(ToDoItems.Count, 4);
        }

        [TestMethod]
        public void SearchToDoItem_Should_ThroughException()
        {
            _toDoService.FailGet = true;
            var response = (ToDoItemController.SearchTodoItem("Item", op).Result);
            Assert.AreEqual((int)HttpStatusCode.NotFound, (response.Result as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public void SearchToDoItem_Should_Return_HttpOkResult_With_ToDoItem()
        {
            var response = (ToDoItemController.SearchTodoItem("Item", op).Result.Result) as ObjectResult;
            var ToDoItems = ((IEnumerable<ToDoItemDTO>)(response.Value)).ToList();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(ToDoItems.Count, 4);
        }

        [TestMethod]
        public void GetToDoItemByID_Should_ThroughException()
        {
            _toDoService.FailGet = true;
            var response = (ToDoItemController.GetTodoItem(11).Result.Result) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetToDoItemByID_Should_Return_HttpOkResult_With_ToDoItem()
        {
            var response = (ToDoItemController.GetTodoItem(11).Result.Result) as ObjectResult;
            var ToDoItems = ((ToDoItemDTO)(response.Value));
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(ToDoItems.ItemId, 11);
        }

        [TestMethod]
        public void CreateToDoItem_Returns_HttpOKStatus_With_NewToDoItemData()
        {
            itemInDTO = new ToDoItemInDTO { ToDoListID = 11, Description = "newToDoItem" };

            var response = (ToDoItemController.PostTodoItem(itemInDTO).Result.Result) as ObjectResult;
            var newToDoItem = ((ToDoItemDTO)(response.Value));

            Assert.AreNotEqual(newToDoItem.ItemId, 0);
            Assert.AreEqual(newToDoItem.ToDoListID, 11);
            Assert.AreEqual(newToDoItem.Description, "newToDoItem");
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void CreateToDoItem_Return_HttpBadRequest_With_NoData()
        {
            itemInDTO = new ToDoItemInDTO { ToDoListID = 0, Description = null };
            _toDoService.FailGet = true;

            var response = (ToDoItemController.PostTodoItem(itemInDTO).Result);
            var newToDoItem = ((ToDoItemDTO)(response.Value));

            Assert.AreEqual(newToDoItem, null);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (response.Result as BadRequestObjectResult).StatusCode);
        }

        [TestMethod]
        public void UpdateToDoItem_Returns_HttpNoContentResult_OnSuccess()
        {
            itemUpDTO = new ToDoItemUpDTO { ItemId = 11, Description = "updateToDoItem" };
            var response = (ToDoItemController.PutTodoItem(itemUpDTO).Result);
            Assert.AreEqual(((int)HttpStatusCode.NoContent), (response as NoContentResult).StatusCode);
        }

        [TestMethod]
        public void UpdateToDoItem_Returns_HttpNotFound_OnRecord()
        {
            itemUpDTO = new ToDoItemUpDTO { ItemId = 9999, Description = "updateToDoItem" };
            var response = (ToDoItemController.PutTodoItem(itemUpDTO).Result);
            Assert.AreEqual(((int)HttpStatusCode.NotFound), (response as NotFoundResult).StatusCode);
        }

        [TestMethod]
        public void PatchToDoItem_Returns_HttpNoContentResult_OnSuccess()
        {
            JsonPatchDocument<ToDoItem> toDoItem = new JsonPatchDocument<ToDoItem>();
            toDoItem.Replace(r => r.Description, "PatchValue");
            var response = (ToDoItemController.JsonPatchTodoItem(21, toDoItem)).Result;
            Assert.AreEqual(((int)HttpStatusCode.OK), (response as OkObjectResult).StatusCode);
        }

        [TestMethod]
        public void PatchToDoItem_Returns_HttpNotFound_OnRecord()
        {
            JsonPatchDocument<ToDoItem> toDoItem = new JsonPatchDocument<ToDoItem>();
            toDoItem.Replace(r => r.Description, "PatchValue");
            var response = (ToDoItemController.JsonPatchTodoItem(9999, toDoItem).Result);
            Assert.AreEqual(((int)HttpStatusCode.BadRequest), (response as BadRequestResult).StatusCode);
        }

        [TestMethod]
        public void DeleteToDoItem_OnSuccess_Returns_NoContentHttpResult()
        {
            var response = ((ToDoItemController.DeleteTodoItem(12)).Result);
            var deleteRequestResult = (response as NoContentResult);
            Assert.AreEqual(((int)HttpStatusCode.NoContent), deleteRequestResult.StatusCode);
        }

        [TestMethod]
        public void DeleteToDoItem_OnFailure_Returns_NotFoundHttpResult()
        {
            var response = ((ToDoItemController.DeleteTodoItem(999)).Result);
            var deleteRequestResult = (response as NotFoundResult);
            Assert.AreEqual(((int)HttpStatusCode.NotFound), deleteRequestResult.StatusCode);
        }
    }
}
