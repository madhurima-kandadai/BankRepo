using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateStreet.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }

        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
    }
}
