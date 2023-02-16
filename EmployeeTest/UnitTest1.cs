using Employ.Business.Services;
using EmployeeManagmentSystem.Controllers;
using EmployeeManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeeTest
{
    public class UnitTest1
    {
        public class EmployeeControllerTests
        {
            [Fact]
            public void Index_ReturnsAViewResult_WithAListOfEmployees()
            {
                // Arrange
                var mockService = new Mock<IEmployeeService>();
                var employees = new List<Employee>
                {
                    new Employee
                        { EmployeeId = 1, Name = "John Doe", Email = "john.doe@example.com", Department = "test" },
                    new Employee
                        { EmployeeId = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Department = "test" },
                };
                mockService.Setup(service => service.GetAllEmploy());
                var controller = new EmployeeController(mockService.Object);
                // Act
                var result = controller.Index();
                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count());
            }

            [Fact]
            public void Create_ReturnsAViewResult_WhenModelStateIsInvalid()
            {
                // Arrange
                var mockService = new Mock<IEmployeeService>();
                var controller = new EmployeeController(mockService.Object);
                controller.ModelState.AddModelError("Name", "Name is required");
                var employee = new Employee { Name = "John Doe", Email = "john.doe@example.com", Department = "test" };

                // Act
                var result = controller.Create(employee);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Equal(employee, viewResult.ViewData.Model);
            }

            [Fact]
            public void Create_RedirectsToIndex_WhenModelStateIsValid()
            {
                // Arrange
                var mockService = new Mock<IEmployeeService>();
                var controller = new EmployeeController(mockService.Object);
                var employee = new Employee { Name = "John Doe", Email = "john.doe@example.com", Department = "test" };

                // Act
                var result = controller.Create(employee);

                // Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }

            [Fact]
            public void Edit_ReturnsAViewResult_WithAnEmployee()
            {
                // Arrange
                var mockService = new Mock<IEmployeeService>();
                var employee = new Employee
                    { EmployeeId = 1, Name = "John Doe", Email = "john.doe@example.com", Department = "test" };
                mockService.Setup(service => service.GetEmpById(1));
                var controller = new EmployeeController(mockService.Object);

                // Act
                var result = controller.Edit(1);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsType<Employee>(viewResult.ViewData.Model);
                Assert.Equal(employee, model);
            }
        }
    }
}