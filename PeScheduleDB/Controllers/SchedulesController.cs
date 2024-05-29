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
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            var schedules = from s in _context.Schedule.Include(s => s.Courses).ThenInclude(s => s.Teachers).Include(s => s.Locations) select s;

            switch (sortOrder)
            {
                case "date_desc":
                    schedules = schedules.OrderByDescending(s => s.Date);
                    break;
                default:
                    schedules = schedules.OrderBy(s => s.Date);
                    break;
            }

            return View(await schedules.ToListAsync());
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
        public async Task<IActionResult> SortSchedule(DateTime? Date)
        {
            if (Date == null)
            {
                
                return RedirectToAction(nameof(Index));
            }

            var FilterDate = Date.Value.Date;

            var ScheduleDate = _context.Schedule.Where(j => j.Date.Date == FilterDate).Include (s => s.Courses).ThenInclude(s => s.Teachers).Include (s => s.Locations);

            return View("Index", await ScheduleDate.ToListAsync());
        }

    }
}
