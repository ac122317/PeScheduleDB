using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PeScheduleDB.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PeScheduleDB.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeachersController : Controller
    {
        private readonly PeScheduleDBContext _context;

        public TeachersController(PeScheduleDBContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string sortOrder)
        {
                ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                var teachers = from t in _context.Teacher select t;

                switch (sortOrder)
                {
                    case "name_desc":
                        teachers = teachers.OrderByDescending(s => s.FirstName);
                        break;
                    default:
                        teachers = teachers.OrderBy(s => s.FirstName);
                        break;
                }
                return View(await teachers.AsNoTracking().ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,LastName,FirstName")] Teacher teacher)
        {

            if (!ModelState.IsValid)
            {
                    //Generate a starter teacher code using the first 2 characters of the Last name and the first character of the First name
                    string initialTeacherCode = (teacher.LastName.Substring(0, 2) + teacher.FirstName.Substring(0, 1)).ToUpper();

                    
                    string teacherCode = initialTeacherCode;
                    int suffix = 1;
                
                    //Checking to see if the new teacher's code matches any other teacher code of a teacher that already exists, if so it adds a number to the end of it to ensure it is unique.
                    while (await _context.Teacher.AnyAsync(t => t.TeacherCode == teacherCode))
                    {
                        teacherCode = initialTeacherCode + suffix.ToString();
                        suffix++;
                    }   
                    teacher.TeacherCode = teacherCode;

                    //Generating a teacher email by using the teacher code and added it to the avcol email domain.
                    teacher.Email = (teacher.TeacherCode + "@avcol.school.nz").ToLower();

                    _context.Add(teacher);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));      
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,LastName,FirstName,Email,TeacherCode")] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher != null)
            {
                _context.Teacher.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.TeacherId == id);
        }

        //Method to allow the user to search for teachers by their TeacherCode
        public async Task<IActionResult> SearchTeacher(string TeacherCode)
        {
            if (TeacherCode == null)
            {

                return RedirectToAction(nameof(Index));
            }

            var SortByTeacherCode = _context.Teacher.Where(j => j.TeacherCode == TeacherCode);
            return View("Index", await SortByTeacherCode.ToListAsync());
        }
    }
}
