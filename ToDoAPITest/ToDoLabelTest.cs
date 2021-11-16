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
    public class ToDoLabelTest
    {

        /// <summary>
        /// The MockToDoAPI service
        /// </summary>
        private MockToDoService _toDoService;

        /// <summary>
        /// The label controller
        /// </summary>
        private ToDoLabelController labelController;

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
            _toDoService.labels = new List<Label>()
            {
                new Label { LabelId = 1, Description = "Label1", ItemOwner = 11 },
                new Label { LabelId = 2, Description = "Label2", ItemOwner = 11 },
                new Label { LabelId = 3, Description = "Label3", ItemOwner = 12 },
                new Label { LabelId = 4, Description = "Label4", ItemOwner = 12 }
            };
            labelController = new ToDoLabelController(_toDoService);
        }

        /// <summary>
        /// Gets the label valid data.
        /// </summary>
        [TestMethod]
        public void GetLabel_Returns_LabelData()
        {
            var response = (labelController.GetTodoLabel(op).Result.Result) as ObjectResult;
            var jj = ((PagedList<Label>)(response.Value)).ToList();
            Assert.AreEqual(jj.Count, 4);
        }

        /// <summary>
        /// Gets the label valid data.
        /// </summary>
        [TestMethod]
        public void GetLabel_Should_ThroughException()
        {
            _toDoService.FailGet = true;
            var response = (labelController.GetTodoLabel(op).Result.Result) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void GetLabel_Should_Return_HttpOkResult_With_Label()
        {
            var response = (labelController.GetTodoLabel(op).Result.Result) as ObjectResult;
            var labels = ((PagedList<Label>)(response.Value)).ToList();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(labels.Count, 4);
        }

        [TestMethod]
        public void CreateLabel_Returns_NewLabelData()
        {
            var response = (labelController.PostLabel(11, "newlabel").Result.Result) as ObjectResult;
            var newLabel = ((Label)(response.Value));

            Assert.AreNotEqual(newLabel.LabelId, 0);
            Assert.AreEqual(newLabel.ItemOwner, 11);
            Assert.AreEqual(newLabel.Description, "newlabel");
            Assert.AreEqual((int)HttpStatusCode.OK, (response as ObjectResult).StatusCode);
        }

        [TestMethod]
        public void CreateLabel_Should_Returns_With_HttpBadRequest_NoData()
        {
            _toDoService.FailGet = true;
            var response = (labelController.PostLabel(0, null).Result);
            var newLabel = ((Label)(response.Value));

            Assert.AreEqual(newLabel, null);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (response.Result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public void DeleteLabel_Returns_NoContentHttpResult()
        {
            var response = ((labelController.DeleteLabel(2)).Result);
            var deleteRequestResult = (response as NoContentResult);
            Assert.AreEqual(((int)HttpStatusCode.NoContent), deleteRequestResult.StatusCode);
        }
    }
}