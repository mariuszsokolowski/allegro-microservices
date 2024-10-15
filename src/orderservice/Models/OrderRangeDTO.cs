using System.ComponentModel;

namespace orderservice.Models
{
    public class OrderRangeDTO
    {
        [DefaultValue("2024-10-15")]
        public string FromDate { get; set; } = string.Empty;
        [DefaultValue("2024-10-20")]
        public string ToDate { get; set; } = string.Empty;
        [DefaultValue("5")]
        public int PageSize { get; set; }
        [DefaultValue("1")]
        public int PageNumber { get; set; }
    }
}
