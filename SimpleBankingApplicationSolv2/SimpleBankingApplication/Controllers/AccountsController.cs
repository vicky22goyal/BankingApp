using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBankingApplication.Models;
using SimpleBankingApplication.Data;

namespace SimpleBankingApplication.Controllers
{
    [Route("api/users/{userId}/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAccounts(int userId)
        {
            var user = _context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            return user.Accounts;
        }

        [HttpPost]
        public ActionResult<Account> CreateAccount(int userId)
        {
            var user = _context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var account = new Account { Id = user.Accounts.Count + 1, Balance = 0 };
            user.Accounts.Add(account);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAccounts), new { userId = user.Id }, account);
        }

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccount(int userId, int accountId)
        {
            var user = _context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var account = user.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }

            user.Accounts.Remove(account);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("{accountId}/deposit")]
        public IActionResult Deposit(int userId, int accountId, [FromBody] decimal amount)
        {
            var user = _context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var account = user.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }

            account.Balance += amount;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("{accountId}/withdraw")]
        public IActionResult Withdraw(int userId, int accountId, [FromBody] decimal amount)
        {
            var user = _context.Users.Include(u => u.Accounts).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var account = user.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                return NotFound();
            }

            if (account.Balance - amount < 100)
            {
                return BadRequest("Insufficient funds");
            }

            var maxWithdrawal = account.Balance * 0.9m;
            if (amount > maxWithdrawal)
            {
                return BadRequest("Exceeded maximum withdrawal amount");
            }

            account.Balance -= amount;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
