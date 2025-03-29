using Microsoft.AspNetCore.Mvc;
using TimeTableGenerator.Models;

namespace TimeTableGenerator.Controllers
{
    public class TimeTableController : Controller
    {
        public IActionResult Index()
        {
            return View(new TimeTableModel());
        }

        [HttpPost]
        public IActionResult EnterSubjectHours(TimeTableModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            var subjectHours = new List<SubjectHoursModel>();
            for (int i = 0; i < model.TotalSubjects; i++)
            {
                subjectHours.Add(new SubjectHoursModel { Subject = $"Subject {i + 1}", Hours = 0 });
            }

            ViewBag.TotalPeriods = model.TotalPeriods;
            return View(subjectHours);
        }

        [HttpPost]
        public IActionResult GenerateTimeTable(List<SubjectHoursModel> subjectHours, int totalPeriods)
        {
            if (subjectHours.Sum(s => s.Hours) != totalPeriods)
            {
                ViewBag.Error = "Total hours must match calculated hours!";
                return View("EnterSubjectHours", subjectHours);
            }
            var timetable = new List<List<string>>();
            int workingDays = (int)Math.Ceiling((double)totalPeriods / subjectHours.Count);
            int subjectsPerDay = subjectHours.Count;

            var allSubjects = subjectHours.SelectMany(s => Enumerable.Repeat(s.Subject, s.Hours)).ToList();
            var rnd = new Random();
            allSubjects = allSubjects.OrderBy(x => rnd.Next()).ToList();

            for (int i = 0; i < subjectsPerDay; i++)
            {
                timetable.Add(new List<string>());
                for (int j = 0; j < workingDays; j++)
                {
                    timetable[i].Add(allSubjects.First());
                    allSubjects.RemoveAt(0);
                }
            }

            ViewBag.TimeTable = timetable;
            return View("GeneratedTimeTable");


        }
    }
}
