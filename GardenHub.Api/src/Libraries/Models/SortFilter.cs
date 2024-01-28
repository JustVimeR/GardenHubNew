using System.Reflection;

namespace Models
{
    public class SortFilter
    {
        public PropertyInfo? PropertyInfo { get; set; }
        public required string SortBy { get; set; }
        public bool Descending { get; set; }
    }

}
