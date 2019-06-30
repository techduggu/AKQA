using System;
using System.Collections.Generic;
using System.Text;

namespace AKQA.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
    }
}