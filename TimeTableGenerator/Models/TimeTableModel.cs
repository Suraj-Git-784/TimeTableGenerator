using System.ComponentModel.DataAnnotations;

namespace TimeTableGenerator.Models
{
    public class TimeTableModel
    {
        [Required, Range(1, 7, ErrorMessage = "Please enter days between 1 to 7.")]
        public int WorkDays { get; set; }

        [Required, Range(1, 9, ErrorMessage = "Please enter subjects between 1 to 9.")]
        public int Periods { get; set; }

        [Required(ErrorMessage = "Please enter total number of subjects.")]
        public int TotalSubjects { get; set; }

        public int TotalPeriods => WorkDays * Periods;
    }
}
