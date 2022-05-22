using Entities.Abstract;
using Entities.Enums;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Invoice : IEntity
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal SubTotal { get; set; }

        public int DiscountRate { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal Total { get; set; }

        public StoreType StoreType { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
