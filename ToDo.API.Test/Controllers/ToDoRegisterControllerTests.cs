using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoAPI.Core.Models;
using ToDoAPITest.MockService.Test;
using ToDoListAPI.ToDoAPI.Controllers;
using ToDoListAPI.ToDoAPI.DTO;
using ToDoListAPI.ToDoAPI.Mappings;

namespace ToDoAPI.Test
{
    [TestClass]
    public class ToDoRegisterControllerTests
    {
        /// <summary>
        /// The MockToDoAPI service
        /// </summary>
        private MockUserService _UserService;

        /// <summary>
        /// The ToDoList controller
        /// </summary>
        private RegisterController loginController;

        /// <summary>
        /// The IMapper
        /// </summary>
        private IMapper _mapper;

        private UsersDTO usersDTO;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _UserService = new MockUserService();
            _UserService.users = new List<Users>()
            {
                new Users { UserId = 201, UserName = "Admin", Password = "Pa$$w0rd" },
                new Users { UserId = 202, UserName = "Standard", Password = "Pa$$w0rd2" },
            };
            loginController = new RegisterController(_UserService, _mapper);
        }

        // / <summary>
        // / Test to register new user.
        // / </summary>
        [TestMethod]
        public void CreateUsers_Returns_With_HttpOkResult()
        {
            usersDTO = new UsersDTO() { UserName = "User2", Password = "pass@key" };

            var response = (loginController.RegisterUser(usersDTO).Result);
            Assert.AreEqual((int)HttpStatusCode.OK, (response as OkResult).StatusCode);
        }

        // / <summary>
        // / Test to fail registeration of new user.
        // / </summary>
        [TestMethod]
        public void CreateUsers_Should_Returns_With_HttpBadRequest()
        {
            usersDTO = new UsersDTO();
            var response = (loginController.RegisterUser(usersDTO));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, (response.Result as BadRequestResult).StatusCode);
        }


    }
}