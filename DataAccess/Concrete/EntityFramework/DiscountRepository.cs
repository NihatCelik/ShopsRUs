using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class DiscountRepository : EfEntityRepositoryBase<Discount, ProjectDbContext>, IDiscountRepository
    {
        public DiscountRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
