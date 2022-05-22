
namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, int count) : base(success)
        {
            Data = data;
            Count = count;
        }

        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }
        public int Count { get; }
    }
}
