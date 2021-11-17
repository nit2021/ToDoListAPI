using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoAPITest.MockService.Test;
using ToDoListAPI.ToDoAPI.Controllers;

namespace ToDoAPI.Test
{
    [TestClass]
    public class ToDoUserTest
    {
        /// <summary>
        /// The MockToDoAPI service
        /// </summary>
        private MockUserService _UserService;

        /// <summary>
        /// The ToDoList controller
        /// </summary>
        private LoginController loginController;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _UserService = new MockUserService();
            _UserService.userId = 101;
            _UserService.userName = "Admin";
            _UserService.Password = "Pa$$w0rd";
            loginController = new LoginController(_UserService);
        }

        // / <summary>
        // / Authenticate valid user.
        // / </summary>
        [TestMethod]
        public void AuthenticateUser_OnSucess_Returns_HttOkStatusCode()
        {
            var response = (loginController.AuthenticateUser("Admin", "Pa$$w0rd").Result);
            var StatusCode= (response as OkResult) ;
            Assert.AreEqual((int)HttpStatusCode.OK, StatusCode.StatusCode);

        }

        // / <summary>
        // / Authenticate invalid user.
        // / </summary>
        [TestMethod]
        public void AuthenticateUser_OnFailure_Returns_HttUnauthorisedStatusCode()
        {
            var response = (loginController.AuthenticateUser("dummy", "Pw0rd").Result);
            var StatusCode= (response as UnauthorizedResult) ;
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, StatusCode.StatusCode);
        }

    }
}