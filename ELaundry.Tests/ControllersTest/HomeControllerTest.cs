using System;
using System.Web.Mvc;
using ELaundry.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ELaundry.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

      
        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Services() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Pricing()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Pricing() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}


