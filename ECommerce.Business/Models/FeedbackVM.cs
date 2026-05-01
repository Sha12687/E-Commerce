using System.ComponentModel.DataAnnotations;

namespace ECommerce.Business.Models
{
    public class FeedbackVM
    {
        public int? Id { get; set; }

        public string? UserId { get; set; }
        public string? Question { get; set; }

        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime CreationDate { get; set; }
    }
}