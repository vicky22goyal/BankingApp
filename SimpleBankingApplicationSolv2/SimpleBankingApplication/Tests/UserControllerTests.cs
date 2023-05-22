using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankingApplication.Controllers;
using SimpleBankingApplication.Models;
using SimpleBankingApplication.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimpleBankingApplication.Tests
{
    public class UsersControllerTests
    {
        [Fact]
        public void GetUsers_ReturnsAllUsers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using var context = new DataContext(options);
            var controller = new UsersController(context);
            context.Users.AddRange(GetTestUsers());
            context.SaveChanges();

            // Act
            var result = controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
            var users = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, users.Count());
        }
        private List<User> GetTestUsers()
        {
            return new List<User>
        {
            new User { Name = "Alice", Accounts = new List<Account>() },
            new User { Name = "Bob", Accounts = new List<Account>() }
        };
        }

        // Add test cases for other UsersController actions
    }
}
