using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeTableGenerator.Models
{
    public class SubjectHoursEntry
    {
        public string SubjectName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Hours must be a positive number.")]
        public int Hours { get; set; }
    }

    public class AllocateHoursModel
    {
        public int TotalSubjects { get; set; }
        public int TotalHours { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please allocate hours for each subject.")]
        public List<SubjectHoursEntry> SubjectHours { get; set; } = new List<SubjectHoursEntry>();
    }
}
