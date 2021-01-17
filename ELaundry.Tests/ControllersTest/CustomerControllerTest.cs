using System;
using System.Web.Mvc;
using ELaundry.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ELaundry.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void SchedulePickup()
        {
            // Arrange
            CustomerController controller = new CustomerController();

            // Act
            ViewResult result = controller.SchedulePickup() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

      
    }
}


