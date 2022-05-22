using Entities.Abstract;

namespace Entities.Concrete
{
    public class InvoiceDetail : IEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public double Quantity { get; set; }

        public decimal Total { get; set; }

        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
