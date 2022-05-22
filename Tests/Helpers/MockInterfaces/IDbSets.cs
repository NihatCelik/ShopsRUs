using Microsoft.EntityFrameworkCore;

namespace Tests.Helpers.MockInterfaces
{
    public interface IDbSets
    {
        DbSet<Customer> Customers { get; set; }
    }
}
