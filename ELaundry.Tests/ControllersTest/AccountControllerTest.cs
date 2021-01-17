using System;
using System.Web.Mvc;
using ELaundry.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ELaundry.Tests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

      
      
    }
}


