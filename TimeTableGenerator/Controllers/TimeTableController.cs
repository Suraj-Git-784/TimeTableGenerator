using Microsoft.AspNetCore.Mvc;
using TimeTableGenerator.Models;

namespace TimeTableGenerator.Controllers
{
    public class TimeTableController : Controller
    {
        [HttpGet]
        public IActionResult Generate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Generate(TimeTableInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Store data in TempData to use in the next view
            TempData["WorkingDays"] = model.WorkingDays;
            TempData["SubjectsPerDay"] = model.SubjectsPerDay;
            TempData["TotalSubjects"] = model.TotalSubjects;
            TempData["TotalHours"] = model.TotalHours;

            return RedirectToAction("AllocateHours");
        }

        [HttpGet]
        public IActionResult AllocateHours()
        {
            if (TempData["TotalSubjects"] == null || TempData["TotalHours"] == null)
            {
                return RedirectToAction("Generate");
            }

            int totalSubjects = (int)TempData["TotalSubjects"];
            int totalHours = (int)TempData["TotalHours"];

            var model = new AllocateHoursModel
            {
                TotalSubjects = totalSubjects,
                TotalHours = totalHours,
                SubjectHours = new List<SubjectHoursEntry>()
            };

            // Sample subjects (replace with user input if needed)
            string[] subjects = { "Gujarati", "English", "Science", "Maths" };

            // Evenly distribute hours among subjects
            int baseHours = totalHours / subjects.Length;
            int remainder = totalHours % subjects.Length;

            for (int i = 0; i < subjects.Length; i++)
            {
                int allocatedHours = baseHours + (i < remainder ? 1 : 0); // Distribute remainder hours
                model.SubjectHours.Add(new SubjectHoursEntry { SubjectName = subjects[i], Hours = allocatedHours });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AllocateHours(AllocateHoursModel model)
        {
            if (model.SubjectHours.Sum(s => s.Hours) != model.TotalHours)
            {
                ModelState.AddModelError("", "Total hours must match the weekly total.");
                return View(model);
            }

            TempData["FinalSubjects"] = Newtonsoft.Json.JsonConvert.SerializeObject(model.SubjectHours);
            return RedirectToAction("GenerateTimeTable");
        }
    }
}
