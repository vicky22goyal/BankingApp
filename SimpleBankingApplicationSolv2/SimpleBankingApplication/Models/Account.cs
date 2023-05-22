using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleBankingApplication.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }




    }
}
