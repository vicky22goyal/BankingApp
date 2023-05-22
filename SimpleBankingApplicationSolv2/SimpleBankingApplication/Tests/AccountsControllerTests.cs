using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankingApplication.Controllers;
using SimpleBankingApplication.Models;
using SimpleBankingApplication.Data;
using Xunit;
using Assert = Xunit.Assert;

namespace SimpleBankingApplication.Tests
{
    public class AccountsControllerTests
    {
        [Fact]
        public void GetAccounts_WithValidUserId_ReturnsUserAccounts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using var context = new DataContext(options);
            var controller = new AccountsController(context);
            context.Users.AddRange(GetTestUsers());
            context.SaveChanges();
            var userId = 1;

            // Act
            var result = controller.GetAccounts(userId);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Account>>>(result);
            var accounts = Assert.IsAssignableFrom<IEnumerable<Account>>(okResult.Value);
            Assert.Equal(0, accounts.Count());
        }

        // Add test cases for other AccountsController actions


        private List<User> GetTestUsers()
        {
            return new List<User>
        {
            new User {  Name = "Alice", Accounts = new List<Account>() },
            new User {  Name = "Bob", Accounts = new List<Account>() }
        };
        }
    }
}
