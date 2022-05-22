using Entities.Abstract;
using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public UserType UserType { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
