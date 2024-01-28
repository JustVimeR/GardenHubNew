using System.Reflection;

namespace Models
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public bool Successful { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
