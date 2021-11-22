using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListAPI.ToDoAPI.Controllers;
using ToDoAPI.Core.Models;
using ToDoAPI.MockService.Test;
using ToDoListAPI.ToDoAPI.DTO;
using AutoMapper;
using ToDoListAPI.ToDoAPI.Mappings;

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
        /// The IMapper
        /// </summary>
        private IMapper _mapper;

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
            _toDoService.labels = new List<Label>()
            {
                new Label { LabelId = 1, Description = "Label1", ToDoItemID = 11 },
                new Label { LabelId = 2, Description = "Label2", ToDoItemID = 11 },
                new Label { LabelId = 3, Description = "Label3", ToDoItemID = 12 },
                new Label { LabelId = 4, Description = "Label4", ToDoItemID = 12 }
            };
            labelController = new ToDoLabelController(_toDoService, _mapper);
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
            var labels = ((IEnumerable<LabelDTO>)(response.Value)).ToList();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(labels.Count, 4);
        }

        [TestMethod]
        public void CreateLabel_Returns_NewLabelData()
        {
            LabelInDTO labelInDTO = new LabelInDTO() { Description = "newlabel", ToDoItemID = 11, ToDoListID = 0 };
            var response = (labelController.PostLabel(labelInDTO).Result.Result) as ObjectResult;
            var newLabel = ((LabelDTO)(response.Value));

            Assert.AreNotEqual(newLabel.LabelId, 0);
            Assert.AreEqual(newLabel.ToDoItemID, 11);
            Assert.AreEqual(newLabel.Description, "newlabel");
            Assert.AreEqual((int)HttpStatusCode.Created, (response as ObjectResult).StatusCode);
        }

        [TestMethod]
        public void CreateLabel_Should_Returns_With_HttpBadRequest_NoData()
        {
            LabelInDTO labelInDTO = new LabelInDTO() { Description = null, ToDoItemID = 0, ToDoListID = 0 };
            _toDoService.FailGet = true;
            var response = (labelController.PostLabel(labelInDTO).Result);
            var newLabel = ((LabelDTO)(response.Value));

            Assert.AreEqual(newLabel, null);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (response.Result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public void DeleteLabel_OnSuccess_Returns_NoContentHttpResult()
        {
            var response = ((labelController.DeleteLabel(2)).Result);
            var deleteRequestResult = (response as NoContentResult);
            Assert.AreEqual(((int)HttpStatusCode.NoContent), deleteRequestResult.StatusCode);
        }
        [TestMethod]
        public void DeleteLabel_OnFailure_Returns_NotFoundHttpResult()
        {
            var response = ((labelController.DeleteLabel(999)).Result);
            var deleteRequestResult = (response as NotFoundResult);
            Assert.AreEqual(((int)HttpStatusCode.NotFound), deleteRequestResult.StatusCode);
        }
    }
}