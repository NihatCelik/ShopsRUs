using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class InvoiceRepository : EfEntityRepositoryBase<Invoice, ProjectDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
