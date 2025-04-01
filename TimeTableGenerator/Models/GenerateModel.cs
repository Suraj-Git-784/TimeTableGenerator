using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeTableGenerator.Models
{
    public class GenerateModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total hours must be greater than zero.")]
        public int TotalHours { get; set; }

        [Required(ErrorMessage = "Please enter at least one subject.")]
        public List<string> Subjects { get; set; } = new List<string>();
    }
}
