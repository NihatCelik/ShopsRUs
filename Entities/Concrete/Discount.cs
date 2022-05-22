using Entities.Abstract;
using Entities.Enums;

namespace Entities.Concrete
{
    public class Discount : IEntity
    {
        public int Id { get; set; }

        public UserType UserType { get; set; }

        public int OverYear { get; set; }

        public int DiscountRate { get; set; }
    }
}
