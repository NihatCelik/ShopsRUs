using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class InvoiceRepository : EfEntityRepositoryBase<Invoice, ProjectDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
