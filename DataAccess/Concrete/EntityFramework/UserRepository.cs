using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EfEntityRepositoryBase<User, ProjectDbContext>, IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context)
        {

        }
    }
}
