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
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber) //Parsing necessary variables for sorting and pagination.
        {
            //Assigning values to sorting parameters
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = string.IsNullOrEmpty(sortOrder)? "date_desc" : "";

            //If the user enters something, the 1st page of results is displayed.
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //Set the current filter to the user input
            ViewData["CurrentFilter"] = searchString;

            //Fetching the data from the database and its related data from other tables - (via include).
            var schedules = from s in _context.Schedule.Include(s => s.Courses).ThenInclude(s => s.Teachers).Include(s => s.Locations) select s;

            //Applying the search filter if user inputs one (using the where and contains keywords to ensure the data matches the criteria entered).
            if (!string.IsNullOrEmpty(searchString))
            {
                schedules = schedules.Where(s => s.Courses.CourseName.Contains(searchString) || s.Locations.LocationName.Contains(searchString));
            }

            //Apply sorting based on the sortOrder parameter (in this case sorting by date upon clicking the hyperlink)
            switch (sortOrder)
            {
                case "date_desc":
                    schedules = schedules.OrderByDescending(s => s.Date);
                    break;
                default:
                    schedules = schedules.OrderBy(s => s.Date);
                    break;
            }

            int pageSize = 10; //Sets the number of records per page

            //Returns the view with the paginated list format
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

        //This method allows users to sort the schedule by a specific date
        public async Task<IActionResult> SortSchedule(DateTime? Date, string sortOrder, string currentFilter, int? pageNumber)
        {
            //If the user enters no date and clicks search, redirect to the index page.
            if (Date == null)
            {
                
                return RedirectToAction(nameof(Index));
            }
            
            //Fetching the schedule data for the date selected by the user when searching.
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
