using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PeScheduleDB.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PeScheduleDB.Controllers
{   
    [Authorize]
    public class SchedulesController : Controller
    {
        private readonly PeScheduleDBContext _context;

        public SchedulesController(PeScheduleDBContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder)? "date_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var schedules = from s in _context.Schedule.Include(s => s.Courses).ThenInclude(s => s.Teachers).Include(s => s.Locations) select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                schedules = schedules.Where(s => s.Courses.CourseName.Contains(searchString) || s.Locations.LocationName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    schedules = schedules.OrderByDescending(s => s.Date);
                    break;
                default:
                    schedules = schedules.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Schedule>.CreateAsync(schedules.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Courses)
                .Include(s => s.Locations)
                .Include(s => s.Courses.Teachers)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        [Authorize(Roles = "Teacher")]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseName");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,CourseId,LocationId,Date")] Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", schedule.CourseId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", schedule.LocationId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseName", schedule.CourseId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationName", schedule.LocationId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,CourseId,LocationId,Date")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId", schedule.CourseId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "LocationId", schedule.LocationId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Courses)
                .Include(s => s.Locations)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.ScheduleId == id);
        }
        public async Task<IActionResult> SortSchedule(DateTime? Date, string sortOrder, string currentFilter, int? pageNumber)
        {
            if (Date == null)
            {
                
                return RedirectToAction(nameof(Index));
            }

            var schedules = from s in _context.Schedule.Include(s => s.Courses).ThenInclude(s => s.Teachers).Include(s => s.Locations)
                            where s.Date.Date == Date.Value.Date
                            select s;

            switch (sortOrder)
            {
                case "date_desc":
                    schedules = schedules.OrderByDescending(s => s.Date);
                    break;
                default:
                    schedules = schedules.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 10;

            return View("Index", await PaginatedList<Schedule>.CreateAsync(schedules.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

    }
}
