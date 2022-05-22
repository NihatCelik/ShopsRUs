namespace Entities.Dtos
{
    public class BasePagingDto
    {
        public string Query { get; set; }
        public int Take { get; set; }
        public string OrderBy { get; set; }
        public int Skip { get; set; }
    }
}
